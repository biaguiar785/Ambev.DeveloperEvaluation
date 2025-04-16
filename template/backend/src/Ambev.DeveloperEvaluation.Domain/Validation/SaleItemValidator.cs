using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class SaleItemValidator: AbstractValidator<SaleItem>
    {
        public SaleItemValidator()
        {
            RuleFor(x => x.ProductId)
                .NotEmpty()
                .WithMessage("Product ID cannot be empty.");

            RuleFor(x => x.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than 0.")
                .LessThanOrEqualTo(20).WithMessage("Quantity must be less than or equal to 20.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price cannot be negative.");

            RuleFor(x => x.Discount)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Discount must be greater than or equal to 0.");

            When(i => i.Quantity < 4, () =>
            {
                RuleFor(x => x.Discount).Equal(0)
                .WithMessage("Discount must be 0 when quantity is less than 4.");
            });

            When(i => i.Quantity >= 4, () =>
            {
                RuleFor(x => x.Discount)
                    .Equal(0.1m)
                    .WithMessage("Discount must be 10% for quantities between 4 and 9");
            });

            When(i => i.Quantity >= 10, () =>
            {
                RuleFor(x => x.Discount)
                    .Equal(0.2m)
                    .WithMessage("Discount must be 20% for quantities between 4 and 9");
            });
        }
    }
}
