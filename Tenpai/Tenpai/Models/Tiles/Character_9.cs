using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Character_9 : Characters, ITerminals
    {
        public override string Display
        {
            get { return "m9"; }
        }

        public override int GetHashCode()
        {
            return 17;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Character_6() };
        }
    }
}
