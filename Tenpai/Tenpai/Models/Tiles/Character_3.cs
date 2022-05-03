using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Character_3 : Characters, IPenchanWaitable
    {
        public override string Display
        {
            get { return "m3"; }
        }

        public override int GetHashCode()
        {
            return 11;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Character_6() };
        }

        public Tile[] Outward
        {
            get { return new Tile[] { new Character_1(), new Character_2() }; }
        }
    }
}
