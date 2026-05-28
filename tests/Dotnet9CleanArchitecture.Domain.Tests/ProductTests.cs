using Dotnet9CleanArchitecture.Domain.Entities;
using Xunit;

namespace Dotnet9CleanArchitecture.Domain.Tests;

public class ProductTests
{
    [Fact]
    public void Create_WithValidParameters_ShouldCreateProduct()
    {
        // Arrange
        var name = "Test Product";
        var price = 99.99m;
        var stock = 10;

        // Act
        var product = Product.Create(name, price, stock, "Description", "Electronics");

        // Assert
        Assert.NotEqual(Guid.Empty, product.Id);
        Assert.Equal(name, product.Name);
        Assert.Equal(price, product.Price);
        Assert.Equal(stock, product.Stock);
        Assert.True(product.IsActive);
        Assert.Single(product.DomainEvents);
    }

    [Fact]
    public void Update_WithValidParameters_ShouldUpdateProduct()
    {
        // Arrange
        var product = Product.Create("Old Name", 10m, 5);
        product.ClearDomainEvents();

        // Act
        product.Update("New Name", 20m, "New Desc", "Category");

        // Assert
        Assert.Equal("New Name", product.Name);
        Assert.Equal(20m, product.Price);
        Assert.Single(product.DomainEvents);
    }

    [Fact]
    public void Deactivate_ShouldSetInactiveAndRaiseEvent()
    {
        // Arrange
        var product = Product.Create("Test", 10m, 5);
        product.ClearDomainEvents();

        // Act
        product.Deactivate();

        // Assert
        Assert.False(product.IsActive);
        Assert.Single(product.DomainEvents);
    }

    [Theory]
    [InlineData(10, 5, 15)]
    [InlineData(0, 3, 3)]
    [InlineData(5, -10, 0)] // Cannot go below 0
    public void AdjustStock_ShouldUpdateCorrectly(int initial, int adjustment, int expected)
    {
        var product = Product.Create("Test", 10m, initial);
        product.AdjustStock(adjustment);
        Assert.Equal(expected, product.Stock);
    }
}
