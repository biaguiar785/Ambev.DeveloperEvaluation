using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class ProductValidator: AbstractValidator<Product>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name)
            .NotEmpty()
            .WithMessage("Product name is required.")
            .MaximumLength(100)
            .WithMessage("Product name cannot exceed 100 characters.");

            RuleFor(p => p.Price)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Unit price cannot be negative.");
        }
    }
}
