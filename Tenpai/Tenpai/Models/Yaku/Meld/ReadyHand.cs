using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using Tenpai.Extensions;
using Tenpai.Models.Tiles;

namespace Tenpai.Models.Yaku.Meld
{
    public class ReadyHand : WaitHand
    {
        public ReadyHand()
            : base()
        { }

        public ReadyHand(params Meld[] melds)
            : base(melds)
        { }

        public ReadyHand(Tile[] odd, params Meld[] melds)
            : base(melds)
        {
            Odd = odd;
        }

        /// <summary>
        /// 合計翻数
        /// </summary>
        public int SumHanCount(bool called)
        {
            return Yakus.Sum(x => x.HanCount(called));
        }

        /// <summary>
        /// 役のコレクション
        /// </summary>
        public ReactiveCollection<Tenpai.Models.Yaku.Yaku> Yakus { get; set; } = new ReactiveCollection<Tenpai.Models.Yaku.Yaku>();

        public Meld[] Waiting
        {
            get { return Melds.Where(a => 
                a is IncompletedMeld 
                && (a as IncompletedMeld).MeldStatusType == IncompletedMeld.MeldStatus.WAIT).ToArray(); }
        }

        public virtual Tile[] WaitingTiles
        {
            get
            {
                HashSet<Tile> wait = new HashSet<Tile>();
                foreach (var meld in Waiting.Cast<IncompletedMeld>())
                {
                    foreach (var waitTile in meld.ComputeWaitTiles(this.Melds))
                    {
                        wait.Add(waitTile);
                    }
                }
                return wait.ToArray();
            }
            set
            {
            }
        }

        public Tile[] Odd { get; private set; }

        public override bool Equals(object obj)
        {
            if (!(obj is ReadyHand))
                return false;
            var other = obj as ReadyHand;
            foreach (var meld in Melds)
            {
                if (!other.Melds.Contains(meld))
                    return false;
            }
            if (!WaitingTiles.SequenceEqualDoNotConsiderRotationAndOrder(other.WaitingTiles))
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            string str = base.ToString() + " 待ち：";
            foreach (var wait in WaitingTiles)
            {
                str += wait;
            }
            if (Odd != null)
            {
                str += " 余り：";
                foreach (var odd in Odd)
                {
                    str += odd;
                }
            }
            return str;
        }
    }
}
