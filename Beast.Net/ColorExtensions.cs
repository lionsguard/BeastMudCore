using System;

namespace Beast.Net
{
    public static class ColorExtensions
    {
        public static string ToAnsiColor(this string text, ConsoleColor color)
        {
            var ansi = AnsiCode.DefaultTextColor; //"\x1B[39m";
            var bold = true;
            switch (color)
            {
                case ConsoleColor.Black:
                    ansi = AnsiCode.Black;
                    break;
                case ConsoleColor.Blue:
                    ansi = AnsiCode.Blue;
                    break;
                case ConsoleColor.Cyan:
                    ansi = AnsiCode.Cyan;
                    break;
                case ConsoleColor.DarkBlue:
                    ansi = AnsiCode.Blue;
                    bold = false;
                    break;
                case ConsoleColor.DarkCyan:
                    ansi = AnsiCode.Cyan;
                    bold = false;
                    break;
                case ConsoleColor.DarkGray:
                    ansi = AnsiCode.DefaultTextColor;
                    bold = false;
                    break;
                case ConsoleColor.DarkGreen:
                    ansi = AnsiCode.Green;
                    bold = false;
                    break;
                case ConsoleColor.DarkMagenta:
                    ansi = AnsiCode.Magenta;
                    bold = false;
                    break;
                case ConsoleColor.DarkRed:
                    ansi = AnsiCode.Red;
                    bold = false;
                    break;
                case ConsoleColor.DarkYellow:
                    ansi = AnsiCode.Yellow;
                    bold = false;
                    break;
                case ConsoleColor.Gray:
                    ansi = AnsiCode.DefaultTextColor;
                    break;
                case ConsoleColor.Green:
                    ansi = AnsiCode.Green;
                    break;
                case ConsoleColor.Magenta:
                    ansi = AnsiCode.Magenta;
                    break;
                case ConsoleColor.Red:
                    ansi = AnsiCode.Red;
                    break;
                case ConsoleColor.White:
                    ansi = AnsiCode.White;
                    break;
                case ConsoleColor.Yellow:
                    ansi = AnsiCode.Yellow;
                    break;
            }

            return string.Concat(ansi.ToString(bold), text, AnsiCode.DefaultTextColor.ToString(true));
        }
    }
}
