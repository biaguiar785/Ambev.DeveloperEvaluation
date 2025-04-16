using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductHandler: IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public CreateProductHandler(IProductRepository productRepository, IMapper mapper, IMediator mediator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _mediator = mediator;
        }

        public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {

            var validator = new CreateProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var product = _mapper.Map<Product>(request);
            var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);

            var result = _mapper.Map<CreateProductResult>(createdProduct);
            return result;
        }
    }
}
