using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a product with name, description, price, status, and timestamps.
    /// </summary>
    public class Product: BaseEntity
    {
        /// <summary>
        /// The name of the product.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The description of the product.
        /// </summary>
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// The category of the product.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// The price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Indicates whether the product is active.
        /// </summary>
        public bool IsActive { get; set; }

        public ProductRating Rating { get; set; } = new ProductRating();

        /// <summary>
        /// The date and time when the product was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The date and time when the product was last updated.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Product"/> class.
        /// </summary>
        public Product()
        {
            CreatedAt = DateTime.UtcNow;
            IsActive = true;
        }

        /// <summary>
        /// Inactive a product.
        /// </summary>
        public void DeactivateProduct()
        {
            IsActive = false;
            UpdatedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Validates the current product instance using <see cref="ProductValidator"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="ValidationResultDetail"/> containing the validation result and any errors.
        /// </returns>
        public ValidationResultDetail Validate()
        {
            var validator = new ProductValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(e => (ValidationErrorDetail)e)
            };
        }

        public class ProductRating
        {
            /// <summary>
            /// Rate of the product.
            /// </summary>
            public double Rate { get; set; }

            /// <summary>
            /// Number of ratings.
            /// </summary>
            public int Count { get; set; }
        }

    }
}