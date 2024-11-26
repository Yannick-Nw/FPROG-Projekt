using System;

namespace WarAndPeace
{
    public static class ConsoleUtils
    {
        public static void Log(string message) => Console.WriteLine(message);
        
        public static void DrawProgressBar(int progress, int total, int barLength = 50)
        {
            Console.CursorLeft = 0; // Reset the cursor position
            double percentage = (double)progress / total;
            int filledBars = (int)(percentage * barLength);
            string bar = new string('=', filledBars) + new string('-', barLength - filledBars);

            Console.Write($"[{bar}] {progress}/{total} ({percentage:P0})");
        }
    }
}