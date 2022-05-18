using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Character_8 : Characters
    {
        public override string Display
        {
            get { return "m8"; }
        }

        public override int GetHashCode()
        {
            return 7;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Character_5() };
        }
    }
}
