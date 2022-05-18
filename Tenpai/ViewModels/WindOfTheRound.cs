using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.ViewModels
{
    /// <summary>
    /// 場風
    /// </summary>
    public enum WindOfTheRound
    {
        [Description("東場")]
        /// <summary>
        /// 東場
        /// </summary>
        East,

        [Description("南場")]
        /// <summary>
        /// 南場
        /// </summary>
        South,

        [Description("西場")]
        /// <summary>
        /// 西場
        /// </summary>
        West,

        [Description("北場")]
        /// <summary>
        /// 北場
        /// </summary>
        North,
    }
}
