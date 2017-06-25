using System;

namespace Beast.Objects
{
    [Flags]
    public enum ContainerStates
    {
        Open        = 0,
        Closeable   = 1,
        Pickproof   = 2,
        Closed      = 4,
        Locked      = 8
    }
}
