using System;

namespace CadavizCodeHub.Framework.Domain
{
    public interface IStampEntity : IEntity
    {
        DateTime CreatedAt { get; }

        DateTime UpdatedAt { get; }

        Guid CreateUserId { get; }

        Guid UpdateUserId { get; }
    }
}
