namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    public class SaleItemCreatedEvent
    {

        public Guid ProductId { get; set; }
        public int Quantity {  get; set; }
        public decimal Discount {  get; set; }
        public decimal TotalItem {  get; set; }
        public SaleItemCreatedEvent(Guid productId, int quantity, decimal discount, decimal totalItem)
        {
            ProductId = productId;
            Quantity = quantity;
            Discount = discount;
            TotalItem = totalItem;
        }
    }
}
