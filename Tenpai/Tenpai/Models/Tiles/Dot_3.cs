using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Dot_3 : Dots, IPenchanWaitable
    {
        public override string Display
        {
            get { return "p3"; }
        }

        public override int GetHashCode()
        {
            return 20;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Dot_6() };
        }

        public Tile[] Outward
        {
            get { return new Tile[] { new Dot_1(), new Dot_2() }; }
        }
    }
}
