using System;

namespace CadavizCodeHub.Framework.Domain
{
    public abstract class EntityBase : IEntity
    {
        protected EntityBase() { }

        public Guid Id { get; protected set; }

        public static bool operator ==(EntityBase entity1, EntityBase entity2)
        {
            if (entity1 is null && entity2 is null)
                return true;

            if (entity1 is null || entity2 is null)
                return false;

            return entity1.Id.Equals(entity2.Id)
             && entity1.GetType() == entity2.GetType();
        }

        public static bool operator !=(EntityBase entity1, EntityBase entity2)
        {
            return !(entity1 == entity2);
        }

        public override bool Equals(object? obj)
        {
            if (obj is null || obj is not EntityBase entity)
                return false;

            return this == entity;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
