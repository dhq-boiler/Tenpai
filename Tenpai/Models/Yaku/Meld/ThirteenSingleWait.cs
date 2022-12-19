using System;
using System.Linq;
using Tenpai.Models.Tiles;

namespace Tenpai.Models.Yaku.Meld
{
    /// <summary>
    /// 国士無双単騎待ち専用
    /// </summary>
    public class ThirteenSingleWait : IncompletedMeld
    {
        public ThirteenSingleWait()
            : base()
        { }

        public ThirteenSingleWait(MeldStatus status)
            : base(status)
        { }

        public ThirteenSingleWait(Tile wait)
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

        public override void ComputeWaitTiles(TileCollection tiles2)
        {
            _Waiting.Clear(); 
            var red = Tile.CreateRedInstance(_Set.First().Code, System.Windows.Visibility.Visible, null, tiles2.Count(x => x.EqualsRedSuitedTileIncluding(_Set.First())));
            var normal = Tile.CreateInstance(_Set.First().Code, System.Windows.Visibility.Visible, null, tiles2.Count(x => x.EqualsRedSuitedTileIncluding(_Set.First())));

            if (tiles2.Where(x => x.EqualsConsiderCodeAndRed(red)).Count() == 0)
            {
                _Waiting.Add(red);
            }
            if (tiles2.Where(x => x.EqualsConsiderCodeAndRed(normal)).Count() <= 3)
            {
                _Waiting.Add(normal);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is ThirteenSingleWait
                && _Waiting.SequenceEqual((obj as ThirteenSingleWait)._Waiting);
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
            var newObj = new ThirteenSingleWait(status);
            newObj._Set = new TileCollection(_Set);
            newObj._Existed = new TileCollection(_Existed);
            newObj._Waiting = new TileCollection(_Waiting);
            return newObj;
        }
    }

    public class ThirteenWait<W> : ThirteenSingleWait
        where W : Tile, new()
    {
        public ThirteenWait()
            : base(Tile.CreateInstance<W>())
        { }
    }
}
