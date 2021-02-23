using System;

namespace SpaceGame
{
    public static class StringExtensions
    {
        public static void Write(this string original, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(original);
            Console.ResetColor();
        }

        public static void WriteLine(this string original, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(original);
            Console.ResetColor();
        }
    }
}
