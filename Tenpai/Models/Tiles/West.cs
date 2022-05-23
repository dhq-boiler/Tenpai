using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class West : Winds
    {
        public override string FileName => "sha";

        public override string Display
        {
            get { return "西"; }
        }

        public override int GetHashCode()
        {
            return 29;
        }
    }
}
