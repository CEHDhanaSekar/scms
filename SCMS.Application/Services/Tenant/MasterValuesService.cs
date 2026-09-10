using scms.Application.Dtos.Tenant;
using scms.Application.Interfaces.Tenant;

namespace scms.Application.Services.Tenant;

public class MasterValuesService : IMasterValuesService
{
    private readonly IMasterValuesRepository _masterValuesRepository;

    public MasterValuesService(IMasterValuesRepository masterValuesRepository)
    {
        _masterValuesRepository = masterValuesRepository;
    }

    public Task<List<MasterValuesDto>> GetAllAsync(CancellationToken ct = default)
        => _masterValuesRepository.GetAllAsync(ct);

    public Task<List<MasterValuesDto>> GetByTypeAsync(string type, CancellationToken ct = default)
        => _masterValuesRepository.GetByTypeAsync(type, ct);

    public Task<MasterValuesDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => _masterValuesRepository.GetByIdAsync(id, ct);
}
