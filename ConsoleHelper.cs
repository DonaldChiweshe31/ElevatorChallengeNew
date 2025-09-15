using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorChallenge.Helpers
{
    public static class ConsoleHelper
    {
        public static void WriteColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }

        public static void WriteLineColored(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        public static void DisplayError(string message)
        {
            WriteLineColored($"ERROR: {message}", ConsoleColor.Red);
        }

        public static void DisplaySuccess(string message)
        {
            WriteLineColored($"SUCCESS: {message}", ConsoleColor.Green);
        }

        public static void DisplayWarning(string message)
        {
            WriteLineColored($"WARNING: {message}", ConsoleColor.Yellow);
        }

        public static void DisplayInfo(string message)
        {
            WriteLineColored($"INFO: {message}", ConsoleColor.Cyan);
        }
    }
}
