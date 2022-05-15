﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
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

        public void RemoveTiles(Tile args, int v)
        {
            var count = this.Where(x => x != null && x.CompareTo(args) == 0).Count();
            int processed = 0;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < this.Count(); j++)
                {
                    var targetTile = this[j];
                    if (targetTile == null)
                        continue;
                    if (args.CompareTo(targetTile) == 0)
                    {
                        this[j] = null;
                        processed++;
                    }
                    if (processed == v)
                    {
                        Arrange();
                        return;
                    }
                }
                Arrange();
            }
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
    }
}
