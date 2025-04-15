using System.ComponentModel.DataAnnotations.Schema;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a single item within a sale.
    /// Contains information about the product, quantity, discount, and item total.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        /// <summary>
        /// Identifier of the associated sale.
        /// </summary>
        [ForeignKey(nameof(Sale))]
        public Guid SaleId { get; set; }

        /// <summary>
        /// Identifier of the associated product.
        /// </summary>
        [ForeignKey(nameof(Product))]
        public required Guid ProductId { get; set; }

        /// <summary>
        /// Quantity of the product in the sale item.
        /// </summary>
        public required int Quantity { get; set; }

        /// <summary>
        /// Unit price of the product.
        /// </summary>
        public required decimal Price { get; set; }

        /// <summary>
        /// Discount applied to the item.
        /// </summary>
        public decimal Discount { get; set; } = 0;

        /// <summary>
        /// Total price of the item, including quantity and discount.
        /// </summary>
        public decimal TotalItemPrice { get; set; }

        /// <summary>
        /// Indicates whether the item was cancelled.
        /// </summary>
        public bool Cancelled { get; set; }

        /// <summary>
        /// Validates the current sale item using <see cref="SaleItemValidator"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing the validation result and any errors.
        /// </returns>
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