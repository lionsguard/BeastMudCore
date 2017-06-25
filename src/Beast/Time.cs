using System;

namespace Beast
{
    public class Time : ITime
    {
        public long Tick { get; }

        public TimeSpan Elapsed { get; }

        public Time(long tick, TimeSpan elapsed)
        {
            Tick = tick;
            Elapsed = elapsed;
        }
    }
}
