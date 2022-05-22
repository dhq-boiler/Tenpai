using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Bamboo_9 : Bamboos, ITerminals
    {
        public override string Display
        {
            get { return "s9"; }
        }
        public override int Number => 9;

        public override int GetHashCode()
        {
            return 26;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Bamboo_6() };
        }
    }
}
