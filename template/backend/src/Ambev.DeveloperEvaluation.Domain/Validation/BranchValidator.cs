using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation
{
    public class BranchValidator: AbstractValidator<Branch>
    {
        public BranchValidator()
        {
            RuleFor(b => b.Name)
           .NotEmpty()
           .WithMessage("Name of Branch is required.")
           .MaximumLength(100)
           .WithMessage("Name of branch cannot exceed 100 characters.");
        }
    }
}
