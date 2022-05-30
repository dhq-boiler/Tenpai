using Prism.Mvvm;
using Reactive.Bindings;
using System.Windows.Input;

namespace Tenpai.Models.Yaku
{
    /// <summary>
    /// 役の抽象クラス
    /// </summary>
    public abstract class Yaku : BindableBase
    {
        /// <summary>
        /// 役の有効性
        /// </summary>
        public ReactivePropertySlim<bool> IsEnable { get; } = new ReactivePropertySlim<bool>();

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

        public ICommand CheckedCommand { get; set; }

        public ICommand UncheckedCommand { get; set; }

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

        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;
            return GetType().Equals(obj.GetType());
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }
}
