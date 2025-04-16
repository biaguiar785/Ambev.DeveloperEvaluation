using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Branches.CreateBranch
{
    public class CreateBranchHandler: IRequestHandler<CreateBranchCommand, CreateBranchResult>
    {
        private readonly IBranchRepository _branchRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateBranchHandler(IBranchRepository branchRepository, IMapper mapper, IMediator mediator)
        {
            _branchRepository = branchRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<CreateBranchResult> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateBranchCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
           
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var branch = _mapper.Map<Branch>(request);
            var createdBranch = await _branchRepository.CreateAsync(branch, cancellationToken);
            var result = _mapper.Map<CreateBranchResult>(createdBranch);

            return result;
        }
    }
}
