using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandValidator: AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.BranchId)
                .NotEmpty()
                .WithMessage("Branch ID cannot be empty.");

            RuleFor(x => x.Items)
                .NotNull().WithMessage("Items list cannot be null.")
                .NotEmpty().WithMessage("At least one item is required to create a sale.");

            RuleForEach(x => x.Items)
                .ChildRules(item =>
                {
                    item.RuleFor(p => p.ProductId)
                        .NotEmpty()
                        .WithMessage("Product ID cannot be empty.");

                    item.RuleFor(p => p.Quantity)
                    .GreaterThan(0)
                    .WithMessage("Quantity must be greater than 0.")
                    .LessThanOrEqualTo(20)
                    .WithMessage("Quantity must be less than or equal to 20.");
                });

            RuleFor(c => c).Custom((command, context) =>
            {
                var groupedByProductId = command.Items.GroupBy(x => x.ProductId).ToList();

                foreach (var group in groupedByProductId)
                {
                    var quantityofEachProduct = group.Sum(x => x.Quantity);
                    if (quantityofEachProduct > 20)
                    {
                        context.AddFailure(
                            $"Product ID {group.Key}",
                            "Total quantity of each product cannot exceed 20.");
                    }
                }

            });
        }
    }
    
}
