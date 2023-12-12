namespace CadavizCodeHub.Domain.Entities
{
    public class Item
    {
        protected Item () { }

        public Item(Product product, int quantity) : this () 
        {
            Product = product;
            Quantity = quantity;
        }

        public Product Product { get; }
        public int Quantity { get; }
        public decimal Total => Quantity * Product.Price;
    }
}
