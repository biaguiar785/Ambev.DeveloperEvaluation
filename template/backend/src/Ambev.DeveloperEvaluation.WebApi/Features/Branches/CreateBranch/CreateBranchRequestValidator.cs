using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch
{
    public class CreateBranchRequestValidator : AbstractValidator<CreateBranchRequest>
    {
        public CreateBranchRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Branch name is required.")
                .MaximumLength(100).WithMessage("Branch name cannot exceed 100 characters.");

        }
    }
}
