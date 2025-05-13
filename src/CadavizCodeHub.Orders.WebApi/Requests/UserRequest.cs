using CadavizCodeHub.Core.WebApi.Requests;

namespace CadavizCodeHub.Orders.WebApi.Requests
{
    public record UserRequest(string Name, string Email) : IRequest;
}