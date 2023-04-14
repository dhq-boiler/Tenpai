using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenpai.Models.Tiles;

namespace Tenpai.Models.Yaku.Meld
{
    /// <summary>
    /// 国士無双十三面待ち
    /// </summary>
    public class ThirteenWait : IncompletedMeld
    {
        public ThirteenWait()
            : base()
        {
            SetWaits();
        }

        public ThirteenWait(MeldStatus status)
            : base(status)
        {
            SetWaits();
        }

        private void SetWaits()
        {
            _Waiting.Add(Tile.CreateInstance<Character_1>());
            _Waiting.Add(Tile.CreateInstance<Character_9>());
            _Waiting.Add(Tile.CreateInstance<Dot_1>());
            _Waiting.Add(Tile.CreateInstance<Dot_9>());
            _Waiting.Add(Tile.CreateInstance<Bamboo_1>());
            _Waiting.Add(Tile.CreateInstance<Bamboo_9>());
            _Waiting.Add(Tile.CreateInstance<East>());
            _Waiting.Add(Tile.CreateInstance<South>());
            _Waiting.Add(Tile.CreateInstance<West>());
            _Waiting.Add(Tile.CreateInstance<North>());
            _Waiting.Add(Tile.CreateInstance<White>());
            _Waiting.Add(Tile.CreateInstance<Green>());
            _Waiting.Add(Tile.CreateInstance<Red>());
        }

        public override IncompletedMeld Clone(MeldStatus status)
        {
            var newObj = new ThirteenWait(status);
            newObj._Set = new TileCollection(_Set);
            newObj._Existed = new TileCollection(_Existed);
            newObj._Waiting = new TileCollection(_Waiting);
            return newObj;
        }
    }
}
