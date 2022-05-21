using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenpai.Models.Tiles;
using Tenpai.Models.Yaku.Meld;

namespace Tenpai.Extensions
{
    internal class IncompletedMeldComparer : IEqualityComparer<IncompletedMeld>
    {
        public bool Equals(IncompletedMeld? x, IncompletedMeld? y)
        {
            if (x.Tiles.Count != y.Tiles.Count)
                return false;
            for (int i = 0; i < x.Tiles.Count(); i++)
            {
                var xt = x.Tiles[i];
                var yt = y.Tiles[i];
                if (xt.Code != yt.Code && (xt is IRedSuitedTile xtr && yt is IRedSuitedTile ytr && xtr.IsRedSuited != ytr.IsRedSuited))
                    return false;
            }
            if (x.WaitTiles.Count != y.WaitTiles.Count)
                return false;
            for (int i = 0; i < x.WaitTiles.Count(); i++)
            {
                var xt = x.WaitTiles[i];
                var yt = y.WaitTiles[i];
                if (xt.Code != yt.Code && (xt is not IRedSuitedTile || yt is not IRedSuitedTile || (xt is IRedSuitedTile xtr && yt is IRedSuitedTile ytr && xtr.IsRedSuited != ytr.IsRedSuited)))
                    return false;
            }
            return true;
        }

        public int GetHashCode([DisallowNull] IncompletedMeld obj)
        {
            return obj.GetHashCode();
        }
    }
}
