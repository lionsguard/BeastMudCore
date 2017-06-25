using System;

namespace Beast.Objects.Mobiles
{
    public interface IMobile : IObject
    {
        Guid PlaceId { get; set; }
        Guid? OwnerId { get; set; }
        MobileActions Actions { get; set; }
        MobileAffects Affects { get; set; }
    }
}
