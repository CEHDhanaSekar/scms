using scms.Application.Dtos.Tenant;

namespace scms.Application.Interfaces.Tenant;

public interface IMasterValuesService
{
    Task<List<MasterValuesDto>> GetAllAsync(CancellationToken ct = default);
    Task<List<MasterValuesDto>> GetByTypeAsync(string type, CancellationToken ct = default);
    Task<MasterValuesDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
}
