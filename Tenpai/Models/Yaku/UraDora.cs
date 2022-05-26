using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 裏ドラ
    /// </summary>
    public class UraDora : Yaku
    {
        /// <summary>
        /// 裏ドラ
        /// </summary>
        /// <param name="hanCount"></param>
        public UraDora(int hanCount) : base("裏ドラ", MenzenExclusiveOrDownfallOrNo.NoDownfall, hanCount)
        {
        }
    }
}
