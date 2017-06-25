using System;
using System.Collections.Generic;
using System.Linq;

namespace Beast.Objects.Places
{
    public class Place : ObjectBase, IPlace
    {
        public string Description { get; set; }
        public ICollection<Exit> Exits { get; set; }
        ICollection<IExit> IPlace.Exits { get => Exits.Cast<IExit>().ToList(); set => Exits = value.Cast<Exit>().ToList(); }

        public Place()
        {
            Exits = new List<Exit>();
        }

        public void Enter(Direction from, IObject obj)
        {

        }

        public void Exit(Direction to, IObject obj)
        {

        }
    }
}
