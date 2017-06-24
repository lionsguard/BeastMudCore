namespace Beast.Net
{
    public enum TelnetOption : byte
    {
        Echo = 1,
        SupressGoAhead = 3,
        Status = 5,
        TimingMark = 6,
        TerminalType = 24,
        WindowSize = 31,
        TerminalSpeed = 32,
        RemoteFlowControl = 33,
        LineMode = 34,
        EnvironmentVariables = 36
    }
}
