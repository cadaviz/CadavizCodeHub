namespace CadavizCodeHub.Framework.Responses
{
    /// <summary>
    /// Message returned by the application
    /// </summary>
    /// <param name="Message">Message for users</param>
    /// <param name="DeveloperMessage">Message for developers</param>
    public record ApplicationMessage(string Message, string? DeveloperMessage = null);
}
