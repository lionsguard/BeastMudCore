using System;
using System.Collections.Generic;
using System.Text;

namespace Beast.Objects.Mobiles
{
    public interface IPlayerCharacter : ICharacter
    {
        Guid? UserId { get; set; }
    }
}
