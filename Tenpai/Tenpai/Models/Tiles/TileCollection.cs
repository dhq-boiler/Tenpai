using System.Collections.Generic;
using System.Linq;
using Tenpai.Yaku.Meld;

namespace Tenpai.Models.Tiles
{
    public class TileCollection : List<Tile>
    {
        public TileCollection() : base()
        { }

        public TileCollection(int capacity) : base(capacity)
        { }

        public TileCollection(IEnumerable<Tile> collection) : base(collection)
        { }

        public TileCollection(IEnumerable<Tile> collection, IEnumerable<Meld> exposed)
            : base(collection)
        {
            if (exposed != null)
            {
                foreach (var one in exposed)
                {
                    foreach (var t in one.Tiles)
                    {
                        this.Add(t);
                    }
                }
            }
        }

        public bool IsContained(TileCollection by)
        {
            foreach (var t in this)
            {
                if (!by.Any(a => a.Equals(t))) return false;
            }
            return true;
        }

        public static implicit operator Tile[](TileCollection tileCollection)
        {
            return tileCollection.ToArray();
        }

        public bool IsAllContained(params Meld[] melds)
        {
            TileCollection tiles = new TileCollection(this);
            foreach (var meld in melds)
            {
                foreach (var tile in meld.Tiles)
                {
                    if (!tiles.Contains(tile))
                        return false;
                    tiles.Remove(tile);
                }
            }
            return true;
        }

        public Tile[] Odd(params Meld[] melds)
        {
            TileCollection tiles = new TileCollection(this);
            foreach (var meld in melds)
            {
                foreach (var tile in meld.Tiles)
                {
                    if (!tiles.Contains(tile))
                        continue;
                    tiles.Remove(tile);
                }
            }
            return tiles.ToArray();
        }
    }
}
