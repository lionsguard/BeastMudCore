using System;

namespace Beast
{
    public interface ITime
    {
        long Tick { get; }
        TimeSpan Elapsed { get; }
    }
}
