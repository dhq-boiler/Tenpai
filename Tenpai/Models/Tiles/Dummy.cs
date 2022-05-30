using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Dummy : Tile
    {
        public override string FileName => string.Empty;

        public override string Display
        {
            get { return "X"; }
        }

        public override bool IsSameType(Tile other)
        {
            return false;
        }

        public override int GetHashCode()
        {
            return int.MaxValue;
        }
    }
}
