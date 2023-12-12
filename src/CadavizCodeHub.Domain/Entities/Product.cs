namespace CadavizCodeHub.Domain.Entities
{
    public class Product
    {
        protected Product() { }

        public Product(string description, decimal price) : this()
        {
            Description = description;
            Price = price;
        }

        public string Description { get; }
        public decimal Price { get; }
    }
}
