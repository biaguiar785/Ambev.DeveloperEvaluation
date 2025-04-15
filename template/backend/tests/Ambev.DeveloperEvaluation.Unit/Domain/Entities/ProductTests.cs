using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class ProductTests
    {
        [Fact(DisplayName = "Should mark product as inactive")]
        public void ShouldDeactivateProductCorrectly()
        {
            //Arrange
            var product = ProductTestData.GenerateValidProduct();
            product.IsActive = true;

            //Act
            product.DeactivateProduct();

            //Assert
            Assert.False(product.IsActive);
            Assert.NotNull(product.UpdatedAt);
            Assert.True(product.UpdatedAt > product.CreatedAt);
        }

        [Fact(DisplayName = "Should validate product correctly")]
        public void ShouldUpdateProductCorrectly()
        {
            //Arrange
            var product = ProductTestData.GenerateValidProduct();

            //Act
            var result = product.Validate();

            //Assert
            Assert.True(result.IsValid);
            Assert.Empty(result.Errors);
        }

        [Fact(DisplayName = "Should not validate product with empty name")]
        public void ShouldNotValidateProductWithEmptyName()
        {
            //Arrange
            var product = ProductTestData.GenerateValidProduct();
            product.Name = string.Empty;

            //Act
            var result = product.Validate();

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.Detail.Contains("Name of Product is required."));
        }

        [Fact(DisplayName = "Should not validate product with name too long")]
        public void ShouldNotValidateProductWithNameTooLong()
        {
            //Arrange
            var product = ProductTestData.GenerateValidProduct();
            product.Name = new string('a', 101);

            //Act
            var result = product.Validate();

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.Detail.Contains("Name of Product must be between 1 and 100 characters."));
        }

        [Fact(DisplayName = "Should not validate product with description too long")]
        public void ShouldNotValidateProductWithDescriptionTooLong()
        {
            //Arrange
            var product = ProductTestData.GenerateValidProduct();
            product.Description = new string('a', 501);

            //Act
            var result = product.Validate();

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.Detail.Contains("Description of Product must be between 0 and 500 characters."));
        }

        [Fact(DisplayName = "Should not validate product with negative price")]
        public void ShouldNotValidateProductWithNegativePrice()
        {
            //Arrange
            var product = ProductTestData.GenerateValidProduct();
            product.Price = -1;

            //Act
            var result = product.Validate();

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.Detail.Contains("Price of Product must be greater than or equal to 0."));
        }

        [Fact(DisplayName = "Should not validate product when is a invalid product")]
        public void ShouldNotValidateProductWhenIsInvalidProduct()
        {
            //Arrange
            var product = new Product
            {
                Name = string.Empty,
                Description = null,
                Price = -10,
                CreatedAt = DateTime.UtcNow,
            };

            //Act
            var result = product.Validate();

            //Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.Detail.Contains("Name of Product is required."));
            Assert.Contains(result.Errors, e => e.Detail.Contains("Price of Product must be greater than or equal to 0."));
        }
    }
}
