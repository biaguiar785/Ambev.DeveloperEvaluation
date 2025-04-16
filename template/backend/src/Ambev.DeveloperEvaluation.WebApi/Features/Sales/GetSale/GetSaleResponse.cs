namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale
{
    public class GetSaleResponse
    {
        public Guid Id { get; set; }
        public decimal Total { get; set; }

        public bool Cancelled { get; set; }
        public required Guid UserId { get; set; }
        public required string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public List<GetSaleItemResponse>? Items { get; set; }
    }
}
