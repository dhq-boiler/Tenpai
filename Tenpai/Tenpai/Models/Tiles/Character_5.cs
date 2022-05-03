using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Character_5 : Characters, IRedSuitedTile
    {
        private bool _IsRedSuited;

        public bool IsRedSuited
        {
            get { return _IsRedSuited; }
            set
            {
                if (IsLocked) return;
                _IsRedSuited = value;
            }
        }

        public override string Display
        {
            get
            {
                if (IsRedSuited)
                    return "m5r";
                return "m5"; 
            }
        }

        public override int GetHashCode()
        {
            return 13;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Character_2(), new Character_8() };
        }
    }
}
