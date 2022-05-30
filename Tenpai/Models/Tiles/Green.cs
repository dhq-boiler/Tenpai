using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Green : Dragons
    {
        public override string FileName => "hatsu";

        public override string Display
        {
            get { return "發"; }
        }

        public override int GetHashCode()
        {
            return 32;
        }
    }
}
