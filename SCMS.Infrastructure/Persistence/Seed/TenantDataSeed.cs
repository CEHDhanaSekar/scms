using scms.Domain.Entities.Tenant.Common;

public static class TenantDataSeed
{
    // ── Master Values GUIDs ───────────────────────────────────────────────────

    // EMPLOYEE_TYPE
    public static readonly Guid MasterValueDoctorId = Guid.Parse("E5555555-0000-0000-0000-000000000001");
    public static readonly Guid MasterValueNurseId = Guid.Parse("E5555555-0000-0000-0000-000000000002");
    public static readonly Guid MasterValueReceptionistId = Guid.Parse("E5555555-0000-0000-0000-000000000003");
    public static readonly Guid MasterValueAdminId = Guid.Parse("E5555555-0000-0000-0000-000000000004");
    public static readonly Guid MasterValueOtherId = Guid.Parse("E5555555-0000-0000-0000-000000000005");

    // VISIT_MODE
    public static readonly Guid MasterValueVisitModeWalkInId = Guid.Parse("E5555555-0002-0000-0000-000000000001");
    public static readonly Guid MasterValueVisitModeScheduledId = Guid.Parse("E5555555-0002-0000-0000-000000000002");

    // GENDER
    public static readonly Guid MasterValueGenderMaleId = Guid.Parse("E5555555-0003-0000-0000-000000000001");
    public static readonly Guid MasterValueGenderFemaleId = Guid.Parse("E5555555-0003-0000-0000-000000000002");
    public static readonly Guid MasterValueGenderOtherId = Guid.Parse("E5555555-0003-0000-0000-000000000003");

    // BLOOD_GROUP
    public static readonly Guid MasterValueBloodGroupAPosId = Guid.Parse("E5555555-0004-0000-0000-000000000001");
    public static readonly Guid MasterValueBloodGroupANegId = Guid.Parse("E5555555-0004-0000-0000-000000000002");
    public static readonly Guid MasterValueBloodGroupBPosId = Guid.Parse("E5555555-0004-0000-0000-000000000003");
    public static readonly Guid MasterValueBloodGroupBNegId = Guid.Parse("E5555555-0004-0000-0000-000000000004");
    public static readonly Guid MasterValueBloodGroupOPosId = Guid.Parse("E5555555-0004-0000-0000-000000000005");
    public static readonly Guid MasterValueBloodGroupONegId = Guid.Parse("E5555555-0004-0000-0000-000000000006");
    public static readonly Guid MasterValueBloodGroupABPosId = Guid.Parse("E5555555-0004-0000-0000-000000000007");
    public static readonly Guid MasterValueBloodGroupABNegId = Guid.Parse("E5555555-0004-0000-0000-000000000008");

    // SALUTATION
    public static readonly Guid MasterValueSalutationMrId = Guid.Parse("E5555555-0005-0000-0000-000000000001");
    public static readonly Guid MasterValueSalutationMrsId = Guid.Parse("E5555555-0005-0000-0000-000000000002");
    public static readonly Guid MasterValueSalutationMsId = Guid.Parse("E5555555-0005-0000-0000-000000000003");
    public static readonly Guid MasterValueSalutationDrId = Guid.Parse("E5555555-0005-0000-0000-000000000004");
    public static readonly Guid MasterValueSalutationMasterId = Guid.Parse("E5555555-0005-0000-0000-000000000005");

    // RELATIONSHIP_TYPE
    public static readonly Guid MasterValueRelSpouseId = Guid.Parse("E5555555-0006-0000-0000-000000000001");
    public static readonly Guid MasterValueRelParentId = Guid.Parse("E5555555-0006-0000-0000-000000000002");
    public static readonly Guid MasterValueRelSiblingId = Guid.Parse("E5555555-0006-0000-0000-000000000003");
    public static readonly Guid MasterValueRelChildId = Guid.Parse("E5555555-0006-0000-0000-000000000004");
    public static readonly Guid MasterValueRelFriendId = Guid.Parse("E5555555-0006-0000-0000-000000000005");
    public static readonly Guid MasterValueRelOtherId = Guid.Parse("E5555555-0006-0000-0000-000000000006");


    // ── Master Values Collections ─────────────────────────────────────────────

    public static MasterValues[] EmployeeTypes => new[]
    {
        new MasterValues { Id = MasterValueDoctorId, Type = "EMPLOYEE_TYPE", Key = "doctor", DisplayName = "Doctor", IsActive = true, Description = "Doctor" },
        new MasterValues { Id = MasterValueNurseId, Type = "EMPLOYEE_TYPE", Key = "nurse", DisplayName = "Nurse", IsActive = true, Description = "Nurse" },
        new MasterValues { Id = MasterValueReceptionistId, Type = "EMPLOYEE_TYPE", Key = "receptionist", DisplayName = "Receptionist", IsActive = true, Description = "Receptionist" },
        new MasterValues { Id = MasterValueAdminId, Type = "EMPLOYEE_TYPE", Key = "admin", DisplayName = "Admin", IsActive = true, Description = "Administrator" },
        new MasterValues { Id = MasterValueOtherId, Type = "EMPLOYEE_TYPE", Key = "other", DisplayName = "Other", IsActive = true, Description = "Other" }
    };

