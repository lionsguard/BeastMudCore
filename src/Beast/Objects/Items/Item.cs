using System;

namespace Beast.Objects.Items
{
    public class Item : ObjectBase, IItem
    {
        public Guid? OwnerId { get; set; }
        public decimal Value { get; set; }
        public int Weight { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
    }
}
