using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using scms.Application.DTOs;
using scms.Application.Interfaces;
using scms.Domain.Entities.SCMS;
using SCMS.Shared.Exceptions;

namespace scms.Application.Services.SCMS;

public class TenantOnboardingService : ITenantOnboardingService
{
    private readonly ITenantRepository _tenantRepository;
    private readonly IPlanRepository _planRepository;
    private readonly ITenantMigrator _tenantMigrator;
    private readonly ITenantDbSeeder _tenantDbSeeder;
    private readonly IPlanModuleRepository _planModuleRepository;
    private readonly IModulePermissionRepository _modulePermissionRepository;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly IEmailSender _emailSender;
    private readonly IMapper _mapper;
    private readonly ILogger<TenantOnboardingService> _logger;
    private readonly IConfiguration _config;

    public TenantOnboardingService(
        ITenantRepository tenantRepository,
        IPlanRepository planRepository,
        ITenantMigrator tenantMigrator,
        ITenantDbSeeder tenantDbSeeder,
        IPlanModuleRepository planModuleRepository,
        IModulePermissionRepository modulePermissionRepository,
        IPasswordHasherService passwordHasherService,
        IEmailSender emailSender,
        IMapper mapper,
        ILogger<TenantOnboardingService> logger,
        IConfiguration config)
    {
        _tenantRepository = tenantRepository;
        _planRepository = planRepository;
        _tenantMigrator = tenantMigrator;
        _tenantDbSeeder = tenantDbSeeder;
        _planModuleRepository = planModuleRepository;
        _modulePermissionRepository = modulePermissionRepository;
        _passwordHasherService = passwordHasherService;
        _emailSender = emailSender;
        _mapper = mapper;
        _logger = logger;
        _config = config;
    }

    public async Task<TenantOnboardingResult> OnboardAsync(CreateTenantDto dto, CancellationToken ct = default)
    {
        // Step 1: Validate TenantCode uniqueness
        var exists = await _tenantRepository.ExistsByCodeAsync(dto.TenantCode, ct);
        if (exists)
        {
            throw new ConflictException($"Tenant code '{dto.TenantCode}' already exists.");
        }

        // Step 2: Validate PlanId is active
        var plan = await _planRepository.GetActiveByIdAsync(dto.PlanId, ct);
        if (plan == null)
        {
            throw new BadRequestException($"Plan '{dto.PlanId}' does not exist or is inactive.");
        }

        // Step 3: Insert Tenant row (Status=Pending, IsActive=false)
        var tenant = _mapper.Map<Domain.Entities.SCMS.Tenant>(dto);
        tenant.Status = TenantStatus.Pending;
        tenant.IsActive = false;
        await _tenantRepository.AddAsync(tenant, ct);

        try
        {
            // Step 4: Update Status -> Provisioning
            tenant.Status = TenantStatus.Provisioning;
            await _tenantRepository.UpdateAsync(tenant, ct);

            // Step 5: Delegate schema migration
            var migrationResult = await _tenantMigrator.MigrateAsync(tenant.TenantCode, ct);
            if (!migrationResult.Success)
            {
                return await FailTenantAsync(tenant, "Step 5 - Migration failed", migrationResult.Error?.Message, ct);
            }

            // Step 6: Resolve module list
            var planModules = await _planModuleRepository.GetByPlanIdAsync(tenant.PlanId, ct);
            var moduleIds = planModules.Select(pm => pm.ModuleId).ToList();

            // Step 7: Resolve permission keys
            var modulePermissions = await _modulePermissionRepository.GetByModuleIdsAsync(moduleIds, ct);
            
            // Deduplicate permissions by PermissionKey
            var distinctPermissions = modulePermissions
                .GroupBy(mp => mp.PermissionKey)
                .Select(g => g.First())
                .ToList();

            var distinctPermissionKeys = distinctPermissions.Select(mp => mp.PermissionKey);

            // Step 8: Seed permissions, roles, and admin user into tenant DB
            var seedResult = await _tenantDbSeeder.SeedTenantDataAsync(
                migrationResult.ConnectionString, 
                tenant.TenantCode,
                tenant.Email,
                distinctPermissionKeys, 
                plan.PlanName, 
                ct);

            // Step 11: Send credentials email
            string subject = $"Welcome to SCMS - Your {tenant.Name} Credentials";
            string body = $@"
                <h2>Welcome to SCMS!</h2>
                <p>Your tenant environment has been successfully provisioned.</p>
                <p><strong>Username:</strong> {seedResult.Username}</p>
                <p><strong>Temporary Password:</strong> {seedResult.RawPassword}</p>
                <p><i>Note: You will be required to change your password upon your first login.</i></p>
            ";
            await _emailSender.SendAsync(tenant.Email, subject, body, ct);

            // Step 12: Mark Tenant Active
            tenant.Status = TenantStatus.Active;
            tenant.IsActive = true;
            await _tenantRepository.UpdateAsync(tenant, ct);

            return new TenantOnboardingResult(true, tenant.Id, tenant.TenantCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to onboard tenant {TenantCode}", tenant.TenantCode);
            return await FailTenantAsync(tenant, "Exception during onboarding steps", ex.Message, ct);
        }
    }

    private async Task<TenantOnboardingResult> FailTenantAsync(Domain.Entities.SCMS.Tenant tenant, string step, string? reason, CancellationToken ct)
    {
        tenant.Status = TenantStatus.Failed;
        tenant.IsActive = false;
        await _tenantRepository.UpdateAsync(tenant, ct);
        return new TenantOnboardingResult(false, tenant.Id, tenant.TenantCode, reason, step);
    }

    public async Task UpdateTenantPermissionsAsync(Guid tenantId, CancellationToken ct = default)
    {
        var tenant = await _tenantRepository.GetByIdAsync(tenantId, ct);
        if (tenant == null)
        {
            throw new NotFoundException($"Tenant with ID {tenantId} not found.");
        }

        var plan = await _planRepository.GetActiveByIdAsync(tenant.PlanId, ct);
        if (plan == null)
        {
            throw new BadRequestException($"Plan '{tenant.PlanId}' does not exist or is inactive.");
        }

        var migrationResult = await _tenantMigrator.MigrateAsync(tenant.TenantCode, ct);
        if (!migrationResult.Success)
        {
            throw new InvalidOperationException($"Migration failed for tenant '{tenant.TenantCode}'.");
        }

        var planModules = await _planModuleRepository.GetByPlanIdAsync(tenant.PlanId, ct);
        var moduleIds = planModules.Select(pm => pm.ModuleId).ToList();

        var modulePermissions = await _modulePermissionRepository.GetByModuleIdsAsync(moduleIds, ct);
        
        var distinctPermissions = modulePermissions
            .GroupBy(mp => mp.PermissionKey)
            .Select(g => g.First())
            .ToList();

        var distinctPermissionKeys = distinctPermissions.Select(mp => mp.PermissionKey);

        await _tenantDbSeeder.UpdateTenantPermissionsAsync(
            migrationResult.ConnectionString,
            distinctPermissionKeys,
            plan.PlanName,
            ct);
    }
}
