using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 一盃口
    /// </summary>
    public class DoubleRun : Yaku
    {
        /// <summary>
        /// 一盃口
        /// </summary>
        public DoubleRun() : base("一盃口", MenzenExclusiveOrDownfallOrNo.MenzenExclusive, 1)
        {
        }
    }
}
