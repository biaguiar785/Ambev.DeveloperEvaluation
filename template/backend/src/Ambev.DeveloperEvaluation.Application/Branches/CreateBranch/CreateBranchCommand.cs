using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch
{
    public class CreateBranchCommand: IRequest<CreateBranchResult>
    {
        public string Name { get; set; } = string.Empty;

        public ValidationResultDetail Validate()
        {
            var validator = new CreateBranchCommandValidator();
            var validationResult = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = validationResult.IsValid,
                Errors = validationResult.Errors.Select(o => (ValidationErrorDetail)o)
            };

        }
    }
}
