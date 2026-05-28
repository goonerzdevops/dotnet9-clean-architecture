using Dotnet9CleanArchitecture.Domain.Common;

namespace Dotnet9CleanArchitecture.Domain.Entities;

public class User : AuditableEntity
{
    public string Email { get; private set; } = string.Empty;
    public string UserName { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public bool IsEmailConfirmed { get; private set; }
    public string? RefreshToken { get; private set; }
    public DateTime? RefreshTokenExpiryTime { get; private set; }
    public bool IsActive { get; private set; } = true;

    private readonly List<Role> _roles = new();
    public IReadOnlyCollection<Role> Roles => _roles.AsReadOnly();

    private User() { } // EF Core

    public static User Create(
        string email,
        string userName,
        string passwordHash,
        string firstName,
        string lastName)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = email.ToLowerInvariant(),
            UserName = userName,
            PasswordHash = passwordHash,
            FirstName = firstName,
            LastName = lastName,
            IsEmailConfirmed = false,
            IsActive = true
        };

        return user;
    }

    public void SetPassword(string passwordHash)
    {
        PasswordHash = passwordHash;
    }

    public void UpdateProfile(string firstName, string lastName, string? userName = null)
    {
        FirstName = firstName;
        LastName = lastName;
        if (!string.IsNullOrWhiteSpace(userName))
            UserName = userName;
    }

    public void ConfirmEmail() => IsEmailConfirmed = true;

    public void SetRefreshToken(string refreshToken, DateTime expiryTime)
    {
        RefreshToken = refreshToken;
        RefreshTokenExpiryTime = expiryTime;
    }

    public void RevokeRefreshToken()
    {
        RefreshToken = null;
        RefreshTokenExpiryTime = null;
    }

    public void Deactivate()
    {
        IsActive = false;
        RevokeRefreshToken();
    }

    public void Activate() => IsActive = true;

    public void AddRole(Role role)
    {
        if (!_roles.Contains(role))
            _roles.Add(role);
    }

    public void RemoveRole(Role role) => _roles.Remove(role);

    public bool HasRole(string roleName) =>
        _roles.Any(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));
}
