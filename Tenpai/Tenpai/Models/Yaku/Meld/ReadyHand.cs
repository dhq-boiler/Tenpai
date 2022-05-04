using System;
using System.Collections.Generic;
using System.Linq;
using Tenpai.Models.Tiles;

namespace Tenpai.Yaku.Meld
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

        public Meld[] Waiting
        {
            get { return Melds.Where(a => 
                a is IncompletedMeld 
                && (a as IncompletedMeld).MeldStatusType == IncompletedMeld.MeldStatus.WAIT).ToArray(); }
        }

        public Tile[] WaitingTiles
        {
            get
            {
                HashSet<Tile> wait = new HashSet<Tile>();
                foreach (var meld in Waiting.Cast<IncompletedMeld>())
                {
                    foreach (var waitTile in meld.WaitTiles)
                    {
                        wait.Add(waitTile);
                    }
                }
                return wait.ToArray();
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
