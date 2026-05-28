
using Dotnet9CleanArchitecture.Domain.Entities;

namespace Dotnet9CleanArchitecture.Application.Interfaces;

public interface ITokenService
{
    string GenerateAccessToken(User user, IEnumerable<string> roles);
    string GenerateRefreshToken();
    bool ValidateRefreshToken(string refreshToken, DateTime expiryTime);
}
