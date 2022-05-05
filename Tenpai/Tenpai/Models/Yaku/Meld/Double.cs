using System;
using System.Diagnostics;
using System.Linq;
using Tenpai.Models.Tiles;

namespace Tenpai.Yaku.Meld
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
            ComputeWaitTiles();
        }

        public override void ComputeWaitTiles()
        {
            Debug.Assert(_Set.Count() == 2);
            _Waiting.Clear();
            _Waiting.Add(_Set.First());
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
