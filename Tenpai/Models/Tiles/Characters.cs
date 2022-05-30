using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public abstract class Characters : Suits
    {
        public override string FileName => Display;

        public override bool IsSameType(Tile other)
        {
            return other is Characters;
        }
    }
}
