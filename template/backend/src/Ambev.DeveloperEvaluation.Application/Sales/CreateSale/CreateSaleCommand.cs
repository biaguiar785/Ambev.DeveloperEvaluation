using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<CreateSaleResult>
    {
        public Guid BranchId { get; set; }
        public List<CreateSaleItemCommand> Items { get; set; } = new();

        public ValidationResultDetail Validate()
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = validationResult.IsValid,
                Errors = validationResult.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
