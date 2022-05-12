﻿using System.Collections.Generic;
using System.Linq;
using Tenpai.Models;
using Tenpai.Models.Tiles;

namespace Tenpai.Models.Yaku.Meld
{
    /// <summary>
    /// 面子
    /// </summary>
    public abstract class Meld
    {
        protected TileCollection _Set;
        protected TileCollection _Existed;

        public Meld()
        {
            _Set = new TileCollection();
            _Existed = new TileCollection();
        }


        public Meld(Tile first, Tile second, Tile third)
            : this()
        {
            Add(first);
            Add(second);
            Add(third);
        }

        public TileCollection Tiles { get { return _Set; } }

        public virtual TileCollection AllTiles
        {
            get
            {
                var list = new List<Tile>();
                list.AddRange(_Set);
                list.Sort();
                return new TileCollection(list);
            }
        }

        public EOpponent? CallFrom
        {
            get
            {
                return Tiles.FirstOrDefault(x => x.CallFrom != EOpponent.Unknown
                                              && x.CallFrom != EOpponent.Default)?.CallFrom;
            }
        }


        public virtual int TextWidth()
        {
            return 1 * 2 + 2 * 3;
        }

        public void Add(Tile tile)
        {
            _Set.Add(tile);
        }

        public bool IsContained(TileCollection tile)
        {
            foreach (var t in _Set)
            {
                if (!tile.Any(a => a.Equals(t)))
                    return false;
            }
            return true;
        }
        
        public override int GetHashCode()
        {
            int hashcode = 0;
            foreach (var tile in Tiles)
            {
                hashcode ^= tile.Code.GetHashCode();
            }
            return hashcode;
        }
        private string ToStringCompleted()
        {
            string ret = "";
            foreach (var tile in Tiles)
            {
                ret += tile.ToString();
            }
            return ret;
        }

        public override string ToString()
        {
            return ToStringCompleted();
        }
    }
}
