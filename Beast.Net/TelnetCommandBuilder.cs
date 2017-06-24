using System.Collections.Generic;

namespace Beast.Net
{
    public class TelnetCommandBuilder
    {
        readonly List<byte> _data = new List<byte>();

        public TelnetCommandBuilder Add(TelnetVerb verb, TelnetOption option)
        {
            _data.Add((byte)TelnetVerb.IAC);
            _data.Add((byte)verb);
            _data.Add((byte)option);

            return this;
        }

        public TelnetCommandBuilder AddSequence(TelnetOption option, params byte[] args)
        {
            Add(TelnetVerb.SB, option);
            if (args != null && args.Length > 0)
            {
                _data.AddRange(args);
            }
            _data.Add((byte)TelnetVerb.IAC);
            _data.Add((byte)TelnetVerb.SE);

            return this;
        }

        public byte[] Build()
        {
            return _data.ToArray();
        }
    }
}
