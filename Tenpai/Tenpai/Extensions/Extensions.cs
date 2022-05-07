using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenpai.Models.Tiles;

namespace Tenpai.Extensions
{
    public static class Extensions
    {
        public static bool ContainsRedSuitedTileIncluding(this TileCollection collection, Tile target)
        {
            foreach (var tile in collection)
            {
                if (tile.Code == target.Code)
                    return true;
            }
            return false;
        }
    }
}
