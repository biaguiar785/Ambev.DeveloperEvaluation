using Ambev.DeveloperEvaluation.Application.SaleItems;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    public class GetSaleResult
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string SaleNumber { get; set; } = default!;
        public DateTime SaleDate { get; set; }
        public decimal Total { get; set; }
        public bool Cancelled { get; set; }
        public List<GetSaleItemResult> Items { get; set; } = [];
    }
}