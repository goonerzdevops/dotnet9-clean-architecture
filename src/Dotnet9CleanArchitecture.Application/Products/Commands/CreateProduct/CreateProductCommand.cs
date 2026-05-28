using MediatR;

namespace Dotnet9CleanArchitecture.Application.Products.Commands.CreateProduct;

public record CreateProductCommand(
    string Name,
    decimal Price,
    int Stock,
    string? Description,
    string? Category
) : IRequest<Guid>;
