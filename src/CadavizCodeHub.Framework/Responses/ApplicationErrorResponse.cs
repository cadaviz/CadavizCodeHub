using System.Collections.Generic;
using System.Net;

namespace CadavizCodeHub.Framework.Responses
{
    /// <summary>
    /// A generic error response for application
    /// </summary>
    /// <param name="StatusCode">Response http status code</param>
    /// <param name="Messages">List of messages</param>
    public record ApplicationErrorResponse(HttpStatusCode StatusCode, IList<ApplicationMessage> Messages);
    
}
