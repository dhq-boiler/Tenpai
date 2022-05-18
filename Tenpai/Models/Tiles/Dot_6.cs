using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Dot_6 : Dots
    {
        public override string Display
        {
            get { return "p6"; }
        }

        public override int GetHashCode()
        {
            return 14;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Dot_3(), new Dot_9() };
        }
    }
}
