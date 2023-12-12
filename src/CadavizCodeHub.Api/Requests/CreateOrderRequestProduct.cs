namespace CadavizCodeHub.Api.Requests
{
    /// <summary>
    /// Data necessary to create a new order
    /// </summary>
    /// <param name="Description">Description of the Product</param>
    /// <param name="Price">Product price</param>
    public record CreateOrderRequestProduct(string Description, decimal Price);
}
