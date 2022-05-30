using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public abstract class Winds : Honors
    {
        public override bool IsSameType(Tile other)
        {
            return other is Winds;
        }
    }
}
