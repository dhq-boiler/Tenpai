using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku.Meld
{
    public enum KongType
    {
        /// <summary>
        /// 未定義
        /// </summary>
        Unknown,

        /// <summary>
        /// 暗槓
        /// </summary>
        ConcealedKong,

        /// <summary>
        /// 大明槓
        /// </summary>
        LargeMeldedKong,

        /// <summary>
        /// 小明槓（加槓）
        /// </summary>
        SmallMeldedKong
    }
}
