using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 三色同刻
    /// </summary>
    public class ThreeColorTriples : Yaku
    {
        /// <summary>
        /// 三色同刻
        /// </summary>
        public ThreeColorTriples() : base("三色同刻", MenzenExclusiveOrDownfallOrNo.NoDownfall, 2)
        {
        }
    }
}
