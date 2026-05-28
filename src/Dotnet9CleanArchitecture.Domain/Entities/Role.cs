using Dotnet9CleanArchitecture.Domain.Common;

namespace Dotnet9CleanArchitecture.Domain.Entities;

public class Role : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public bool IsActive { get; private set; } = true;

    private readonly List<User> _users = new();
    public IReadOnlyCollection<User> Users => _users.AsReadOnly();

    private Role() { } // EF Core

    public static Role Create(string name, string? description = null)
    {
        var role = new Role
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            IsActive = true
        };

        return role;
    }

    public void Update(string name, string? description)
    {
        Name = name;
        Description = description;
    }

    public void Deactivate() => IsActive = false;
    public void Activate() => IsActive = true;
}
