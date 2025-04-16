using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    public class ListProductsHandler : IRequestHandler<ListProductsCommand, List<GetProductResult>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public ListProductsHandler(IProductRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<GetProductResult>> Handle(ListProductsCommand request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllPaginatedAsync(request.PageNumber, request.Size, cancellationToken);

            var results = products.Select(x => _mapper.Map<GetProductResult>(x)).ToList();

            return results;

        }

    }
}
