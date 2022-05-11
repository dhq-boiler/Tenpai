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
            ComputeWaitTiles();
        }

        public override bool Equals(object obj)
        {
            var a = obj is Single;
            if (!a)
                return false;
            var b = WaitTiles[0].EqualsRedSuitedTileIncluding((obj as Single).WaitTiles[0]);
            return a && b;
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

        public override IncompletedMeld Clone(IncompletedMeld.MeldStatus status)
        {
            var newObj = new Single(status);
            newObj._Set = new TileCollection(_Set);
            newObj._Existed = new TileCollection(_Existed);
            newObj._Waiting = new TileCollection(_Waiting);
            return newObj;
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
