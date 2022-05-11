namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 役の抽象クラス
    /// </summary>
    public abstract class Yaku
    {
        /// <summary>
        /// 役の名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 翻数
        /// </summary>
        public int HanCount { get; set; }
    }
}
