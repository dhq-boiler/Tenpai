using System;
using System.Linq;
using Tenpai.Models.Tiles;

namespace Tenpai.Yaku.Meld
{
    /// <summary>
    /// 国士無双単騎待ち専用
    /// </summary>
    public class ThirteenWait : IncompletedMeld
    {
        public ThirteenWait()
            : base()
        { }

        public ThirteenWait(MeldStatus status)
            : base(status)
        { }

        public ThirteenWait(Tile wait)
            : this()
        {
            _Set.Add(wait);
            _Waiting.Add(wait);
        }

        public override void ComputeWaitTiles()
        {
            _Waiting.Clear();
            _Waiting.Add(_Set[0]);
        }

        public override bool Equals(object obj)
        {
            return obj is ThirteenWait
                && _Waiting.SequenceEqual((obj as ThirteenWait)._Waiting);
        }

        public override int GetHashCode()
        {
            return _Waiting.Single().GetHashCode();
        }

        public override string ToString()
        {
            return "(" + _Waiting[0].ToString() + ")";
        }
        public override IncompletedMeld Clone(IncompletedMeld.MeldStatus status)
        {
            var newObj = new ThirteenWait(status);
            newObj._Set = new TileCollection(_Set);
            newObj._Existed = new TileCollection(_Existed);
            newObj._Waiting = new TileCollection(_Waiting);
            return newObj;
        }
    }

    public class ThirteenWait<W> : ThirteenWait
        where W : Tile, new()
    {
        public ThirteenWait()
            : base(Tile.CreateInstance<W>())
        { }
    }
}
