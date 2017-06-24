using System;
using System.Collections.Generic;
using System.Text;

namespace Beast.Characters
{
    public interface ICharacter : IObject
    {
        Guid? UserId { get; set; }
    }
}
