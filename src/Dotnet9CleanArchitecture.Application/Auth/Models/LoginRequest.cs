
namespace Dotnet9CleanArchitecture.Application.Auth.Models;

public record LoginRequest(
    string Email,
    string Password
);
