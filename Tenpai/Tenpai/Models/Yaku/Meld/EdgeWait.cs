using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Tenpai.Models.Tiles;

namespace Tenpai.Yaku.Meld
{
    /// <summary>
    /// 辺張待ちを表すクラス
    /// </summary>
    public class EdgeWait : IncompletedMeld
    {
        public EdgeWait()
            : base()
        { }

        public EdgeWait(MeldStatus status)
            : base(status)
        { }

        public EdgeWait(Tile have1, Tile have2)
            : base()
        {
            _Set.Add(have1);
            _Set.Add(have2);
        }

        public EdgeWait(Tile have1, Tile have2, Tile wait1)
            : base()
        {
            _Set.Add(have1);
            _Set.Add(have2);
            _Waiting.Add(wait1);
        }

        public override void ComputeWaitTiles()
        {
            Debug.Assert(_Set.Count() == 2);
            var terminal = _Set.Where(a => a is ITerminals).Single();
            var next = _Set.Where(a => !(a is ITerminals)).Single();
            int terminal_hash = terminal.Code;
            int next_hash = next.Code;
            int diff = terminal_hash - next_hash;
            _Waiting.Clear();
            _Waiting.Add(Tile.CreateInstance(next_hash - diff, Visibility.Visible, new RotateTransform(90)));
        }

        public override bool Equals(object obj)
        {
            return obj is EdgeWait
                && WaitTiles.SequenceEqual((obj as EdgeWait).WaitTiles);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override IncompletedMeld Clone(IncompletedMeld.MeldStatus status)
        {
            var newObj = new EdgeWait(status);
            newObj._Set = new TileCollection(_Set);
            newObj._Existed = new TileCollection(_Existed);
            newObj._Waiting = new TileCollection(_Waiting);
            return newObj;
        }
    }

    public class EdgeWait<T1, T2, W1> : EdgeWait
        where T1 : Tile, new()
        where T2 : Tile, new()
        where W1 : Tile, new()
    {
        public EdgeWait()
            :base(Tile.CreateInstance<T1>(),
                  Tile.CreateInstance<T2>(),
                  Tile.CreateInstance<W1>())
        { }
    }
}
