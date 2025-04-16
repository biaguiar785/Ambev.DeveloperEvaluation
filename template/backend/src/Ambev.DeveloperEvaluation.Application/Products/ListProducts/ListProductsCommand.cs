using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts
{
    public class ListProductsCommand: IRequest<List<GetProductResult>>
    {
        public int PageNumber { get; set; } = 1;
        public int Size { get; set; } = 10;

        public ListProductsCommand(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            Size = pageSize;
        }

    }
}
