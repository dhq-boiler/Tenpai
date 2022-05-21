using System;
using System.Linq;
using Tenpai.Models.Tiles;
using Tenpai.Models.Yaku.Meld;

namespace Tenpai.Models.Yaku.Meld
{
    /// <summary>
    /// 槓子
    /// </summary>
    public class Quad : Meld
    {
        public Quad(Tile t1, Tile t2, Tile t3, Tile t4)
            : base()
        {
            _Existed.Add(t1);
            _Existed.Add(t2);
            _Existed.Add(t3);
            _Existed.Add(t4);
            _Set.Add(t1);
            _Set.Add(t2);
            _Set.Add(t3);
            _Set.Add(t4);
        }

        public KongType Type { get; set; }

        public int TypeAsInt { get { return (int)Type; } }

        public override int TextWidth()
        {
            switch(Type)
            {
                case KongType.ConcealedKong:
                    return 2 * 4;
                case KongType.LargeMeldedKong:
                    return 1 * 2 + 2 * 4;
                case KongType.SmallMeldedKong:
                    return 1 * 4 + 2 * 4;
                default:
                    throw new NotSupportedException();
            }
        }

        public override bool Equals(object obj)
        {
            return obj is Quad q && Tiles.SequenceEqual((obj as Quad).Tiles) && Type.Equals(q.Type);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class Quad<T> : Quad
        where T : Tile, new()
    {
        public Quad(KongType kongType)
            : base(Tile.CreateInstance<T>(),
                   Tile.CreateInstance<T>(),
                   Tile.CreateInstance<T>(),
                   Tile.CreateInstance<T>())
        {
            this.Type = kongType;
        }
    }

    public class Quad<T1, T2, T3, T4> : Quad
        where T1 : Tile, new()
        where T2 : Tile, new()
        where T3 : Tile, new()
        where T4 : Tile, new()
    {
        public Quad(KongType kongType)
            : base(Tile.CreateInstance<T1>(),
                   Tile.CreateInstance<T2>(),
                   Tile.CreateInstance<T3>(),
                   Tile.CreateInstance<T4>())
        {
            this.Type = kongType;
        }
    }
}
