namespace GetBusy.Utils
{
    public static class CustomConsole
    {
        public static void WriteWarning(string message)
        {
            WriteCustomColor(message, ConsoleColor.DarkYellow);

        }

        public static void WriteError(string message)
        {
            WriteCustomColor(message, ConsoleColor.Red);
        }

        private static void WriteCustomColor(string message, ConsoleColor backgroundColor = ConsoleColor.Black, ConsoleColor foregroundColor = ConsoleColor.White)
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            Console.Write(message);
            Console.ResetColor();
        }
    }
}
