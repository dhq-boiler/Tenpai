using System;
using System.Linq;
using Tenpai.Models.Tiles;
using Tenpai.Models.Yaku.Meld;

namespace Tenpai.Yaku.Meld
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
            return obj is Quad && Tiles.SequenceEqual((obj as Quad).Tiles);
        }

        public override int GetHashCode()
        {
            return Tiles[0].GetHashCode();
        }
    }
}
