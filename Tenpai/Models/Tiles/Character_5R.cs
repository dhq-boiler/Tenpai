using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Character_5R : Character_5
    {
        public Character_5R()
        {
            IsRedSuited = true;
        }
        public override int Number => 5;

        public override bool Equals(object obj)
        {
            if (!(obj is Character_5 b))
                return false;
            return IsRedSuited == b.IsRedSuited;
        }

        public override int GetHashCode()
        {
            return 4;
        }
    }
}
