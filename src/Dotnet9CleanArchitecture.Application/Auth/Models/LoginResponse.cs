
namespace Dotnet9CleanArchitecture.Application.Auth.Models;

public record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresAt,
    string Email,
    string UserName,
    IEnumerable<string> Roles
);
