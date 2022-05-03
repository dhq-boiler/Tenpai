using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Dot_4 : Dots
    {
        public override string Display
        {
            get { return "p4"; }
        }

        public override int GetHashCode()
        {
            return 21;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Dot_1(), new Dot_7() };
        }
    }
}
