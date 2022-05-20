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

        public ManualWaitReadyHand(Tile wait, params Meld[] melds)
            : base(melds)
        {
            WaitingTiles = new Tile[] { wait };
        }

        public ManualWaitReadyHand(Tile wait1, Tile wait2, params Meld[] melds)
            : base(melds)
        {
            WaitingTiles = new Tile[] { wait1, wait2 };
        }

        private Tile[] _WaitTiles;
        public override Tile[] WaitingTiles
        {
            get { return _WaitTiles; }
            set { _WaitTiles = value; }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ManualWaitReadyHand))
                return false;
            var other = obj as ManualWaitReadyHand;
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
            int hash = base.GetHashCode();
            foreach (var w in WaitingTiles)
            {
                hash ^= w.GetHashCode();
            }
            return hash;
        }
    }
}
