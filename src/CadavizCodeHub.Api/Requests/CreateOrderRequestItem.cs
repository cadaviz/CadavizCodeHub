namespace CadavizCodeHub.Api.Requests
{
    /// <summary>
    /// Data necessary to create a new order
    /// </summary>
    /// <param name="Product">Product information</param>
    /// <param name="Quantity">Item quantity</param>
    public record CreateOrderRequestItem(CreateOrderRequestProduct Product, int Quantity);
}
