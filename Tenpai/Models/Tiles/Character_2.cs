using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Character_2 : Characters
    {
        public override string Display
        {
            get { return "m2"; }
        }
        public override int Number => 2;

        public override int GetHashCode()
        {
            return 1;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Character_5() };
        }
    }
}
