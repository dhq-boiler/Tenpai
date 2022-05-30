using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 三色同順
    /// </summary>
    public class ThreeColorRuns : Yaku
    {
        /// <summary>
        /// 三色同順
        /// </summary>
        public ThreeColorRuns() : base("三色同順", MenzenExclusiveOrDownfallOrNo.Downfall, 2)
        {
        }
    }
}
