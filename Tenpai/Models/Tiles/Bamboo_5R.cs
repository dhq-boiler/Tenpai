using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Bamboo_5R : Bamboo_5
    {
        public Bamboo_5R()
        {
            IsRedSuited = true;
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
    }
}
