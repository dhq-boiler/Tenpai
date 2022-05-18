using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Tenpai.Models.Tiles;

namespace Tenpai.Models.Yaku.Meld
{
    /// <summary>
    /// 両面待ちを表すクラス
    /// </summary>
    public class OpenWait : IncompletedMeld
    {
        public OpenWait()
            : base()
        { }

        public OpenWait(MeldStatus status)
            : base(status)
        { }

        public OpenWait(Tile have1, Tile have2)
            : base()
        {
            _Set.Add(have1);
            _Set.Add(have2);
            //ComputeWaitTiles();
        }

        public OpenWait(Tile wait1, Tile have1, Tile have2, Tile wait2)
            : base()
        {
            _Set.Add(have1);
            _Set.Add(have2);
            _Waiting.Add(wait1);
            _Waiting.Add(wait2);
        }

        public override void ComputeWaitTiles()
        {
            Debug.Assert(_Set.Count() == 2);
            Debug.Assert(_Set[0].Code != _Set[1].Code);
            Debug.Assert(!(_Set[0] is ITerminals));
            Debug.Assert(!(_Set[1] is ITerminals));
            _Waiting.Clear();
            if (_Set[0].Code < _Set[1].Code)
            {
                var tile1 = Tile.CreateInstance(_Set[0].Code - 1, Visibility.Visible, new RotateTransform(90));
                _Waiting.Add(tile1);
                if (tile1 is IRedSuitedTile r1)
                {
                    _Waiting.Add(Tile.CreateRedInstance(tile1.Code, Visibility.Visible, new RotateTransform(90)));
                }

                var tile2 = Tile.CreateInstance(_Set[1].Code + 1, Visibility.Visible, new RotateTransform(90));
                _Waiting.Add(tile2);
                if (tile2 is IRedSuitedTile r2)
                {
                    _Waiting.Add(Tile.CreateRedInstance(tile2.Code, Visibility.Visible, new RotateTransform(90)));
                }
            }
            else
            {
                var tile1 = Tile.CreateInstance(_Set[1].Code - 1, Visibility.Visible, new RotateTransform(90));
                _Waiting.Add(tile1);
                if (tile1 is IRedSuitedTile r1)
                {
                    _Waiting.Add(Tile.CreateRedInstance(tile1.Code, Visibility.Visible, new RotateTransform(90)));
                }

                var tile2 = Tile.CreateInstance(_Set[0].Code + 1, Visibility.Visible, new RotateTransform(90));
                _Waiting.Add(tile2);
                if (tile2 is IRedSuitedTile r2)
                {
                    _Waiting.Add(Tile.CreateRedInstance(tile2.Code, Visibility.Visible, new RotateTransform(90)));
                }
            }
        }

        public override bool Equals(object obj)
        {
            return obj is OpenWait
                && _Set.SequenceEqual((obj as OpenWait)._Set);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }
        public override IncompletedMeld Clone(IncompletedMeld.MeldStatus status)
        {
            var newObj = new OpenWait(status);
            newObj._Set = new TileCollection(_Set);
            newObj._Existed = new TileCollection(_Existed);
            newObj._Waiting = new TileCollection(_Waiting);
            return newObj;
        }
    }

    public class OpenWait<W1, T1, T2, W2> : OpenWait
        where W1 : Tile, new()
        where T1 : Tile, new()
        where T2 : Tile, new()
        where W2 : Tile, new()
    {
        public OpenWait()
            : base(Tile.CreateInstance<W1>(),
                   Tile.CreateInstance<T1>(),
                   Tile.CreateInstance<T2>(),
                   Tile.CreateInstance<W2>())
        { }
    }
}
