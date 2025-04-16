using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.DeleteBranch
{
    public class DeleteBranchHandler : IRequestHandler<DeleteBranchCommand, DeleteBranchResult>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMediator _mediator;

        public DeleteBranchHandler(IBranchRepository branchRepository, IMediator mediator)
        {
            _branchRepository = branchRepository;
            _mediator = mediator;
        }

        public async Task<DeleteBranchResult> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteBranchCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var branchDeleted = await _branchRepository.DeleteAsync(request.Id, cancellationToken);
            if (!branchDeleted)
                throw new KeyNotFoundException($"Branch with id {request.Id} not found.");
            
            //TODO: publicar evento

            return new DeleteBranchResult { Success = true };
        }
    }

}
