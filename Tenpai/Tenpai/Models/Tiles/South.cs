using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class South : Winds
    {
        public override string Display
        {
            get { return "南"; }
        }

        public override int GetHashCode()
        {
            return 28;
        }
    }
}
