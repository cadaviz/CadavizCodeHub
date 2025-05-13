using System;

namespace CadavizCodeHub.Orders.Domain.Entities
{
    public class User
    {
        public Guid Id { get; } = Guid.NewGuid();
        public required string Name { get; init; }
        public required string Email { get; init; }
    }
}