using Dotnet9CleanArchitecture.Application.DTOs;
using MediatR;

namespace Dotnet9CleanArchitecture.Application.Products.Queries.GetProductById;

public record GetProductByIdQuery(Guid Id) : IRequest<ProductDto>;
