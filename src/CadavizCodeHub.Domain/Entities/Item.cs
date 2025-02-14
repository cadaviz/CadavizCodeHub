namespace CadavizCodeHub.Domain.Entities
{
    public class Item
    {
        public Item() { }

        public required Product Product { get; init; }
        public required int Quantity { get; init; }
        public decimal Total => Quantity * Product.Price;
    }
}
