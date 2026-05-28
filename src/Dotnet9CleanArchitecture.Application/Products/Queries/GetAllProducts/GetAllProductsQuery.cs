using Dotnet9CleanArchitecture.Application.DTOs;
using MediatR;

namespace Dotnet9CleanArchitecture.Application.Products.Queries.GetAllProducts;

public record GetAllProductsQuery : IRequest<IReadOnlyList<ProductDto>>;
