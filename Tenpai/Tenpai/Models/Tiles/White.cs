using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class White : Dragons
    {
        public override string Display
        {
            get { return "白"; }
        }

        public override int GetHashCode()
        {
            return 31;
        }
    }
}
