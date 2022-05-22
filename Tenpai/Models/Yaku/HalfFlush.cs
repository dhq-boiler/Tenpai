using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 混一色
    /// </summary>
    public class HalfFlush : Yaku
    {
        /// <summary>
        /// 混一色
        /// </summary>
        public HalfFlush() : base("混一色", MenzenExclusiveOrDownfallOrNo.Downfall, 3)
        {
        }
    }
}
