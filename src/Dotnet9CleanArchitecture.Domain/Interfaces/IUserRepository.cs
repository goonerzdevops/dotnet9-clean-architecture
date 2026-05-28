
using Dotnet9CleanArchitecture.Domain.Entities;

namespace Dotnet9CleanArchitecture.Domain.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<User?> GetByUserNameAsync(string userName, CancellationToken cancellationToken = default);
    Task<User?> GetByRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<User>> GetByRoleNameAsync(string roleName, CancellationToken cancellationToken = default);
    Task<bool> IsEmailInUseAsync(string email, CancellationToken cancellationToken = default);
    Task<bool> IsUserNameInUseAsync(string userName, CancellationToken cancellationToken = default);
}
