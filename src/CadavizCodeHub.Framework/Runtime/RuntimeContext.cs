using System.Threading;

namespace CadavizCodeHub.Framework.Runtime
{
    public class RuntimeContext
    {
        private static readonly ThreadLocal<IRuntimeContext> _runtimeContext = new();

        public static IRuntimeContext Current
        {
            get => _runtimeContext.Value;
            set
            {
                _runtimeContext.Value = value;
            }
        }
    }
}
