using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Bamboo_6 : Bamboos
    {
        public override string Display
        {
            get { return "s6"; }
        }

        public override int GetHashCode()
        {
            return 5;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Bamboo_3(), new Bamboo_9() };
        }
    }
}
