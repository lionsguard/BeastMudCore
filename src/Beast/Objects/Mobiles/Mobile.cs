using System;

namespace Beast.Objects.Mobiles
{
    public abstract class Mobile : ObjectBase, IMobile
    {
        public Guid PlaceId { get; set; }
        public Guid? OwnerId { get; set; }
        public MobileActions Actions { get; set; }
        public MobileAffects Affects { get; set; }
    }
}
