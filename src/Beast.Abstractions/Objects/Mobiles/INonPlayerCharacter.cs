using System;
using System.Collections.Generic;
using System.Text;

namespace Beast.Objects.Mobiles
{
    public interface INonPlayerCharacter : ICharacter
    {
        List<string> InteractionKeywords { get; set; }
        string ShortDescription { get; set; }
        string LongDescription { get; set; }
    }
}
