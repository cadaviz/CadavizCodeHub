using System;
using System.Collections.Generic;
using System.Linq;

namespace CadavizCodeHub.Core.WebApi.Responses
{
    /// <summary>
    /// A generic error response for application
    /// </summary>
    /// <param name="StatusCode">Response http status code</param>
    /// <param name="Messages">List of messages</param>
    public record class ApplicationErrorResponse : IResponse
    {
        public int StatusCode { get; }
        public IReadOnlyCollection<ApplicationMessage> Messages { get; }

        public ApplicationErrorResponse(int statusCode, ApplicationMessage message)
        : this(statusCode, [message])
        {
            if (message is null)
                throw new ArgumentException("Message cannot be null.", nameof(message));
        }

        public ApplicationErrorResponse(int statusCode, IEnumerable<ApplicationMessage> messages)
        {
            StatusCode = statusCode;
            Messages = messages?.ToArray() ?? [];
        }
    }
}
