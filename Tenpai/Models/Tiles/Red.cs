using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Red : Dragons
    {
        public override string Display
        {
            get { return "中"; }
        }

        public override int GetHashCode()
        {
            return 33;
        }
    }
}
