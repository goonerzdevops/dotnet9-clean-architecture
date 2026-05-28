using Dotnet9CleanArchitecture.Application.DTOs;
using Dotnet9CleanArchitecture.Domain.Interfaces;
using Mapster;
using MediatR;

namespace Dotnet9CleanArchitecture.Application.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IReadOnlyList<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetAllProductsQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<IReadOnlyList<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        return products.Adapt<IReadOnlyList<ProductDto>>();
    }
}
