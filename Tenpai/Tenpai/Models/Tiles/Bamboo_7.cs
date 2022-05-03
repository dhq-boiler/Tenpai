using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Bamboo_7 : Bamboos, IPenchanWaitable
    {
        public override string Display
        {
            get { return "s7"; }
        }

        public override int GetHashCode()
        {
            return 6;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Bamboo_4() };
        }

        public Tile[] Outward
        {
            get { return new Tile[] { new Bamboo_8(), new Bamboo_9() }; }
        }
    }
}
