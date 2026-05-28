using Dotnet9CleanArchitecture.Domain.Common;
using Dotnet9CleanArchitecture.Domain.Events;

namespace Dotnet9CleanArchitecture.Domain.Entities;

public class Product : AuditableEntity
{
    public string Name { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string? Category { get; private set; }
    public bool IsActive { get; private set; } = true;

    private Product() { } // EF Core

    public static Product Create(string name, decimal price, int stock, string? description = null, string? category = null)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = name,
            Price = price,
            Stock = stock,
            Description = description,
            Category = category,
            IsActive = true
        };

        product.AddDomainEvent(new ProductCreatedEvent(product));
        return product;
    }

    public void Update(string name, decimal price, string? description, string? category)
    {
        Name = name;
        Price = price;
        Description = description;
        Category = category;

        AddDomainEvent(new ProductUpdatedEvent(this));
    }

    public void AdjustStock(int quantity)
    {
        Stock += quantity;
        if (Stock < 0) Stock = 0;
    }

    public void Deactivate()
    {
        IsActive = false;
        AddDomainEvent(new ProductDeactivatedEvent(this));
    }

    private readonly List<BaseEvent> _domainEvents = new();
    public IReadOnlyCollection<BaseEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(BaseEvent domainEvent) => _domainEvents.Add(domainEvent);
    public void ClearDomainEvents() => _domainEvents.Clear();
}
