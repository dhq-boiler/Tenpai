namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 役の抽象クラス
    /// </summary>
    public abstract class Yaku
    {
        /// <summary>
        /// 役の有効性
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 役の名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 門前時翻数
        /// </summary>
        public int MenzenHanCount { get; set; }

        /// <summary>
        /// 副露後翻数
        /// </summary>
        public int CalledHanCount { get; set; }

        /// <summary>
        /// 門前限定
        /// true:門前限定
        /// false:鳴いても翻数がつく
        /// </summary>
        public MenzenExclusiveOrDownfallOrNo CallType { get; set; }

        public enum MenzenExclusiveOrDownfallOrNo
        {
            MenzenExclusive,
            Downfall,
            NoDownfall
        }

        /// <summary>
        /// 翻数
        /// </summary>
        public int HanCount(bool called)
        {
            if (called && (CallType == MenzenExclusiveOrDownfallOrNo.Downfall || CallType == MenzenExclusiveOrDownfallOrNo.NoDownfall))
                return CalledHanCount;
            else
                return MenzenHanCount;
        }

        public Yaku(string name, MenzenExclusiveOrDownfallOrNo callType, int hanCount)
        {
            Name = name;
            MenzenHanCount = hanCount;
            CallType = callType;
            switch (CallType)
            {
                case MenzenExclusiveOrDownfallOrNo.MenzenExclusive:
                    CalledHanCount = 0;
                    break;
                case MenzenExclusiveOrDownfallOrNo.Downfall:
                    CalledHanCount = hanCount - 1;
                    break;
                case MenzenExclusiveOrDownfallOrNo.NoDownfall:
                    CalledHanCount = hanCount;
                    break;
            }
        }
    }
}
