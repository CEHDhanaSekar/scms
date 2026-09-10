// SCMS.Infrastructure/Persistence/Seed/DataSeed.cs
using scms.Domain.Entities.SCMS;

public static class TenantDataSeed
{
    // ── Master Values (Tenant) ────────────────────────────────────────────────
    public static readonly Guid MasterValueDoctorId = Guid.Parse("E5555555-0000-0000-0000-000000000001");
    public static readonly Guid MasterValueNurseId = Guid.Parse("E5555555-0000-0000-0000-000000000002");
    public static readonly Guid MasterValueReceptionistId = Guid.Parse("E5555555-0000-0000-0000-000000000003");
    public static readonly Guid MasterValueAdminId = Guid.Parse("E5555555-0000-0000-0000-000000000004");
    public static readonly Guid MasterValueOtherId = Guid.Parse("E5555555-0000-0000-0000-000000000005");

    public static scms.Domain.Entities.Tenant.MasterValues[] EmployeeTypes => new[]
    {
        new scms.Domain.Entities.Tenant.MasterValues { Id = MasterValueDoctorId, Type = "EmployeeType", Key = "Doctor", DisplayName = "Doctor", IsActive = true, Description = "Doctor" },
        new scms.Domain.Entities.Tenant.MasterValues { Id = MasterValueNurseId, Type = "EmployeeType", Key = "Nurse", DisplayName = "Nurse", IsActive = true, Description = "Nurse" },
        new scms.Domain.Entities.Tenant.MasterValues { Id = MasterValueReceptionistId, Type = "EmployeeType", Key = "Receptionist", DisplayName = "Receptionist", IsActive = true, Description = "Receptionist" },
        new scms.Domain.Entities.Tenant.MasterValues { Id = MasterValueAdminId, Type = "EmployeeType", Key = "Admin", DisplayName = "Admin", IsActive = true, Description = "Administrator" },
        new scms.Domain.Entities.Tenant.MasterValues { Id = MasterValueOtherId, Type = "EmployeeType", Key = "Other", DisplayName = "Other", IsActive = true, Description = "Other" }
    };
}