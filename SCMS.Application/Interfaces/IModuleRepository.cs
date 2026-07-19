namespace scms.Application.Interfaces;

public interface IModuleRepository
{
    Task<IEnumerable<Module>> GetAllAsync();
    Task<Module?> GetByIdAsync(Guid id);
    Task<Module> AddAsync(Module module);
    Task UpdateAsync(Module module);
    Task DeleteAsync(Guid id);
}
