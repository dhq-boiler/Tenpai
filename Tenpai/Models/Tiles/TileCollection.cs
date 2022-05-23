using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Tenpai.Extensions;
using Tenpai.Models.Yaku.Meld;

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

        public TileCollection(Tile[] wait, params Meld[] melds)
        {
            if (melds != null)
            {
                foreach (var meld in melds)
                {
                    this.AddRange(meld.Tiles);
                }
            }
            this.AddRange(wait);
        }

        public bool IsMoreThan4TilesOfTheSameType()
        {
            foreach (var tile in this)
            {
                if (this.Count(x => x.EqualsRedSuitedTileIncluding(tile)) > 4)
                    return true;
            }
            return false;
        }

        public bool RemoveTiles(Tile args, int v)
        {
            var count = this.Where(x => x != null && x.EqualsRedSuitedTileIncluding(args)).Count();
            int processed = 0;
            for (int i = 0; i < count && i < v; i++)
            {
                for (int j = 0; j < this.Count(); j++)
                {
                    var targetTile = this[j];
                    if (targetTile == null)
                        continue;
                    if (args.EqualsRedSuitedTileIncluding(targetTile))
                    {
                        this[j] = new Dummy();
                        processed++;
                    }
                    if (processed == v)
                    {
                        return true;
                    }
                }
                Arrange();
            }
            Arrange();
            return processed == v;
        }

        public void Arrange()
        {
            RemoveAll(x => x is null);
            Sort();
        }

        public void UpdateTile(Tile replaced, Tile target, int count)
        {
            int processedCount = 0;
            for (int j = 0; j < this.Count(); j++)
            {
                var tile = this[j];
                if (tile is null || tile.Visibility.Value == Visibility.Collapsed)
                    continue;
                if (tile.Equals(replaced) && tile.Visibility.Value != Visibility.Collapsed && processedCount < count)
                {
                    this[j] = target;
                    processedCount++;
                }
                if (processedCount == count)
                {
                    return;
                }
            }
        }

        public bool Has(Func<Tile, bool> condition)
        {
            return this.Any(t => condition.Invoke(t));
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
            for (int i = 0; i < melds.Count(); i++)
            {
                var meld = melds[i];
                for (int j = 0;  j < meld.Tiles.Count(); j++)
                {
                    var tile = meld.Tiles[j];
                    if (!tiles.ContainsRedSuitedTileIncluding(tile))
                        return false;
                    tiles.RemoveTiles(tile, 1);
                }
            }
            return true;
        }

        public Tile[] Odd(params Meld[] melds)
        {
            TileCollection tiles = new TileCollection(this);
            foreach (var meld in melds.Where(x => !(x is IncompletedMeld)))
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

        public override string ToString()
        {
            var str = string.Empty;
            foreach (var tile in this)
            {
                str += tile.ToString();
            }
            return str;
        }

        public A Enumerate<T>(int count) where T : Tile, new()
        {
            var collection = new TileCollection(this);
            return new A(collection.RemoveTiles(Tile.CreateInstance<T>(), count), collection);
        }

        public class A
        {
            public A(bool isEnable, TileCollection collection)
            {
                IsEnable = isEnable;
                this.collection = collection;
            }

            public bool IsEnable { get; set; }
            public TileCollection collection { get; set; }

            public A Enumerate<T>(int count) where T : Tile, new()
            {
                if (this.IsEnable)
                    return collection.Enumerate<T>(count);
                else
                    return new A(false, new TileCollection());
            }

            public bool Evaluate()
            {
                return this.IsEnable;
            }
        }
    }
}
