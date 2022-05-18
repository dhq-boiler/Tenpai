using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// ダブル立直
    /// </summary>
    public class DoubleReach : Yaku
    {
        /// <summary>
        /// ダブル立直
        /// </summary>
        public DoubleReach() : base("ダブル立直", MenzenExclusiveOrDownfallOrNo.MenzenExclusive, 1)
        {
        }
    }
}
