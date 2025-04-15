using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities
{
    public class SaleTests
    {
        [Fact(DisplayName = "Should cancel a sale and update timestamp")]
        public void ShouldCancelSaleCorrectly()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();

            // Act
            sale.CancelSale();

            // Assert
            Assert.NotNull(sale.UpdatedAt);
            Assert.True(sale.Cancelled);
        }

        [Fact(DisplayName = "Should validate a sale correctly")]
        public void ShouldNotCancelSaleCorrectly()
        {
            // Arrange   
            var sale = SaleTestData.GenerateValidSale();

            // Act

            var result = sale.Validate();

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact(DisplayName = "Should not validate a sale with empty branch ID")]
        public void ShouldNotValidateSaleWithEmptyBranchId()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.BranchId = Guid.Empty;

            // Act
            var result = sale.Validate();

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.Detail.Contains("Branch ID is required."));
        }

        [Fact(DisplayName = "Should not validate a sale with empty items")]
        public void ShouldNotValidateSaleWithEmptyItems()
        {
            // Arrange
            var sale = SaleTestData.GenerateValidSale();
            sale.Items = new List<SaleItem>();

            // Act
            var result = sale.Validate();

            // Assert
            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.Detail.Contains("Sale must contain at least one item."));
        }
    }
}
