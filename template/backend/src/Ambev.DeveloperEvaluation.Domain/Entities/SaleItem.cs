using System.ComponentModel.DataAnnotations.Schema;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;
using MediatR;
using Microsoft.Extensions.DependencyModel;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents one item in a sale.
    /// Stores product information, quantity, discount, and other item-related data.
    /// </summary>
    public class SaleItem: BaseEntity
    {
        [ForeignKey(nameof(Sale))]
        public Guid SaleId { get; set; }

        [ForeignKey(nameof(Product))]
        public required Guid ProductId { get; set; }

        public required int Quantity { get; set; }

        public required decimal Price { get; set; }

        public decimal Discount { get; set; } = 0;

        public decimal TotalItemPrice { get; set; }
        public bool Cancelled { get; set; }

        public ValidationResultDetail Validate()
        {
            var validator = new SaleItemValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
            };
        }

    }
}