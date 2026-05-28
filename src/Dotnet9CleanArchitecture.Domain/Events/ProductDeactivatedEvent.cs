using Dotnet9CleanArchitecture.Domain.Common;
using Dotnet9CleanArchitecture.Domain.Entities;

namespace Dotnet9CleanArchitecture.Domain.Events;

public class ProductDeactivatedEvent : BaseEvent
{
    public Product Product { get; }

    public ProductDeactivatedEvent(Product product)
    {
        Product = product;
    }
}
