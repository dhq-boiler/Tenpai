using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenpai.Models.Tiles;

namespace Tenpai.Extensions
{
    internal class TileComparer : IEqualityComparer<Tile>
    {
        public bool Equals(Tile? x, Tile? y)
        {
            return x.Code == y.Code
                && (!(x is IRedSuitedTile xr) && !(y is IRedSuitedTile yr)
                || (x is IRedSuitedTile xrr && y is IRedSuitedTile yrr && xrr.IsRedSuited == yrr.IsRedSuited));
        }

        public int GetHashCode([DisallowNull] Tile obj)
        {
            return obj.GetHashCode();
        }
    }
}
