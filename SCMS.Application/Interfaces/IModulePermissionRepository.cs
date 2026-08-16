namespace scms.Application.Interfaces;

public interface IModulePermissionRepository
{
    Task<IEnumerable<ModulePermission>> GetAllAsync();
    Task<IEnumerable<ModulePermission>> GetByModuleIdsAsync(IEnumerable<Guid> moduleIds, CancellationToken ct = default);
    Task<ModulePermission?> GetByIdAsync(Guid id);
    Task<ModulePermission> AddAsync(ModulePermission modulePermission);
    Task UpdateAsync(ModulePermission modulePermission);
    Task DeleteAsync(Guid id);
}
