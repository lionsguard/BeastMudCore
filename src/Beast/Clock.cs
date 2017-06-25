using System;
using System.Diagnostics;

namespace Beast
{
    public class Clock : IClock
    {
        static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public long Tick { get; private set; }

        public TimeSpan ElapsedTime { get; private set; }

        public TimeSpan TotalTIme { get; private set; }

        public ITime CurrentTime { get; private set; }

        private Stopwatch _sw = new Stopwatch();
        private TimeSpan _lastElapsed;

        public void Dispose()
        {
            _sw.Stop();
        }

        public void Start()
        {
            _sw.Start();
            CurrentTime = new Time(0, TimeSpan.Zero);
        }

        public void Update()
        {
            if (!_sw.IsRunning)
                _sw.Start();

            Tick++;
            TotalTIme += ElapsedTime;

            var elapsed = _sw.Elapsed;
            ElapsedTime = elapsed - _lastElapsed;
            _lastElapsed = elapsed;

            CurrentTime = new Time(Tick, ElapsedTime);
        }
    }
}
