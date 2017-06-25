using System;

namespace Beast
{
    public interface IClock : IDisposable
    {
        long Tick { get; }
        TimeSpan ElapsedTime { get; }
        TimeSpan TotalTIme { get; }
        ITime CurrentTime { get; }
        void Start();
        void Update();
    }
}