    public static MasterValues[] VisitModes => new[]
    {
        new MasterValues { Id = MasterValueVisitModeWalkInId, Type = "VISIT_MODE", Key = "walk_in", DisplayName = "Walk In", IsActive = true, Description = "Walk In" },
        new MasterValues { Id = MasterValueVisitModeScheduledId, Type = "VISIT_MODE", Key = "scheduled", DisplayName = "Scheduled", IsActive = true, Description = "Scheduled" }
    };

    public static MasterValues[] Genders => new[]
    {
        new MasterValues { Id = MasterValueGenderMaleId, Type = "GENDER", Key = "male", DisplayName = "Male", IsActive = true, Description = "Male" },
        new MasterValues { Id = MasterValueGenderFemaleId, Type = "GENDER", Key = "female", DisplayName = "Female", IsActive = true, Description = "Female" },
        new MasterValues { Id = MasterValueGenderOtherId, Type = "GENDER", Key = "other", DisplayName = "Other", IsActive = true, Description = "Other" }
    };

    public static MasterValues[] BloodGroups => new[]
    {
        new MasterValues { Id = MasterValueBloodGroupAPosId, Type = "BLOOD_GROUP", Key = "a_pos", DisplayName = "A+", IsActive = true, Description = "A Positive" },
        new MasterValues { Id = MasterValueBloodGroupANegId, Type = "BLOOD_GROUP", Key = "a_neg", DisplayName = "A-", IsActive = true, Description = "A Negative" },
        new MasterValues { Id = MasterValueBloodGroupBPosId, Type = "BLOOD_GROUP", Key = "b_pos", DisplayName = "B+", IsActive = true, Description = "B Positive" },
        new MasterValues { Id = MasterValueBloodGroupBNegId, Type = "BLOOD_GROUP", Key = "b_neg", DisplayName = "B-", IsActive = true, Description = "B Negative" },
        new MasterValues { Id = MasterValueBloodGroupOPosId, Type = "BLOOD_GROUP", Key = "o_pos", DisplayName = "O+", IsActive = true, Description = "O Positive" },
        new MasterValues { Id = MasterValueBloodGroupONegId, Type = "BLOOD_GROUP", Key = "o_neg", DisplayName = "O-", IsActive = true, Description = "O Negative" },
        new MasterValues { Id = MasterValueBloodGroupABPosId, Type = "BLOOD_GROUP", Key = "ab_pos", DisplayName = "AB+", IsActive = true, Description = "AB Positive" },
        new MasterValues { Id = MasterValueBloodGroupABNegId, Type = "BLOOD_GROUP", Key = "ab_neg", DisplayName = "AB-", IsActive = true, Description = "AB Negative" }
    };

    public static MasterValues[] Salutations => new[]
    {
        new MasterValues { Id = MasterValueSalutationMrId, Type = "SALUTATION", Key = "mr", DisplayName = "Mr", IsActive = true, Description = "Mr" },
        new MasterValues { Id = MasterValueSalutationMrsId, Type = "SALUTATION", Key = "mrs", DisplayName = "Mrs", IsActive = true, Description = "Mrs" },
        new MasterValues { Id = MasterValueSalutationMsId, Type = "SALUTATION", Key = "ms", DisplayName = "Ms", IsActive = true, Description = "Ms" },
        new MasterValues { Id = MasterValueSalutationDrId, Type = "SALUTATION", Key = "dr", DisplayName = "Dr", IsActive = true, Description = "Doctor" },
        new MasterValues { Id = MasterValueSalutationMasterId, Type = "SALUTATION", Key = "master", DisplayName = "Master", IsActive = true, Description = "Master" }
    };

    public static MasterValues[] RelationshipTypes => new[]
    {
        new MasterValues { Id = MasterValueRelSpouseId, Type = "RELATIONSHIP_TYPE", Key = "spouse", DisplayName = "Spouse", IsActive = true, Description = "Spouse" },
        new MasterValues { Id = MasterValueRelParentId, Type = "RELATIONSHIP_TYPE", Key = "parent", DisplayName = "Parent", IsActive = true, Description = "Parent" },
        new MasterValues { Id = MasterValueRelSiblingId, Type = "RELATIONSHIP_TYPE", Key = "sibling", DisplayName = "Sibling", IsActive = true, Description = "Sibling" },
        new MasterValues { Id = MasterValueRelChildId, Type = "RELATIONSHIP_TYPE", Key = "child", DisplayName = "Child", IsActive = true, Description = "Child" },
        new MasterValues { Id = MasterValueRelFriendId, Type = "RELATIONSHIP_TYPE", Key = "friend", DisplayName = "Friend", IsActive = true, Description = "Friend" },
        new MasterValues { Id = MasterValueRelOtherId, Type = "RELATIONSHIP_TYPE", Key = "other", DisplayName = "Other", IsActive = true, Description = "Other" }
    };

    public static MasterValues[] AllMasterValues =>
        EmployeeTypes
            .Concat(VisitModes)
            .Concat(Genders)
            .Concat(BloodGroups)
            .Concat(Salutations)
            .Concat(RelationshipTypes)
            .ToArray();
}