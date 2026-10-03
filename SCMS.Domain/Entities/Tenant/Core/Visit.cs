using scms.Domain.Entities.Tenant.Common;
using scms.Domain.Entities.Tenant.Master;
using SCMS.Shared.Entities;

namespace scms.Domain.Entities.Tenant.Core
{
    public class Visit : AuditableEntity
    {
        public string VisitCode { get; set; } = string.Empty;     // auto-generated, e.g. VIS-0001

        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;

        public Guid VisitModeId { get; set; }                     // MasterValues: WALK_IN / SCHEDULED
        public MasterValues VisitMode { get; set; } = null!;

        public DateTime? ScheduledDateTime { get; set; }           // required only when mode = SCHEDULED

        public Guid DepartmentId { get; set; }
        public Department Department { get; set; } = null!;

        public Guid? PreferredDoctorId { get; set; }
        public Employee? PreferredDoctor { get; set; }

        public Guid? AssignedDoctorId { get; set; }
        public Employee? AssignedDoctor { get; set; }

        public string Reason { get; set; } = string.Empty;

        public DateTime? CheckInTime { get; set; }
        public DateTime? ConsultationStartTime { get; set; }

        public VisitStatus Status { get; set; } = VisitStatus.Waiting;   // hardcoded enum — transitions guarded in code
        public string? CancellationReason { get; set; }
    }

    public enum VisitStatus
    {
        Scheduled,
        Waiting,
        InConsultation,
        Completed,
        Admitted,
        Cancelled,
        NoShow
    }
}
