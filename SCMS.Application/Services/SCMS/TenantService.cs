using AutoMapper;
using scms.Application.DTOs;
using scms.Application.Interfaces;
using SCMS.Shared.Exceptions;

namespace scms.Application.Services.SCMS;

public interface ITenantService
{
    Task<IEnumerable<TenantDto>> GetAllTenantsAsync(CancellationToken ct = default);
    Task<TenantDto?> GetTenantByIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> UpdateTenantAsync(Guid id, UpdateTenantDto dto, CancellationToken ct = default);
    Task<bool> DeleteTenantAsync(Guid id, CancellationToken ct = default);
}

public class TenantService(ITenantRepository repository, IMapper mapper) : ITenantService
{
    public async Task<IEnumerable<TenantDto>> GetAllTenantsAsync(CancellationToken ct = default)
    {
        var entities = await repository.GetAllAsync(ct);
        return mapper.Map<IEnumerable<TenantDto>>(entities);
    }

    public async Task<TenantDto?> GetTenantByIdAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await repository.GetByIdAsync(id, ct);
        if (entity == null) throw new NotFoundException("Tenant not found");
        return mapper.Map<TenantDto>(entity);
    }

    public async Task<bool> UpdateTenantAsync(Guid id, UpdateTenantDto dto, CancellationToken ct = default)
    {
        var entity = await repository.GetByIdAsync(id, ct);
        if (entity == null) return false;

        mapper.Map(dto, entity);

        await repository.UpdateAsync(entity, ct);
        return true;
    }

    public async Task<bool> DeleteTenantAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await repository.GetByIdAsync(id, ct);
        if (entity == null) return false;

        await repository.DeleteAsync(id, ct);
        return true;
    }
}
