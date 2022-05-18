using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 一発
    /// </summary>
    public class FirstTurnWin : Yaku
    {
        /// <summary>
        /// 一発
        /// </summary>
        public FirstTurnWin() : base("一発", MenzenExclusiveOrDownfallOrNo.MenzenExclusive, 1)
        {
        }
    }
}
