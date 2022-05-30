using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 対々和
    /// </summary>
    public class AllTriples : Yaku
    {
        /// <summary>
        /// 対々和
        /// </summary>
        public AllTriples() : base("対々和", MenzenExclusiveOrDownfallOrNo.NoDownfall, 2)
        {
        }
    }
}
