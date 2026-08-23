SETX ASPNETCORE_ENVIRONMENT "Development"

SETX SCMS_DEFAULT_CONNECTION "Host=localhost;Port=5432;Database=scms;Username=postgres;Password=1618"

SETX SCMS_CLINICA_CONNECTION "Host=localhost;Port=5432;Database=scms_clinica;Username=postgres;Password=1618"
SETX SCMS_CLINICB_CONNECTION "Host=localhost;Port=5432;Database=scms_clinicb;Username=postgres;Password=1618"

REM ── Tenant design-time connection ────────────────────────────────────────────
REM Used ONLY by EF Core design-time tools (dotnet ef migrations add / update).
REM At runtime the connection is supplied by ITenantContext (multi-tenant middleware).
SETX TENANT_DESIGN_CONN "Host=localhost;Port=5432;Database=scms_tenant_model;Username=postgres;Password=1618"
