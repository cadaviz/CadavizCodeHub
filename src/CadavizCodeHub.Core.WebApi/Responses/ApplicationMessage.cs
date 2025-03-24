using System;

namespace CadavizCodeHub.Core.WebApi.Responses
{
    /// <summary>
    /// Message returned by the application
    /// </summary>
    /// <param name="Message">Message for users</param>
    /// <param name="DeveloperMessage">Message for developers</param>
    public record class ApplicationMessage
    {
        public string Message { get; }
        public string? DeveloperMessage { get; }

        public ApplicationMessage(string message, string? developerMessage = null)
        {
            ArgumentException.ThrowIfNullOrEmpty(message);

            Message = message;
            DeveloperMessage = developerMessage;
        }
    }
}
