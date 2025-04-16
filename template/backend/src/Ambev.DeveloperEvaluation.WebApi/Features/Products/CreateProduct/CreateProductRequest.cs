namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    public class CreateProductRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
    }
}
