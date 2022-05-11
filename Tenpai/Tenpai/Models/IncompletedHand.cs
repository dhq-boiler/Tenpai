using Reactive.Bindings;
using System.Linq;
using Tenpai.Models.Tiles;
using Tenpai.Yaku.Meld;

namespace Tenpai.Models
{
    public class IncompletedHand
    {
        public TileCollection Tiles { get; set; } = new TileCollection();

        public ReactiveCollection<Meld> Melds { get; set; } = new ReactiveCollection<Meld>();

        public Tile WaitingTile { get; set; }

        /// <summary>
        /// 合計翻数
        /// </summary>
        public int SumHanCount
        {
            get
            {
                return Yakus.Sum(x => x.HanCount);
            }
        }

        /// <summary>
        /// 役のコレクション
        /// </summary>
        public ReactiveCollection<Yaku.Yaku> Yakus { get; set; } = new ReactiveCollection<Yaku.Yaku>();
    }
}
