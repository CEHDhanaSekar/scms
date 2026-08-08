using AutoMapper;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;
using scms.Domain.Entities.Tenant;

namespace scms.Application.Services.Tenant;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<EmployeeDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(id, ct);
        if (employee == null) return null;
        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<List<EmployeeDto>> GetAllAsync(CancellationToken ct = default)
    {
        var employees = await _employeeRepository.GetAllAsync(ct);
        return _mapper.Map<List<EmployeeDto>>(employees);
    }

    public async Task<EmployeeDto> CreateAsync(CreateEmployeeDto dto, string createdBy, CancellationToken ct = default)
    {
        var employee = _mapper.Map<Employee>(dto);
        employee.CreatedAt = DateTime.UtcNow;
        employee.CreatedBy = createdBy;

        await _employeeRepository.AddAsync(employee, ct);
        await _employeeRepository.SaveChangesAsync(ct);

        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<EmployeeDto> UpdateAsync(UpdateEmployeeDto dto, string updatedBy, CancellationToken ct = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(dto.Id, ct);
        if (employee == null) throw new KeyNotFoundException("Employee not found");

        _mapper.Map(dto, employee);
        employee.UpdatedAt = DateTime.UtcNow;
        employee.UpdatedBy = updatedBy;

        await _employeeRepository.UpdateAsync(employee, ct);
        await _employeeRepository.SaveChangesAsync(ct);

        return _mapper.Map<EmployeeDto>(employee);
    }

    public async Task<bool> DeleteAsync(Guid id, string deletedBy, CancellationToken ct = default)
    {
        var employee = await _employeeRepository.GetByIdAsync(id, ct);
        if (employee == null) return false;

        employee.IsDeleted = true;
        employee.DeletedAt = DateTime.UtcNow;
        employee.DeletedBy = deletedBy;

        await _employeeRepository.DeleteAsync(employee, ct);
        await _employeeRepository.SaveChangesAsync(ct);
        return true;
    }
}
