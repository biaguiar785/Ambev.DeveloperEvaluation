﻿using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductHandler : IRequestHandler<GetProductCommand, GetProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public GetProductHandler(
            IProductRepository productRepository,
            IMapper mapper,
            IMediator mediator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _mediator = mediator;
        }
        public async Task<GetProductResult> Handle(GetProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetProductCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if (product == null)
                throw new KeyNotFoundException($"Product with ID {request.Id} not found.");

            var result = _mapper.Map<GetProductResult>(product);

            return result;

        }
    }
}
