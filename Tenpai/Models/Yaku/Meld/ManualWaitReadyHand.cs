﻿using System.Linq;
using Tenpai.Extensions;
using Tenpai.Models.Tiles;

namespace Tenpai.Models.Yaku.Meld
{
    public class ManualWaitReadyHand : ReadyHand
    {
        public ManualWaitReadyHand()
            : base()
        { }

        public ManualWaitReadyHand(params Meld[] melds)
            : base(melds)
        { }

        public ManualWaitReadyHand(IncompletedMeld waitForm, Tile waitingTile, params Meld[] melds)
            : base(melds)
        {
            WaitingTiles = new Tile[] { waitingTile };
            WaitForm = new Meld[] { waitForm };
        }
        public ManualWaitReadyHand(Tile waitingTile, params Meld[] melds)
            : base(melds)
        {
            WaitingTiles = new Tile[] { waitingTile };
            WaitForm = melds.Where(x => x is IncompletedMeld).ToArray();
        }

        public ManualWaitReadyHand(Tile wait1, Tile wait2, params Meld[] melds)
            : base(melds)
        {
            WaitingTiles = new Tile[] { wait1, wait2 };
        }

        public ManualWaitReadyHand(IncompletedMeld waitForm, Tile[] waitTiles, params Meld[] melds)
            : base(melds)
        {
            WaitingTiles = waitTiles;
            WaitForm = new Meld[] { waitForm };
        }

        public ManualWaitReadyHand(Tile[] waitTiles, params Meld[] melds)
            : base(melds)
        {
            WaitingTiles = waitTiles;
            WaitForm = melds.Where(x => x is IncompletedMeld).ToArray();
        }

        private Tile[] _WaitTiles;
        public override Tile[] WaitingTiles
        {
            get { return _WaitTiles; }
            set { _WaitTiles = value; }
        }

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
            int hash = 0;
            foreach (var meld in Melds)
            {
                hash ^= meld.GetHashCode();
            }
            foreach (var wait in WaitingTiles)
            {
                if (wait is IRedSuitedTile r)
                {
                    hash ^= wait.GetHashCode() ^ (r.IsRedSuited ? 1 : 0);
                }
                else
                {
                    hash ^= wait.GetHashCode();
                }
            }
            return hash;
        }
    }
}
