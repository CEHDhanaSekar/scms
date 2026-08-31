using AutoMapper;
using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;
using scms.Domain.Entities.Tenant;

namespace scms.Application.Services.Tenant;

public class SpecializationService : ISpecializationService
{
    private readonly ISpecializationRepository _specializationRepository;
    private readonly IMapper _mapper;

    public SpecializationService(ISpecializationRepository specializationRepository, IMapper mapper)
    {
        _specializationRepository = specializationRepository;
        _mapper = mapper;
    }

    public async Task<SpecializationDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var specialization = await _specializationRepository.GetByIdAsync(id, ct);
        if (specialization == null) return null;
        return _mapper.Map<SpecializationDto>(specialization);
    }

    public async Task<List<SpecializationDto>> GetAllAsync(CancellationToken ct = default)
    {
        var specializations = await _specializationRepository.GetAllAsync(ct);
        return _mapper.Map<List<SpecializationDto>>(specializations);
    }

    public async Task<SpecializationDto> CreateAsync(CreateSpecializationDto dto, string createdBy, CancellationToken ct = default)
    {
        var specialization = _mapper.Map<Specialization>(dto);
        specialization.CreatedAt = DateTime.UtcNow;
        specialization.CreatedBy = createdBy;

        await _specializationRepository.AddAsync(specialization, ct);
        await _specializationRepository.SaveChangesAsync(ct);

        return _mapper.Map<SpecializationDto>(specialization);
    }

    public async Task<SpecializationDto> UpdateAsync(UpdateSpecializationDto dto, string updatedBy, CancellationToken ct = default)
    {
        var specialization = await _specializationRepository.GetByIdAsync(dto.Id, ct);
        if (specialization == null) throw new KeyNotFoundException("Specialization not found");

        _mapper.Map(dto, specialization);
        specialization.UpdatedAt = DateTime.UtcNow;
        specialization.UpdatedBy = updatedBy;

        await _specializationRepository.UpdateAsync(specialization, ct);
        await _specializationRepository.SaveChangesAsync(ct);

        return _mapper.Map<SpecializationDto>(specialization);
    }

    public async Task<bool> DeleteAsync(Guid id, string deletedBy, CancellationToken ct = default)
    {
        var specialization = await _specializationRepository.GetByIdAsync(id, ct);
        if (specialization == null) return false;

        specialization.IsDeleted = true;
        specialization.DeletedAt = DateTime.UtcNow;
        specialization.DeletedBy = deletedBy;

        await _specializationRepository.DeleteAsync(specialization, ct);
        await _specializationRepository.SaveChangesAsync(ct);
        return true;
    }
}
