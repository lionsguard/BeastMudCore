using System;

namespace Beast
{
    public abstract class ObjectBase : IObject
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
    }
}
