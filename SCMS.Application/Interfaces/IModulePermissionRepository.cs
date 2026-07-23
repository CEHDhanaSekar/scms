namespace scms.Application.Interfaces;

public interface IModulePermissionRepository
{
    Task<IEnumerable<ModulePermission>> GetAllAsync();
    Task<ModulePermission?> GetByIdAsync(Guid id);
    Task<ModulePermission> AddAsync(ModulePermission modulePermission);
    Task UpdateAsync(ModulePermission modulePermission);
    Task DeleteAsync(Guid id);
}
