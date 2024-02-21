using System;

namespace CadavizCodeHub.Framework.Runtime
{
    public class MemoryRuntimeUser : IRuntimeUser
    {
        public MemoryRuntimeUser(Guid id, string username, UserType type)
        {
            Id = id;
            Username = username;
            Type = type;
        }

        public Guid Id { get; }

        public string Username { get; }

        public UserType Type { get; }
    }
}
