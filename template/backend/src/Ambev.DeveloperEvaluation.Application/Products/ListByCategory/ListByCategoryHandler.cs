using Ambev.DeveloperEvaluation.Application.Commom;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListByCategory
{
    public class ListByCategoryHandler: IRequestHandler<ListByCategoryCommand, PaginatedResult<Product>>
    {
        private readonly IProductRepository _repository;
        private readonly IMediator _mediator;

        public ListByCategoryHandler(IProductRepository repository, IMediator mediator)
        {
            _repository = repository;
           _mediator = mediator;
        }

        public async Task<PaginatedResult<Product>> Handle(ListByCategoryCommand request, CancellationToken cancellationToken)
        {
            var (products, totalCount) = await _repository.GetAllPaginatedAsync(request.Page, request.Size, cancellationToken);

            return new PaginatedResult<Product>(
            products,
            totalCount,
            request.Page,
            request.Size);
        }
    }
}
