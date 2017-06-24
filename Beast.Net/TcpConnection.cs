using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Beast.Net
{
    public class TcpConnection : IConnection
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>(StringComparer.CurrentCultureIgnoreCase);
        public IConnectionContext Context { get; set; }

        readonly ILogger _logger;
        readonly List<byte> _sequence = new List<byte>();
        readonly List<byte> _buffer = new List<byte>();

        Socket _socket;
        TelnetCommand _telnetCommand;

        public TcpConnection(Socket socket, ILogger logger)
        {
            _logger = logger;
            _socket = socket;
        }

        public async Task SendAsync(string message)
        {
            var buffer = Encoding.ASCII.GetBytes(message);
            await SendRaw(buffer);
        }

        public async Task SendErrorAsync(string error)
        {
            await SendAsync(error.ToAnsiColor(ConsoleColor.Red));
        }

        async Task SendRaw(byte[] data)
        {
            await _socket.SendAsync(new ArraySegment<byte>(data), SocketFlags.None);
        }

        public Task CloseAsync()
        {
            _logger.LogInformation("Closing connection '{0}'", _socket?.RemoteEndPoint?.ToString());
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Dispose();
            _socket = null;
            return Task.FromResult(0);
        }

        public async void BeginReceive()
        {
            if (_socket == null || !_socket.Connected)
                return;

            var buffer = new ArraySegment<byte>(new byte[4096]);
            var bytesReceived = await _socket.ReceiveAsync(buffer, SocketFlags.None);

            if (bytesReceived == 0)
            {
                await CloseAsync();
                return;
            }

            var data = new byte[bytesReceived];
            Array.Copy(buffer.Array, 0, data, 0, bytesReceived);

            System.Diagnostics.Debug.WriteLine("Received '{0}' from '{1}'", string.Join(",", data.Select(o => o.ToString())), Id);

            await ParseBytes(data);

            BeginReceive();
        }

        async Task ParseBytes(byte[] data)
        {
            using (var ms = new MemoryStream(data))
            {
                using (var reader = new BinaryReader(ms))
                {
                    while (reader.BaseStream.Position < reader.BaseStream.Length)
                    {
                        var input = reader.ReadByte();

                        if (_telnetCommand != null)
                        {
                            if (_telnetCommand.Verb == null)
                            {
                                _telnetCommand.Verb = (TelnetVerb)input;
                                continue;
                            }

                            if (_telnetCommand.Option == null)
                            {
                                _telnetCommand.Option = (TelnetOption)input;
                            }

                            var verb = _telnetCommand.Verb.Value;
                            var option = _telnetCommand.Option.Value;

                            switch (verb)
                            {
                                case TelnetVerb.SB:
                                    // Sub-negotiation
                                    // Grab bytes until IAC|SE is reached.
                                    if (input == (byte)option)
                                        continue;

                                    if (input == 0x0)
                                        continue;

                                    if (input == (byte)TelnetVerb.IAC)
                                        continue;

                                    if (input == (byte)TelnetVerb.SE)
                                    {
                                        EndSequence(option);
                                        continue;
                                    }

                                    _sequence.Add(input);
                                    break;
                                case TelnetVerb.SE:
                                    // End of a sequence
                                    EndSequence(option);
                                    break;
                                case TelnetVerb.WILL:
                                    switch (option)
                                    {
                                        case TelnetOption.TerminalType:
                                            await SendRaw(new TelnetCommandBuilder()
                                                .Add(TelnetVerb.DO, TelnetOption.TerminalType)
                                                .AddSequence(TelnetOption.TerminalType, 1)
                                                .Build());
                                            break;
                                        case TelnetOption.WindowSize:
                                            await SendRaw(new TelnetCommandBuilder()
                                                .Add(TelnetVerb.DO, TelnetOption.WindowSize)
                                                .Build());
                                            break;
                                        default:
                                            await SendRaw(new TelnetCommandBuilder()
                                                .Add(TelnetVerb.DONT, option)
                                                .Build());
                                            break;
                                    }
                                    _telnetCommand = null;
                                    break;
                                case TelnetVerb.WONT:
                                    _telnetCommand = null;
                                    break;
                                case TelnetVerb.DO:
                                    switch (option)
                                    {
                                        case TelnetOption.SupressGoAhead:
                                            await SendRaw(new TelnetCommandBuilder()
                                                .Add(TelnetVerb.WILL, TelnetOption.SupressGoAhead)
                                                .Build());
                                            break;
                                        default:
                                            await SendRaw(new TelnetCommandBuilder()
                                                .Add(TelnetVerb.WONT, option)
                                                .Build());
                                            break;
                                    }
                                    _telnetCommand = null;
                                    break;
                                case TelnetVerb.DONT:
                                    _telnetCommand = null;
                                    break;
                                case TelnetVerb.IAC:
                                    // Probably not a telnet command...
                                    _buffer.Add(input);
                                    _telnetCommand = null;
                                    break;
                                default:
                                    _telnetCommand = null;
                                    break;
                            }

                            continue;
                        }

                        switch (input)
                        {
                            case (byte)TelnetVerb.IAC:
                                _telnetCommand = new TelnetCommand();
                                break;
                            case 0x7f: // Backspace
                                // Remove the last character entered.
                                _buffer.RemoveAt(_buffer.Count - 1);
                                break;
                            case 0x0A: // New Line \n
                            case 0x0D: // Carriage Return \r
                                await Context?.ProcessInput(Encoding.ASCII.GetString(_buffer.ToArray()));
                                _buffer.Clear();
                                return;
                            default:
                                _buffer.Add(input);
                                break;
                        }
                    }
                }
            }
        }

        void EndSequence(TelnetOption option)
        {
            try
            {
                if (option == TelnetOption.TerminalType)
                {
                    if (!Properties.ContainsKey("TerminalType"))
                        Properties["TerminalType"] = string.Empty;
                    var name = Properties["TerminalType"] as string ?? string.Empty;
                    if (name.Length > 0)
                        name += " ";

                    name += Encoding.ASCII.GetString(_sequence.ToArray());
                    Properties["TerminalType"] = name;
                }

                if (option == TelnetOption.WindowSize)
                {
                    Properties["Width"] = (short)_sequence[0];
                    Properties["Height"] = (short)_sequence[1];
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }

            _sequence.Clear();
            _telnetCommand = null;
        }

        class ParseBytesResult
        {
            public bool IsInput { get; set; }
            public List<byte> Data { get; }

            public ParseBytesResult()
            {
                Data = new List<byte>();
            }
        }

        class TelnetCommand
        {
            public TelnetVerb? Verb;
            public TelnetOption? Option;
        }
    }
}
