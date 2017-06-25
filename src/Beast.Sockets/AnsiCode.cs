namespace Beast.Sockets
{
    public enum AnsiCode : int
    {
        Reset = 0,
        Bold = 1,
        NormalColorIntensity = 22,
        DefaultTextColor = 39,
        Black = 30,
        Red = 31,
        Green = 32,
        Yellow = 33,
        Blue = 34,
        Magenta = 35,
        Cyan = 36,
        White = 37
    }

    public static class AnsiCodeExtensions
    {
        public const string Escape = "\x1B[";

        public static string ToString(this AnsiCode code, bool bold)
        {
            return string.Concat(Escape, (int)code, bold ? ";1" : string.Empty, "m");
        }
    }
}
