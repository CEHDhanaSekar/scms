using scms.Application.Dtos.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface ISpecializationService
{
    Task<SpecializationDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<List<SpecializationDto>> GetAllAsync(CancellationToken ct = default);
    Task<SpecializationDto> CreateAsync(CreateSpecializationDto dto, string createdBy, CancellationToken ct = default);
    Task<SpecializationDto> UpdateAsync(UpdateSpecializationDto dto, string updatedBy, CancellationToken ct = default);
    Task<bool> DeleteAsync(Guid id, string deletedBy, CancellationToken ct = default);
}
