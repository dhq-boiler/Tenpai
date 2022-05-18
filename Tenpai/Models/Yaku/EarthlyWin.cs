using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 地和
    /// </summary>
    public class EarthlyWin : Yaku
    {
        /// <summary>
        /// 地和
        /// </summary>
        public EarthlyWin() : base("地和", MenzenExclusiveOrDownfallOrNo.MenzenExclusive, 13)
        {
        }
    }
}
