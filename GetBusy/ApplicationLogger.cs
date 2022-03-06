using GetBusy.Core;
using System.Diagnostics;

namespace GetBusy
{
    internal class SimpleLogger : IApplicationLogger
    {
        public void LogInformation(string message)
        {
            Log(message);
        }

        public void LogDebug(string message)
        {
            Log(message);
        }

        public void LogTrace(string message)
        {
            Log(message);
        }

        public void LogError(string errorMessage, string stackTrace = "")
        {
            Log($"{errorMessage}. {Environment.NewLine} {stackTrace}");
        }

        private static void Log(string errorMessage)
        {
            Debug.WriteLine(errorMessage);
        }
    }
}
