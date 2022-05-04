using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Character_4 : Characters
    {
        public override string Display
        {
            get { return "m4"; }
        }

        public override int GetHashCode()
        {
            return 3;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Character_1(), new Character_7() };
        }
    }
}
