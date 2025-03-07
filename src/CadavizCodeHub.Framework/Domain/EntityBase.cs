using System;

namespace CadavizCodeHub.Framework.Domain
{
    public abstract class EntityBase : IEntity
    {
        protected EntityBase() { }

        public Guid Id { get; protected set; } = Guid.NewGuid();

        public override bool Equals(object? obj)
        {
            return obj is EntityBase entity && Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
