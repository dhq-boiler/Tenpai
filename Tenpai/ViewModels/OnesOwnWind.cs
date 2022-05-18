using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.ViewModels
{
    /// <summary>
    /// 自風
    /// </summary>
    public enum OnesOwnWind
    {
        [Description("東家")]
        /// <summary>
        /// 東家
        /// </summary>
        East,

        [Description("南家")]
        /// <summary>
        /// 南家
        /// </summary>
        South,

        [Description("西家")]
        /// <summary>
        /// 西家
        /// </summary>
        West,

        [Description("北家")]
        /// <summary>
        /// 北家
        /// </summary>
        North,
    }
}
