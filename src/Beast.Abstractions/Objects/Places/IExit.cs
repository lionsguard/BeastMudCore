using System;

namespace Beast.Objects.Places
{
    public interface IExit
    {
        Guid PlaceId { get; set; }
        Guid DestinationId { get; set; }
        Direction Direction { get; set; }
        ContainerStates State { get; set; }
    }
}
