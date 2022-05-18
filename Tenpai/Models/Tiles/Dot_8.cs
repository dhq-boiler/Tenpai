using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Dot_8 : Dots
    {
        public override string Display
        {
            get { return "p8"; }
        }

        public override int GetHashCode()
        {
            return 16;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Dot_5() };
        }
    }
}
