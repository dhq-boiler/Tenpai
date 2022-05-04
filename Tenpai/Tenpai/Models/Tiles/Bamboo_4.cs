using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Bamboo_4 : Bamboos
    {
        public override string Display
        {
            get { return "s4"; }
        }

        public override int GetHashCode()
        {
            return 21;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Bamboo_1(), new Bamboo_7() };
        }
    }
}
