using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 一気通貫
    /// </summary>
    public class FullStraight : Yaku
    {
        /// <summary>
        /// 一気通貫
        /// </summary>
        public FullStraight() : base("一気通貫", MenzenExclusiveOrDownfallOrNo.Downfall, 2)
        {
        }
    }
}
