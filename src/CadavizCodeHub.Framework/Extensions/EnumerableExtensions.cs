using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Collections.Generic
{
    [ExcludeFromCodeCoverage]
    public static class EnumerableExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
    }
}
