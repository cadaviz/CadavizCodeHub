using System;

namespace CadavizCodeHub.Framework.Extensions
{
    public static class EnumExtensions
    {
        public static TDestination Convert<TSource, TDestination>(TSource source, TDestination? defaultValue = null)
        where TSource : Enum
        where TDestination : struct, Enum
        {
            if (Enum.IsDefined(typeof(TSource), source) && Enum.TryParse(typeof(TDestination), source.ToString(), out object? result))
            {
                return (TDestination)result;
            }
            else if (defaultValue.HasValue)
            {
                return defaultValue.Value;
            }

            throw new ArgumentException($"Error while converting an {typeof(TSource)} enum into an {typeof(TDestination)}.");
        }
    }
}