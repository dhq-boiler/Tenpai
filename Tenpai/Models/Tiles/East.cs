using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class East : Winds
    {
        public override string Display
        {
            get { return "東"; }
        }

        public override int GetHashCode()
        {
            return 27;
        }
    }
}
