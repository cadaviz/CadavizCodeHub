using CadavizCodeHub.Core.WebApi.Responses;
using System;

namespace CadavizCodeHub.Orders.WebApi.Responses
{
    public record UserResponse(Guid Id, string Name, string Email) : IResponse;
}