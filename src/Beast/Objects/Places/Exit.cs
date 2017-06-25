using System;

namespace Beast.Objects.Places
{
    public class Exit : IExit
    {
        public Guid PlaceId { get; set; }
        public Guid DestinationId { get; set; }
        public Direction Direction { get; set; }
        public ContainerStates State { get; set; }

        public Place Place { get; set; }
        public Place Destination { get; set; }
    }
}
