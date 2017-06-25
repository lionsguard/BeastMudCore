using System;

namespace Beast.Objects.Items
{
    public interface IItem : IObject
    {
        Guid? OwnerId { get; set; }
        decimal Value { get; set; }
        int Weight { get; set; }
        string ShortDescription { get; set; }
        string LongDescription { get; set; }
    }
}
