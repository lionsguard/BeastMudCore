using System;
using System.Collections.Generic;
using System.Text;

namespace Beast.Characters
{
    public class Character : ObjectBase, ICharacter
    {
        public Guid? UserId { get; set; }
    }
}
