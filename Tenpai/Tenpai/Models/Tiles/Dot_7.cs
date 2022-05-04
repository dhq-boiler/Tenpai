using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Dot_7 : Dots, IPenchanWaitable
    {
        public override string Display
        {
            get { return "p7"; }
        }

        public override int GetHashCode()
        {
            return 15;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Dot_4() };
        }

        public Tile[] Outward
        {
            get { return new Tile[] { new Dot_8(), new Dot_9() }; }
        }
    }
}
