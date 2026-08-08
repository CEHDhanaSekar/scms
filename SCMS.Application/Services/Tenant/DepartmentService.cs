using AutoMapper;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;
using scms.Domain.Entities.Tenant;

namespace scms.Application.Services.Tenant;

public class DepartmentService : IDepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;
    private readonly IMapper _mapper;

    public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper)
    {
        _departmentRepository = departmentRepository;
        _mapper = mapper;
    }

    public async Task<DepartmentDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var department = await _departmentRepository.GetByIdAsync(id, ct);
        if (department == null) return null;
        return _mapper.Map<DepartmentDto>(department);
    }

    public async Task<List<DepartmentDto>> GetAllAsync(CancellationToken ct = default)
    {
        var departments = await _departmentRepository.GetAllAsync(ct);
        return _mapper.Map<List<DepartmentDto>>(departments);
    }

    public async Task<DepartmentDto> CreateAsync(CreateDepartmentDto dto, string createdBy, CancellationToken ct = default)
    {
        var department = _mapper.Map<Department>(dto);
        department.CreatedAt = DateTime.UtcNow;
        department.CreatedBy = createdBy;

        await _departmentRepository.AddAsync(department, ct);
        await _departmentRepository.SaveChangesAsync(ct);

        return _mapper.Map<DepartmentDto>(department);
    }

    public async Task<DepartmentDto> UpdateAsync(UpdateDepartmentDto dto, string updatedBy, CancellationToken ct = default)
    {
        var department = await _departmentRepository.GetByIdAsync(dto.Id, ct);
        if (department == null) throw new KeyNotFoundException("Department not found");

        _mapper.Map(dto, department);
        department.UpdatedAt = DateTime.UtcNow;
        department.UpdatedBy = updatedBy;

        await _departmentRepository.UpdateAsync(department, ct);
        await _departmentRepository.SaveChangesAsync(ct);

        return _mapper.Map<DepartmentDto>(department);
    }

    public async Task<bool> DeleteAsync(Guid id, string deletedBy, CancellationToken ct = default)
    {
        var department = await _departmentRepository.GetByIdAsync(id, ct);
        if (department == null) return false;

        department.IsDeleted = true;
        department.DeletedAt = DateTime.UtcNow;
        department.DeletedBy = deletedBy;

        await _departmentRepository.DeleteAsync(department, ct);
        await _departmentRepository.SaveChangesAsync(ct);
        return true;
    }
}
