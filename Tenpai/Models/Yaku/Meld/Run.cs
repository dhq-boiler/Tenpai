using System;
using System.Linq;
using Tenpai.Models.Tiles;

namespace Tenpai.Models.Yaku.Meld
{
    /// <summary>
    /// 順子
    /// </summary>
    public class Run : Meld
    {
        public Run(Tile first, Tile second, Tile third)
            : base(first, second, third)
        { }

        public override bool Equals(object obj)
        {
            return obj is Run && Tiles.SequenceEqual((obj as Run).Tiles);
        }

        public override int GetHashCode()
        {
            int hash = 0;
            foreach (var tile in Tiles)
            {
                hash ^= tile.GetHashCode();
            }
            return hash;
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
    }

    public class Run<T1, T2, T3> : Run
        where T1 : Tile, new()
        where T2 : Tile, new()
        where T3 : Tile, new()
    {
        public Run()
            : base(Tile.CreateInstance<T1>(),
                   Tile.CreateInstance<T2>(),
                   Tile.CreateInstance<T3>())
        { }
    }
}
