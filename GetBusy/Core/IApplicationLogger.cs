namespace GetBusy.Core
{
    public interface IApplicationLogger
    {
        void LogInformation(string message);
        void LogDebug(string message);
        void LogTrace(string message);
        void LogError(string errorMessage, string stackTrace = "");
    }
}
