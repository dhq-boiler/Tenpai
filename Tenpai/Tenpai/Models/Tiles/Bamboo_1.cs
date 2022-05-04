using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Bamboo_1 : Bamboos, ITerminals
    {
        public override string Display
        {
            get { return "s1"; }
        }

        public override int GetHashCode()
        {
            return 18;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Bamboo_4() };
        }
    }
}
