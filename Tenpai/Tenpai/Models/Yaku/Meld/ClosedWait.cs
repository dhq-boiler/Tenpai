using System;
using System.Diagnostics;
using System.Linq;
using Tenpai.Models.Tiles;

namespace Tenpai.Yaku.Meld
{
    /// <summary>
    /// 嵌張待ちを表すクラス
    /// </summary>
    public class ClosedWait : IncompletedMeld
    {
        public ClosedWait()
            : base()
        { }

        public ClosedWait(MeldStatus status)
            : base(status)
        { }

        public ClosedWait(Tile have1, Tile have2)
            : base()
        {
            _Set.Add(have1);
            _Set.Add(have2);
        }

        public ClosedWait(Tile have1, Tile wait1, Tile have2)
            : base()
        {
            _Set.Add(have1);
            _Set.Add(have2);
            _Waiting.Add(wait1);
        }

        public override void ComputeWaitTiles()
        {
            Debug.Assert(_Set.Count() == 2);
            var tiles = _Set.ToArray();
            _Waiting.Clear();
            var tile = Tile.CreateInstance((int)tiles.Average(a => a.Code));
            _Waiting.Add(tile);
            if (tile is IRedSuitedTile r)
            {
                tile = Tile.CreateRedInstance(tile.Code);
                _Waiting.Add(tile);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is ClosedWait 
                && WaitTiles.SequenceEqual((obj as ClosedWait).WaitTiles);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override IncompletedMeld Clone(IncompletedMeld.MeldStatus status)
        {
            var newObj = new ClosedWait(status);
            newObj._Set = new TileCollection(_Set);
            newObj._Existed = new TileCollection(_Existed);
            newObj._Waiting = new TileCollection(_Waiting);
            return newObj;
        }

    }

    public class ClosedWait<T1, W1, T2> : ClosedWait
        where T1 : Tile, new()
        where W1 : Tile, new()
        where T2 : Tile, new()
    {
        public ClosedWait()
            : base(Tile.CreateInstance<T1>(),
                   Tile.CreateInstance<W1>(),
                   Tile.CreateInstance<T2>())
        { }
    }
}
