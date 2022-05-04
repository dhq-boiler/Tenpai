using System;
using System.Diagnostics;
using System.Linq;
using Tenpai.Models.Tiles;

namespace Tenpai.Yaku.Meld
{
    /// <summary>
    /// 単騎待ちを表すクラス
    /// </summary>
    public class Single : IncompletedMeld
    {
        public Single()
            : base()
        { }

        public Single(MeldStatus status)
            : base(status)
        { }

        public Single(Tile tile)
            : base()
        {
            Tiles.Add(tile);
        }

        public override bool Equals(object obj)
        {
            return obj is Single
                && WaitTiles.SequenceEqual((obj as Single).WaitTiles);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void ComputeWaitTiles()
        {
            Debug.Assert(_Set.Count() == 1);
            _Waiting.Clear();
            _Waiting.Add(_Set.Single());
        }
    }

    public class Single<T> : Single
        where T : Tile, new()
    {
        public Single()
            :base(Tile.CreateInstance<T>())
        { }
    }
}
