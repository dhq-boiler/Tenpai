using System;
using System.Linq;
using Tenpai.Models.Tiles;

namespace Tenpai.Yaku.Meld
{
    /// <summary>
    /// 槓子
    /// </summary>
    public class Quad : Meld
    {
        /// <summary>
        /// 暗槓用コンストラクタ
        /// </summary>
        /// <param name="t"></param>
        public Quad(Tile t)
            : base()
        {
            _Existed.Add(t);
            _Existed.Add(t);
            _Existed.Add(t);
            _Existed.Add(t);
            _Set.Add(t);
            _Set.Add(t);
            _Set.Add(t);
            _Set.Add(t);
            this.Type = KongType.ConcealedKong;
        }

        ///// <summary>
        ///// 大明槓用コンストラクタ
        ///// </summary>
        ///// <param name="discaded"></param>
        ///// <param name="from"></param>
        //public Quad(Tile discaded, EOpponent from)
        //    : base()
        //{
        //    _Existed.Add(discaded);
        //    _Existed.Add(discaded);
        //    _Existed.Add(discaded);
        //    _Set.Add(discaded);
        //    _Set.Add(discaded);
        //    _Set.Add(discaded);
        //    RotatedTile = discaded;
        //    GetFrom = from;
        //    this.Type = KongType.LargeMeldedKong;
        //}

        ///// <summary>
        ///// 小明槓用コンストラクタ
        ///// </summary>
        ///// <param name="add"></param>
        ///// <param name="pung"></param>
        //public Quad(Tile add, Triple pung)
        //    :base()
        //{
        //    _Existed.Add(pung.Tiles[0]);
        //    _Existed.Add(pung.Tiles[1]);
        //    _Existed.Add(pung.Tiles[2]);
        //    _Set.Add(pung.Tiles[0]);
        //    _Set.Add(pung.Tiles[1]);
        //    _Set.Add(pung.Tiles[2]);
        //    _Set.Add(add);
        //    RotatedTile = pung.RotatedTile;
        //    GetFrom = pung.GetFrom;
        //    this.Type = KongType.SmallMeldedKong;
        //}

        public KongType Type { get; private set; }

        public enum KongType
        {
            ConcealedKong,
            LargeMeldedKong,
            SmallMeldedKong
        }

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
