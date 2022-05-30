using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Tenpai.Models.Tiles;

namespace Tenpai.Models.Yaku.Meld
{
    /// <summary>
    /// 刻子
    /// </summary>
    public class Triple : Meld
    {
        public Triple()
            : base()
        { }

        //public Triple(MeldStatus status)
        //    : base(status)
        //{ }

        public Triple(Tile trio)
            : base()
        {
            for (int i = 0; i < 3; ++i)
            {
                _Set.Add(trio);
            }
            //ComputeWaitTiles();
        }

        public Triple(Tile a, Tile b, Tile c)
            : base()
        {
            _Set.Add(a);
            _Set.Add(b);
            _Set.Add(c);
            //ComputeWaitTiles();
        }

        //public override void ComputeWaitTiles()
        //{
        //    Debug.Assert(_Set.Count() == 3);
        //    Debug.Assert(_Set[0].Code == _Set[1].Code && _Set[0].Code == _Set[2].Code);
        //    _Waiting.Clear();
        //    _Waiting.Add(Tile.CreateInstance(_Set[0].Code, Visibility.Visible, null));
        //}

        public override bool Equals(object obj)
        {
            return obj is Triple && Tiles.SequenceEqual((obj as Triple).Tiles);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public override string ToString()
        {
            string ret = "";
            foreach (var tile in Tiles)
            {
                ret += tile.ToString();
            }
            return ret;
        }
        //public override IncompletedMeld Clone(IncompletedMeld.MeldStatus status)
        //{
        //    var newObj = new Triple(status);
        //    newObj._Set = new TileCollection(_Set);
        //    newObj._Existed = new TileCollection(_Existed);
        //    newObj._Waiting = new TileCollection(_Waiting);
        //    return newObj;
        //}
    }

    public class Triple<T> : Triple
        where T : Tile, new()
    {
        public Triple()
            : base(Tile.CreateInstance<T>())
        { }
    }
}
