namespace Dotnet9CleanArchitecture.Application.DTOs;

public record ProductDto(
    Guid Id,
    string Name,
    string? Description,
    decimal Price,
    int Stock,
    string? Category,
    bool IsActive,
    DateTime CreatedAt
);
