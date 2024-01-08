using System.Collections.Generic;

namespace CadavizCodeHub.Framework.Responses
{
    /// <summary>
    /// A generic error response for application
    /// </summary>
    /// <param name="StatusCode">Response http status code</param>
    /// <param name="Messages">List of messages</param>
    public record ApplicationErrorResponse(int StatusCode, IEnumerable<ApplicationMessage> Messages);
    
}
