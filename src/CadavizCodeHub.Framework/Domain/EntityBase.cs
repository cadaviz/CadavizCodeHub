using System;

namespace CadavizCodeHub.Framework.Domain
{
    public abstract class EntityBase : IEntity
    {
        protected EntityBase() { }

        public Guid Id { get; protected set; }

        public override bool Equals(object? entity2)
        {
            if (entity2 is null || entity2 is not EntityBase entity)
                return false;

            if (this is null && entity2 is null)
                return true;

            if (this is null || entity2 is null)
                return false;

            return Id.Equals(entity.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
