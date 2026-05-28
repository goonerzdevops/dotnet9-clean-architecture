
using Dotnet9CleanArchitecture.Domain.Entities;
using Dotnet9CleanArchitecture.Domain.Interfaces;
using Dotnet9CleanArchitecture.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Dotnet9CleanArchitecture.Infrastructure.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Roles.FindAsync(new object[] { id }, cancellationToken);
    }

    public async Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Roles
            .AsNoTracking()
            .Where(r => r.IsActive)
            .OrderBy(r => r.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Role> AddAsync(Role entity, CancellationToken cancellationToken = default)
    {
        await _context.Roles.AddAsync(entity, cancellationToken);
        return entity;
    }

    public void Update(Role entity)
    {
        _context.Roles.Update(entity);
    }

    public void Delete(Role entity)
    {
        _context.Roles.Remove(entity);
    }

    public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        return await _context.Roles
            .FirstOrDefaultAsync(r => r.Name == name && r.IsActive, cancellationToken);
    }

    public async Task<IReadOnlyList<Role>> GetByNamesAsync(IEnumerable<string> names, CancellationToken cancellationToken = default)
    {
        return await _context.Roles
            .AsNoTracking()
            .Where(r => names.Contains(r.Name) && r.IsActive)
            .ToListAsync(cancellationToken);
    }
}
