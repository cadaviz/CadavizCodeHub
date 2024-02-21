namespace CadavizCodeHub.Framework.Runtime
{
    public class MemoryRuntimeContext : IRuntimeContext
    {
        public MemoryRuntimeContext(IRuntimeUser user)
        {
            User = user;
        }

        public IRuntimeUser User { get; }
    }
}
