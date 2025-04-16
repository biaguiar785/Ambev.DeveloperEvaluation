using System.Security.Claims;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleHandler: IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly IHttpContextAccessor _httpFactory;
        private readonly ISaleRepository _saleRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMediator _mediator;

        public CreateSaleHandler(
            IHttpContextAccessor httpFactory, 
            ISaleRepository saleRepository, 
            IBranchRepository branchRepository, 
            IProductRepository productRepository, 
            IMediator mediator)
        {
            _httpFactory = httpFactory;
            _saleRepository = saleRepository;
            _branchRepository = branchRepository;
            _productRepository = productRepository;
            _mediator = mediator;
        }

        public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpFactory.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value!;

            //command validator
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            //verify branch exists
            var branch = await _branchRepository.GetByIdAsync(request.BranchId, cancellationToken);
            if (branch == null)
                throw new KeyNotFoundException($"Branch with id {request.BranchId} not found.");

            //create sale
            var sale = new Sale
            {
                SaleNumber = Guid.NewGuid().ToString(),
                BranchId = branch.Id,
                SaleDate = DateTime.UtcNow,
                Cancelled = false,
                UserId = new Guid(userId),
                Items = [],
                CreatedAt = DateTime.UtcNow,
            };

            //create sale items and apply discount

            var saleItems = new List<SaleItem>();

            foreach(var item in request.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
                if (product == null)
                    throw new KeyNotFoundException($"Product with id {item.ProductId} not found.");

                decimal disccount = 0m;
                if (item.Quantity >= 4 && item.Quantity < 10)
                    disccount = 0.10m;
                else if(item.Quantity>=10 && item.Quantity < 20)
                    disccount = 0.20m;

                var saleItemTotal = product.Price * item.Quantity;
                var discountValue = saleItemTotal * disccount;
                var total = saleItemTotal - discountValue;

                sale.Items.Add(new SaleItem
                {
                    SaleId = sale.Id,
                    ProductId = product.Id,
                    Quantity = item.Quantity,
                    Price = product.Price,
                    Discount = disccount,
                    TotalItemPrice = total,
                    Cancelled = false
                });
            }

            var saleResult = await _saleRepository.CreateAsync(sale, cancellationToken);

            //Todo: adicionar publicacao de evento

            return new CreateSaleResult { Id = saleResult.Id, Total= saleResult.TotalSale };

        }
    }
    
}
