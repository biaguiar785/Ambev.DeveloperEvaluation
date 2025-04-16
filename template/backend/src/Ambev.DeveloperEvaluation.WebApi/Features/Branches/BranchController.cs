using Ambev.DeveloperEvaluation.Application.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.DeleteBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branches.GetBranch;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branches
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchController: BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public BranchController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateBranchResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBranch ([FromBody] CreateBranchRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateBranchRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            
            var command = _mapper.Map<CreateBranchCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateBranchResponse>
            {
                Data = _mapper.Map<CreateBranchResponse>(response),
                Success = true,
                Message = "Branch created successfully"
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetBranchResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetBranch([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetBranchRequest { Id = id };
            var validator = new GetBranchRequestValidator();
            
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            
            var response = await _mediator.Send(request, cancellationToken);
            
            return Ok(new ApiResponseWithData<GetBranchResponse>
            {
                Data = _mapper.Map<GetBranchResponse>(response),
                Success = true
            });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBranch([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new DeleteBranchRequest { Id = id };
            var validator = new DeleteBranchRequestValidator();

            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var response = await _mediator.Send(request, cancellationToken);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Branch deleted successfully"
            });
        }
    }
}
