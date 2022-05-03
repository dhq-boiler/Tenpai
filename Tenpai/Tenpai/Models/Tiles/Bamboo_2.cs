using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Bamboo_2 : Bamboos
    {
        public override string Display
        {
            get { return "s2"; }
        }

        public override int GetHashCode()
        {
            return 1;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Bamboo_5() };
        }
    }
}
