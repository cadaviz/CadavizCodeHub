namespace System
{
    public static class ValidationExtensions
    {
        public static void ThrowArgumentNullExceptionIfNullWithLog(this object? argument, Action logAction)
        {
            if (argument is null)
            {
                logAction.Invoke();
                ArgumentNullException.ThrowIfNull(argument);
            }
        }
    }
}
