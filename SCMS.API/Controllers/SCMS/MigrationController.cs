using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using scms.Application.Interfaces;
using scms.Shared.Models;

namespace scms.API.Controllers;

[Route("api/owner/v1/[controller]")]
[ApiController]
[Authorize(Policy = "OwnerOnly")]
public class MigrationController(ITenantMigrator migrator) : ControllerBase
{
    /// <summary>
    /// Applies any pending EF Core migrations for the given tenant's database.
    /// If all migrations are already applied, the operation is a no-op and
    /// still returns 200 with the current applied-migration list.
    /// </summary>
    /// <param name="tenantCode">The tenant code (e.g. "acme", "ACME", "acme-corp").</param>
    /// <param name="ct">Cancellation token injected by the framework.</param>
    [HttpPost("{tenantCode}/migrate")]
    public async Task<ActionResult<ApiResponse<MigrationResultDto>>> Migrate(
        string tenantCode,
        CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(tenantCode))
        {
            return BadRequest(new ApiResponse<MigrationResultDto>
            {
                Success = false,
                StatusCode = 400,
                Message = "Tenant code must not be empty."
            });
        }

        var result = await migrator.MigrateAsync(tenantCode, ct);

        var dto = new MigrationResultDto(
            result.TenantCode,
            result.AppliedMigrations,
            result.PendingMigrations,
            result.Error?.Message);

        if (!result.Success)
        {
            return StatusCode(500, new ApiResponse<MigrationResultDto>
            {
                Success = false,
                StatusCode = 500,
                Message = result.Error?.Message ?? "Migration failed for an unknown reason.",
                Data = dto
            });
        }

        var wasAlreadyUpToDate = result.PendingMigrations.Count == 0;

        return Ok(new ApiResponse<MigrationResultDto>
        {
            Success = true,
            StatusCode = 200,
            Message = wasAlreadyUpToDate
                ? $"Tenant '{result.TenantCode}' is already up to date. No migrations were applied."
                : $"Successfully applied {result.PendingMigrations.Count} migration(s) to tenant '{result.TenantCode}'.",
            Data = dto
        });
    }
}

/// <summary>
/// Response payload returned by the migration endpoint.
/// </summary>
/// <param name="TenantCode">Normalised tenant code that was migrated.</param>
/// <param name="AppliedMigrations">All migrations that now exist in the history table.</param>
/// <param name="PendingMigrations">Migrations that were pending before this run (empty when already up to date).</param>
/// <param name="ErrorMessage">Populated only when the migration attempt failed.</param>
public sealed record MigrationResultDto(
    string TenantCode,
    IReadOnlyList<string> AppliedMigrations,
    IReadOnlyList<string> PendingMigrations,
    string? ErrorMessage);
