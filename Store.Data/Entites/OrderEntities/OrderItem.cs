namespace Store.Data.Entites.OrderEntities
{
    public class OrderItem : BaseEntity<Guid>
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public ProductItemOrderd ItemOrderd { get; set; }
        public Guid OrderId { get; set; }
    }
}