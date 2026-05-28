
namespace Dotnet9CleanArchitecture.Application.Auth.Models;

public record RegisterRequest(
    string Email,
    string UserName,
    string Password,
    string FirstName,
    string LastName
);
