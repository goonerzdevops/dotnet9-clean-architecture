using MediatR;

namespace Dotnet9CleanArchitecture.Application.Products.Commands.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest;
