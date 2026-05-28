
using Dotnet9CleanArchitecture.Domain.Entities;

namespace Dotnet9CleanArchitecture.Domain.Interfaces;

public interface IRoleRepository : IRepository<Role>
{
    Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Role>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default);
}
