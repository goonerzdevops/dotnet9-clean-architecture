namespace Dotnet9CleanArchitecture.Application.Interfaces;

public interface ICurrentUser
{
    string? UserId { get; }
    string? UserName { get; }
}
