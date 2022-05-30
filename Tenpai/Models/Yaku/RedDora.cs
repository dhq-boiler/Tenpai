using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 赤ドラ
    /// </summary>
    public class RedDora : Yaku
    {
        /// <summary>
        /// 赤ドラ
        /// </summary>
        /// <param name="hanCount"></param>
        public RedDora(int hanCount) : base("赤ドラ", MenzenExclusiveOrDownfallOrNo.NoDownfall, hanCount)
        {
        }
    }
}
