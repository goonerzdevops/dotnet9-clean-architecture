using Dotnet9CleanArchitecture.Domain.Common;
using Dotnet9CleanArchitecture.Domain.Entities;

namespace Dotnet9CleanArchitecture.Domain.Events;

public class ProductCreatedEvent : BaseEvent
{
    public Product Product { get; }

    public ProductCreatedEvent(Product product)
    {
        Product = product;
    }
}
