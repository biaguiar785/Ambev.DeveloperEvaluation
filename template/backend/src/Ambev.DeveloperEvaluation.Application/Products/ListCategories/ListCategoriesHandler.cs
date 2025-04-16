using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategories
{
    public class ListCategoriesHandler :  IRequestHandler<ListCategoriesCommand, List<string>>
    {

        private readonly IProductRepository _repository;
        private readonly IMediator _mediator;

        public ListCategoriesHandler(IProductRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<List<string>> Handle(ListCategoriesCommand request, CancellationToken cancellationToken)
        {
            return await _repository.GetCategoriesAsync(cancellationToken);
        }
    }
}
