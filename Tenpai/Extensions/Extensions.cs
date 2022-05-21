using System.Linq;
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
        public static bool ContainsRedSuitedTileIncluding(this Tile[] collection, Tile target)
        {
            foreach (var tile in collection)
            {
                if (tile.Code == target.Code)
                    return true;
            }
            return false;
        }

        public static bool SequenceEqualDoNotConsiderRotationAndOrder(this Tile[] tiles, Tile[] other)
        {
            if (tiles.Count() != other.Count())
                return false;
            for (int i = 0; i < tiles.Count(); i++)
            {
                var a = tiles[i];
                var b = other[i];
                if (!a.Code.Equals(b.Code)
                    || !((a is IRedSuitedTile rthis && b is IRedSuitedTile r && rthis.IsRedSuited == r.IsRedSuited) || !(a is IRedSuitedTile && b is IRedSuitedTile)))
                {
                    return false;
                }
            }
            return true;
        }

        public static TileCollection CloneAndUnion(this TileCollection collection, Tile[] targets)
        {
            var clone = new TileCollection(collection);
            clone.AddRange(targets);
            return clone;
        }
    }
}
