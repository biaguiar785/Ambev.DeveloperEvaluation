namespace Ambev.DeveloperEvaluation.Application.SaleItems
{
    public class GetSaleItemResult
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal TotalItem { get; set; }
    }
}
