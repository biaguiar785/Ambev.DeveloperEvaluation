using Ambev.DeveloperEvaluation.Application.Commom;
using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    public class ListProductsCommand: IRequest<PaginatedResult<Product>>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? Order { get; set; }
    }
}
