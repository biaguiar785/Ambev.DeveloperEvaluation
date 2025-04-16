using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.Description));

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than 0.");
        }
    }
}
