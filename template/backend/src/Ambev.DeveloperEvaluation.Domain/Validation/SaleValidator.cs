using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Common.Validation
{
    public class SaleValidator: AbstractValidator<Sale>
    {
        public SaleValidator()
        {
            RuleFor(s => s.SaleNumber)
           .NotEmpty()
           .WithMessage("Sale number cannot be empty.");

            RuleFor(s => s.SaleDate)
                .NotEmpty()
                .WithMessage("Sale date is required.");

            RuleFor(s => s.TotalSale)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total sale cannot be negative.");

            RuleFor(s => s.Items)
                .NotNull()
                .NotEmpty()
                .WithMessage("Sale must contain at least one item.");
        }
    }
}
