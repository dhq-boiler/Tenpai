using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Character_6 : Characters
    {
        public override string Display
        {
            get { return "m6"; }
        }

        public override int GetHashCode()
        {
            return 14;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Character_3(), new Character_9() };
        }
    }
}
