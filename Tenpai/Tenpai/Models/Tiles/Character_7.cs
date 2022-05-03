using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Character_7 : Characters, IPenchanWaitable
    {
        public override string Display
        {
            get { return "m7"; }
        }

        public override int GetHashCode()
        {
            return 15;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Character_4() };
        }

        public Tile[] Outward
        {
            get { return new Tile[] { new Character_8(), new Character_9() }; }
        }
    }
}
