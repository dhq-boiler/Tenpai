using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 二盃口
    /// </summary>
    public class TwoDoubleRuns : Yaku
    {
        /// <summary>
        /// 二盃口
        /// </summary>
        public TwoDoubleRuns() : base("二盃口", MenzenExclusiveOrDownfallOrNo.MenzenExclusive, 3)
        {
        }
    }
}
