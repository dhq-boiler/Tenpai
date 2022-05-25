using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// ドラ
    /// </summary>
    public class Dora : Yaku
    {
        /// <summary>
        /// ドラ
        /// </summary>
        /// <param name="hanCount"></param>
        public Dora(int hanCount) : base("ドラ", MenzenExclusiveOrDownfallOrNo.NoDownfall, hanCount)
        {
        }
    }
}
