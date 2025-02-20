using System;

namespace CadavizCodeHub.Framework.Domain
{
    public abstract class EntityBase : IEntity
    {
        protected EntityBase() { }

        public Guid Id { get; protected set; } = Guid.NewGuid();

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not EntityBase entity)
                return false;

            return Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
