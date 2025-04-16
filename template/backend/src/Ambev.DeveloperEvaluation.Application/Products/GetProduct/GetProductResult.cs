namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductResult
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}