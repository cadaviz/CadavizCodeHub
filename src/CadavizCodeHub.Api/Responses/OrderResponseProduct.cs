namespace CadavizCodeHub.Api.Responses
{
    /// <summary>
    /// Product representation
    /// </summary>
    /// <param name="Description">Description of the Product</param>
    /// <param name="Price">Product price</param>
    public record OrderResponseProduct(string Description, decimal Price);
}
