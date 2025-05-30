﻿using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class SaleTestData
    {
        public static Sale GenerateValidSale()
        {
            return new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = $"SALE-{Guid.NewGuid().ToString()[..8]}",
                SaleDate = DateTime.UtcNow,
                UserId = Guid.NewGuid(),
                BranchId = Guid.NewGuid(),
                Items = new List<SaleItem>
                {
                    SaleItemTestData.GenerateValidSaleItem(quantity: 5, discount: 0.10m)
                }
            };
        }
    }
}
