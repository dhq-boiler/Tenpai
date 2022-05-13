using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    public class FirstTurnWin : Yaku
    {
        public FirstTurnWin() : base("一発", MenzenExclusiveOrDownfallOrNo.MenzenExclusive, 1)
        {
        }
    }
}
