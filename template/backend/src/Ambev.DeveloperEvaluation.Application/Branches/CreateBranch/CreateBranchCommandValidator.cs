using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch
{
    public class CreateBranchCommandValidator: AbstractValidator<CreateBranchCommand>
    {
        public CreateBranchCommandValidator()
        {
            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Branch name cannot be empty.")
            .MaximumLength(100).WithMessage("Branch name cannot exceed 100 characters.");
        }
    }
}