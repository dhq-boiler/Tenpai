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
        public bool IsMenzenExclusive { get; set; }

        /// <summary>
        /// 翻数
        /// </summary>
        public int HanCount(bool called)
        {
            if (called && !IsMenzenExclusive)
                return CalledHanCount;
            else
                return MenzenHanCount;
        }

        public Yaku(string name, bool isMenzenExclusive, int hanCount)
        {
            Name = name;
            MenzenHanCount = hanCount;
            if (IsMenzenExclusive)
            {
                CalledHanCount = 0;
            }
            else
            {
                CalledHanCount = hanCount - 1;
            }
        }
    }
}
