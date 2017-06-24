using System;

namespace Beast
{
    public interface IObject
    {
        Guid Id { get; set; }
        string Name { get; set; }
    }
}
