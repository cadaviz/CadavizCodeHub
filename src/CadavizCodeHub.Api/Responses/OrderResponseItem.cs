namespace CadavizCodeHub.Api.Responses
{
    /// <summary>
    /// Item representation
    /// </summary>
    /// <param name="Product">Product information</param>
    /// <param name="Quantity">Item quantity</param>
    public record OrderResponseItem(OrderResponseProduct Product, int Quantity);
}
