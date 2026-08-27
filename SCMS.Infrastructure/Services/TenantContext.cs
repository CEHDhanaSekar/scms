using System.Text.RegularExpressions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using scms.Application.Interfaces;
using scms.Shared.Models;
using SCMS.Shared.Exceptions;

namespace scms.Infrastructure.Services;

/// <summary>
/// Per-scoped implementation of <see cref="ITenantContext"/>.
/// Populated by <c>TenantResolverMiddleware</c> on every tenant request.
///
/// Resolution steps (inside <see cref="ResolveAsync"/>):
///   1. Validate tenant code format (same regex as DB check constraint)
///   2. Normalize: trim + lowercase
///   3. Cache check (key: "tenantctx::{code}")
///   4. On miss:
///      a. Read env var SCMS_{CODE}_CONNECTION → throw if absent
///      b. Verify tenant is active in DB via ITenantRepository
///      c. Determine IsOwner from config "OwnerTenantCode"
///      d. Cache (30 min absolute / 10 min sliding)
///   5. Populate properties
/// </summary>
public sealed class TenantContext : ITenantContext
{
    private static readonly Regex ValidTenantCode = new(
        @"^(?!-)(?!.*--)[A-Za-z0-9-]+(?<!-)$",
        RegexOptions.Compiled);

    private static readonly TimeSpan AbsoluteExpiry = TimeSpan.FromMinutes(30);
    private static readonly TimeSpan SlidingExpiry  = TimeSpan.FromMinutes(10);

    public string TenantCode      { get; private set; } = string.Empty;
    public string ConnectionString { get; private set; } = string.Empty;
    public bool   IsOwner          { get; private set; }

    private readonly IMemoryCache       _cache;
    private readonly IConfiguration     _config;
    private readonly ITenantRepository  _tenantRepo;
    private readonly ILogger<TenantContext> _logger;

    public TenantContext(
        IMemoryCache cache,
        IConfiguration config,
        ITenantRepository tenantRepo,
        ILogger<TenantContext> logger)
    {
        _cache      = cache;
        _config     = config;
        _tenantRepo = tenantRepo;
        _logger     = logger;
    }

    public async Task ResolveAsync(string tenantCode, CancellationToken ct = default)
    {
        // 1 — Format validation
        if (!ValidTenantCode.IsMatch(tenantCode))
            throw new BadRequestException($"Invalid tenant code format: '{tenantCode}'");

        // 2 — Normalize
        var normalized = tenantCode.Trim().ToLowerInvariant();

        // 3 — Cache check
        var cacheKey = $"tenantctx::{normalized}";
        if (_cache.TryGetValue(cacheKey, out CachedTenantState? state) && state is not null)
        {
            Apply(state);
            return;
        }

        // 4a — Read env var
        var envCode = normalized.ToUpperInvariant().Replace("-", "_");
        var envVar  = $"SCMS_{envCode}_CONNECTION";
        var connStr = Environment.GetEnvironmentVariable(envVar)
                   ?? _config[envVar];  // also check appsettings fallback

        if (string.IsNullOrWhiteSpace(connStr))
        {
            _logger.LogError(
                "Environment variable '{EnvVar}' is not set for tenant '{TenantCode}'.",
                envVar, normalized);
            throw new BadRequestException(
                $"Tenant '{normalized}' is not configured on this server.");
        }

        // 4b — Verify active in DB
        var exists = await _tenantRepo.ExistsAndActiveAsync(normalized, ct);
        if (!exists)
            throw new BadRequestException($"Tenant '{normalized}' not found or inactive.");

        // 4c — IsOwner
        var ownerCode = _config["OwnerTenantCode"] ?? "vktech";
        var isOwner   = normalized == ownerCode.ToLowerInvariant();

        // 4d — Cache
        state = new CachedTenantState(normalized, connStr, isOwner);
        _cache.Set(cacheKey, state, new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = AbsoluteExpiry,
            SlidingExpiration               = SlidingExpiry
        });

        // 5 — Populate
        Apply(state);
    }

    private void Apply(CachedTenantState state)
    {
        TenantCode       = state.TenantCode;
        ConnectionString = state.ConnectionString;
        IsOwner          = state.IsOwner;
    }

    /// <summary>Value type stored in the cache — avoids capturing the TenantContext itself.</summary>
    private sealed record CachedTenantState(
        string TenantCode,
        string ConnectionString,
        bool   IsOwner);
}
