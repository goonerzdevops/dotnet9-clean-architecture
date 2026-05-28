using Dotnet9CleanArchitecture.Application.Products.Commands.CreateProduct;
using FluentValidation.TestHelper;
using Xunit;

namespace Dotnet9CleanArchitecture.Application.Tests;

public class CreateProductCommandValidatorTests
{
    private readonly CreateProductCommandValidator _validator = new();

    [Fact]
    public void Should_Have_Error_When_Name_Is_Empty()
    {
        var command = new CreateProductCommand("", 10m, 5, null, null);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }

    [Fact]
    public void Should_Have_Error_When_Price_Is_Zero()
    {
        var command = new CreateProductCommand("Test", 0m, 5, null, null);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Price);
    }

    [Fact]
    public void Should_Have_Error_When_Stock_Is_Negative()
    {
        var command = new CreateProductCommand("Test", 10m, -1, null, null);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Stock);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid()
    {
        var command = new CreateProductCommand("Test Product", 99.99m, 10, "Desc", "Cat");
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
