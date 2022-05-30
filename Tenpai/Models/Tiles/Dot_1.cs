using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Dot_1 : Dots, ITerminals
    {
        public override string Display
        {
            get { return "p1"; }
        }
        public override int Number => 1;

        public override int GetHashCode()
        {
            return 9;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Dot_4() };
        }
    }
}
