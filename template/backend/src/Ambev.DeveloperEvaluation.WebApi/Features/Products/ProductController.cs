using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.ListCategories;
using Ambev.DeveloperEvaluation.Application.Products.ListProducts;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    [ApiController]
    [Route("api/[controller]")]

    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            var command = _mapper.Map<CreateProductCommand>(request);
            var response = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
            {
                Data = _mapper.Map<CreateProductResponse>(response),
                Message = "Product created successfully.",
                Success = true
            });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var request = new GetProductRequest { Id = id };
            var validator = new GetProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<GetProductCommand>(request.Id);

            var response = await _mediator.Send(command, cancellationToken);
            var product = _mapper.Map<GetProductResponse>(response);
            return Ok(product);
        }

        [HttpGet("products")]
        [ProducesResponseType(typeof(ApiResponseWithData<IEnumerable<GetProductResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProducts(
        [FromQuery(Name = "_page")] int? page = null,
        [FromQuery(Name = "_size")] int? size = null,
        [FromQuery(Name = "_order")] string? order = null,
        CancellationToken cancellationToken = default)
        {
            var command = new ListProductsCommand
            {
                Page = page ?? 1,
                Size = size ?? 10,
                Order = order
            };

            var response = await _mediator.Send(command, cancellationToken);
            var products = _mapper.Map<IEnumerable<GetProductResponse>>(response);
            var paginated = new PaginatedList<GetProductResponse>(
                products.ToList(),
                response.TotalCount,
                response.CurrentPage,
                response.PageSize);

            return OkPaginated(paginated);

        }

        [HttpGet("categories")]
        [ProducesResponseType(typeof(ApiResponseWithData<List<string>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
        {
            var categories = await _mediator.Send(new ListCategoriesCommand(), cancellationToken);
            return Ok(categories);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct([FromRoute] Guid id, CancellationToken cancellationToken)
        {

            var request = new DeleteProductRequest { Id = id };
            var validator = new DeleteProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<DeleteProductCommand>(request.Id);
            await _mediator.Send(command, cancellationToken);

            return Ok(new ApiResponse
            {
                Success = true,
                Message = "Product deleted successfully."
            });

        }
    }
}
