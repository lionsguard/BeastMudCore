using System;

namespace Beast.Objects
{
    public interface IObject
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
