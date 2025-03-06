﻿using System;

namespace CadavizCodeHub.Framework.Responses
{
    /// <summary>
    /// Message returned by the application
    /// </summary>
    /// <param name="Message">Message for users</param>
    /// <param name="DeveloperMessage">Message for developers</param>
    public record ApplicationMessage
    {
        public string Message { get; init; }
        public string? DeveloperMessage { get; init; }

        public ApplicationMessage(string message, string? developerMessage = null)
        {
            ArgumentException.ThrowIfNullOrEmpty(message);

            Message = message;
            DeveloperMessage = developerMessage;
        }
    }
}
