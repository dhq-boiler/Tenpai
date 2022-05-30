using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 清一色
    /// </summary>
    public class FullFlush : Yaku
    {
        /// <summary>
        /// 清一色
        /// </summary>
        public FullFlush() : base("清一色", MenzenExclusiveOrDownfallOrNo.Downfall, 6)
        {
        }
    }
}
