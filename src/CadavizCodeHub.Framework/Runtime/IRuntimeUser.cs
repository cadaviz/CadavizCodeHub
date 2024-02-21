using System;

namespace CadavizCodeHub.Framework.Runtime
{
    public interface IRuntimeUser
    {
        Guid Id { get; }

        string Username { get; }

        UserType Type { get; }
    }
}
