using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Dot_5R : Dot_5
    {
        public Dot_5R()
        {
            IsRedSuited = true;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Dot_5 b))
                return false;
            return IsRedSuited == b.IsRedSuited;
        }

        public override int GetHashCode()
        {
            return 13;
        }
    }
}
