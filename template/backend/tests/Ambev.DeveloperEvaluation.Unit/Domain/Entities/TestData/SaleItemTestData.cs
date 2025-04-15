using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class SaleItemTestData
    {
        public static SaleItem GenerateValidSaleItem(int quantity = 1, decimal discount = 0)
        {
            return new SaleItem
            {
                Id = Guid.NewGuid(),
                ProductId = Guid.NewGuid(),
                SaleId = Guid.NewGuid(),
                Price = 10.0m,
                Quantity = quantity,
                Discount = discount,
                TotalItemPrice = 0,
                Cancelled = false,
            };
        }
    }
}
