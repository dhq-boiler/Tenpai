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
    }
}
