using System;
using CadavizCodeHub.Framework.Runtime;

namespace CadavizCodeHub.Framework.Domain
{
    public abstract class StampEntityBase : EntityBase, IStampEntity
    {
        protected StampEntityBase() : base() { }

        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }
        public Guid CreateUserId { get; private set; }
        public Guid UpdateUserId { get; private set; }

        public virtual void Stamp()
        {
            StampInsert();
            StampUpdate();
        }

        private void StampUpdate()
        {
            UpdateUserId = RuntimeContext.Current.User.Id;
            UpdatedAt = DateTime.UtcNow;
        }

        private void StampInsert()
        {
            CreateUserId = RuntimeContext.Current.User.Id;
            CreatedAt = DateTime.UtcNow;
        }
    }
}