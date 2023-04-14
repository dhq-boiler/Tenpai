using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Bamboo_5 : Bamboos, IRedSuitedTile
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
                    return "s5r";
                return "s5";
            }
        }
        public override int Number => 5;

        public override bool Equals(object obj)
        {
            if (!(obj is Bamboo_5 b))
                return false;
            return IsRedSuited == b.IsRedSuited;
        }

        public override int GetHashCode()
        {
            return 22;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Bamboo_2(), new Bamboo_8() };
        }
    }
}
