using scms.Application.DTOs;

namespace scms.Application.Services.SCMS;

public interface ITenantOnboardingService
{
    Task<TenantOnboardingResult> OnboardAsync(CreateTenantDto dto, CancellationToken ct = default);
}

public sealed record TenantOnboardingResult(
    bool Success,
    Guid TenantId,
    string TenantCode,
    string? FailureReason = null,
    string? FailedAtStep = null);
