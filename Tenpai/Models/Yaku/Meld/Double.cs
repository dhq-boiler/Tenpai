using System;
using System.Diagnostics;
using System.Linq;
using Tenpai.Models.Tiles;

namespace Tenpai.Models.Yaku.Meld
{
    /// <summary>
    /// 対子
    /// </summary>
    public class Double : IncompletedMeld
    {
        public Double()
            : base()
        { }

        public Double(MeldStatus status)
            : base(status)
        { }

        public Double(Tile pairone)
            : base()
        {
            _Set.Add(pairone);
            _Set.Add(pairone);
        }

        public Double(Tile a, Tile b)
            : this(MeldStatus.COMPLETED)
        {
            _Set.Add(a);
            _Set.Add(b);
        }

        public override void ComputeWaitTiles()
        {
            Debug.Assert(_Set.Count() == 2);
            _Waiting.Clear();

            if (_Set.First() is IRedSuitedTile)
            {
                var red = Tile.CreateRedInstance(_Set.First().Code, System.Windows.Visibility.Visible, null, _Set.Count(x => x.EqualsRedSuitedTileIncluding(_Set.First())));
                if (_Set.Where(x => x.EqualsConsiderCodeAndRed(red)).Count() == 0)
                {
                    _Waiting.Add(red);
                }
            }
            var normal = Tile.CreateInstance(_Set.First().Code, System.Windows.Visibility.Visible, null, _Set.Count(x => x.EqualsRedSuitedTileIncluding(_Set.First())));
            if (_Set.Where(x => x.EqualsConsiderCodeAndRed(normal)).Count() < 3)
            {
                _Waiting.Add(normal);
            }
        }

        public override void ComputeWaitTiles(TileCollection tiles2)
        {
            Debug.Assert(_Set.Count() == 2);
            _Waiting.Clear();

            if (_Set.First() is IRedSuitedTile)
            {
                var red = Tile.CreateRedInstance(_Set.First().Code, System.Windows.Visibility.Visible, null, tiles2.Count(x => x.EqualsRedSuitedTileIncluding(_Set.First())));
                if (tiles2.Where(x => x.EqualsConsiderCodeAndRed(red)).Count() == 0)
                {
                    _Waiting.Add(red);
                }
            }
            var normal = Tile.CreateInstance(_Set.First().Code, System.Windows.Visibility.Visible, null, tiles2.Count(x => x.EqualsRedSuitedTileIncluding(_Set.First())));
            if (tiles2.Where(x => x.EqualsConsiderCodeAndRed(normal)).Count() <= 3)
            {
                _Waiting.Add(normal);
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Double && Tiles.SequenceEqual((obj as Double).Tiles);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override IncompletedMeld Clone(IncompletedMeld.MeldStatus status)
        {
            var newObj = new Double(status);
            newObj._Set = new TileCollection(_Set);
            newObj._Existed = new TileCollection(_Existed);
            newObj._Waiting = new TileCollection(_Waiting);
            return newObj;
        }
    }

    public class Double<T> : Double
        where T : Tile, new()
    {
        public Double()
            : base(Tile.CreateInstance<T>())
        { }
    }
}
