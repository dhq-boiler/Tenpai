using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Character_1 : Characters, ITerminals
    {
        public override string Display
        {
            get { return "m1"; }
        }
        public override int Number => 1;

        public override int GetHashCode()
        {
            return 0;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Character_4() };
        }
    }
}
