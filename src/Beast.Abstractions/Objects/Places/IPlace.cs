using System.Collections.Generic;

namespace Beast.Objects.Places
{
    public interface IPlace : IObject
    {
        string Description { get; set; }
        ICollection<IExit> Exits { get; set; }

        void Enter(Direction from, IObject obj);
        void Exit(Direction to, IObject obj);
    }
}
