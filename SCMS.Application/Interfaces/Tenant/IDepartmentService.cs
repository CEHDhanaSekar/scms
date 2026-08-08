using scms.Application.Dtos.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface IDepartmentService
{
    Task<DepartmentDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<DepartmentDto>> GetAllAsync(CancellationToken ct = default);
    Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto, string createdBy, CancellationToken ct = default);
    Task<DepartmentDto> UpdateAsync(UpdateDepartmentDto dto, string updatedBy, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, string deletedBy, CancellationToken ct = default);
}
