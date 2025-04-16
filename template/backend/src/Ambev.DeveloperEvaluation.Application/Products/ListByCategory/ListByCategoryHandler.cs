using Ambev.DeveloperEvaluation.Application.Commom;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListByCategory
{
    public class ListByCategoryHandler: IRequestHandler<ListByCategoryCommand, PaginatedResult<Product>>
    {
        private readonly IProductRepository _repository;

        public ListByCategoryHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedResult<Product>> Handle(ListByCategoryCommand request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllPaginatedAsync(request.Page, request.Size, cancellationToken);

            var totalCount = products.Count;
            return new PaginatedResult<Product>(
            products,
            totalCount,
            request.Page,
            request.Size);
        }
    }
}
