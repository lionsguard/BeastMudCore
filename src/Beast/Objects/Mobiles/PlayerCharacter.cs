using System;
using System.Collections.Generic;
using System.Text;

namespace Beast.Objects.Mobiles
{
    public class PlayerCharacter : Character, IPlayerCharacter
    {
        public Guid? UserId { get; set; }
    }
}
