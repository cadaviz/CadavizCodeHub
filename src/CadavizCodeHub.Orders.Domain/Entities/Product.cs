namespace CadavizCodeHub.Orders.Domain.Entities
{
    public class Product
    {
        public Product() { }

        public required string Description { get; init; }
        public required decimal Price { get; init; }
    }
}
