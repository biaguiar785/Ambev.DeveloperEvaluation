namespace Ambev.DeveloperEvaluation.Domain.Events.Sales
{
    public class SaleCreatedEvent
    {

        public Guid Id { get; set; }
        public Guid BranchId { get; set; }
        public Guid UserId { get; set; }
        public List<SaleItemCreatedEvent> Items { get; set; }
       
        public SaleCreatedEvent(Guid id, Guid branchId, Guid userId, List<SaleItemCreatedEvent> items)
        {
            Id = id;
            BranchId = branchId;
            UserId = userId;
            Items = items;
        }

    }
}
