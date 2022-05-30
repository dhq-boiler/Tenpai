using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Dot_9 : Dots, ITerminals
    {
        public override string Display
        {
            get { return "p9"; }
        }
        public override int Number => 9;

        public override int GetHashCode()
        {
            return 17;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Dot_6() };
        }
    }
}
