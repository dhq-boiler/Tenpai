using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenpai.Models.Tiles;

namespace Tenpai.ViewModels
{
    public class DoraDisplayTileCollection : TileCollection
    {
        public DoraDisplayTileCollection(params Tile[] tiles)
            : base(tiles)
        { }
    }
}
