using Dotnet9CleanArchitecture.Domain.Common;
using Dotnet9CleanArchitecture.Domain.Entities;

namespace Dotnet9CleanArchitecture.Domain.Events;

public class ProductUpdatedEvent : BaseEvent
{
    public Product Product { get; }

    public ProductUpdatedEvent(Product product)
    {
        Product = product;
    }
}
