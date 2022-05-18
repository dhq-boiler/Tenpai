using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public abstract class Dragons : Honors
    {
        public override bool IsSameType(Tile other)
        {
            return other is Dragons;
        }
    }
}
