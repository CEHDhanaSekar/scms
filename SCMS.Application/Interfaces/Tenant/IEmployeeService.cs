using scms.Application.Dtos.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface IEmployeeService
{
    Task<EmployeeDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<EmployeeDto>> GetAllAsync(CancellationToken ct = default);
    Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto, string createdBy, CancellationToken ct = default);
    Task<EmployeeDto> UpdateAsync(UpdateEmployeeDto dto, string updatedBy, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, string deletedBy, CancellationToken ct = default);
}
