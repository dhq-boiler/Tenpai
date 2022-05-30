using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenpai.Models.Tiles;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 役牌
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ValueTiles<T> : Yaku where T : Honors
    {
        /// <summary>
        /// 役牌
        /// </summary>
        /// <param name="type"></param>
        public ValueTiles(ValueType type)
            : base($"役牌{DisplayString<T>(type)}", MenzenExclusiveOrDownfallOrNo.NoDownfall, 1)
        {
        }

        private static object DisplayString<T>(ValueType type) where T : Honors
        {
            switch (type)
            {
                case ValueType.Unknown:
                    throw new Exception("ValueTypeが指定されていません");
                case ValueType.ThreeElementTille:
                    if (typeof(T) == typeof(White))
                        return "白";
                    else if (typeof(T) == typeof(Green))
                        return "發";
                    else if (typeof(T) == typeof(Red))
                        return "中";
                    else
                        throw new Exception("ValueTypeとtargetの組み合わせがおかしいです");
                case ValueType.SelfStyledTile:
                    return $":自風牌";
                case ValueType.FiledStyleTiles:
                    return $":場風牌";
            }
            throw new Exception("ありえないことがおこりました");
        }
    }

    public enum ValueType
    {
        /// <summary>
        /// 未指定
        /// </summary>
        Unknown,

        /// <summary>
        /// 三元牌
        /// </summary>
        ThreeElementTille,

        /// <summary>
        /// 自風牌
        /// </summary>
        SelfStyledTile,

        /// <summary>
        /// 場風牌
        /// </summary>
        FiledStyleTiles,
    }
}
