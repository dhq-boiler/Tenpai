using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 七対子
    /// </summary>
    public class SevenPairs : Yaku
    {
        /// <summary>
        /// 七対子
        /// </summary>
        public SevenPairs() : base("七対子", MenzenExclusiveOrDownfallOrNo.MenzenExclusive, 2)
        {
        }
    }
}
