using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tenpai.Extensions;
using Tenpai.Models;
using Tenpai.Models.Tiles;
using Tenpai.ViewModels;

namespace Tenpai.Models.Yaku.Meld
{
    /// <summary>
    /// 面子
    /// </summary>
    public abstract class Meld
    {
        protected TileCollection _Set;
        protected TileCollection _Existed;
        protected TileCollection _Waiting;

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

        public TileCollection WaitTiles
        {
            get
            {
                return _Waiting;
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

        public int HashCode
        {
            get { return GetHashCode(); }
        }

        public override int GetHashCode()
        {
            int hashcode = 0;
            hashcode ^= GetType().Name.GetHashCode();
            foreach (var tile in Tiles)
            {
                if (tile is IRedSuitedTile r)
                {
                    hashcode ^= tile.GetHashCode() ^ (r.IsRedSuited ? 1 : 0);
                }
                else
                {
                    hashcode ^= tile.GetHashCode();
                }
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

        public static IncompletedMeld operator- (Meld meld, Tile tile)
        {
            var tiles = meld.Tiles;
            tiles.Remove(tile);
            var a = tiles.ElementAt(0);
            var b = tiles.ElementAt(1);
            if (Math.Abs(a.Code - b.Code) == 2)
            {
                var wait = new ClosedWait(a, b);
                wait.ComputeWaitTiles();
                Debug.Assert(wait.WaitTiles.First().EqualsRedSuitedTileIncluding(tile));
                return wait;
            }
            else if (Math.Abs(a.Code - b.Code) == 1)
            {
                if (a is ITerminals || b is ITerminals)
                {
                    var wait = new EdgeWait(a, b);
                    wait.ComputeWaitTiles();
                    Debug.Assert(wait.WaitTiles.First().EqualsRedSuitedTileIncluding(tile));
                    return wait;
                }
                else
                {
                    var wait = new OpenWait(a, b);
                    wait.ComputeWaitTiles();
                    Debug.Assert(wait.WaitTiles.ContainsRedSuitedTileIncluding(tile));
                    return wait;
                }
            }
            throw new Exception("Something wrong!");
        }

        public bool HasYaku(WindOfTheRound windOfTheRound, OnesOwnWind onesOwnWind)
        {
            Debug.Assert(this is Double || this is Triple || this is Quad);
            return HasYakuByWindOfTheRound(windOfTheRound) || HasYakuByOnesOwnWind(onesOwnWind);
        }

        public bool HasYakuByOnesOwnWind(OnesOwnWind onesOwnWind)
        {
            if (onesOwnWind == OnesOwnWind.East && Tiles.First().EqualsRedSuitedTileIncluding(Tile.CreateInstance<East>()))
                return true;
            else if (onesOwnWind == OnesOwnWind.South && Tiles.First().EqualsRedSuitedTileIncluding(Tile.CreateInstance<South>()))
                return true;
            else if (onesOwnWind == OnesOwnWind.West && Tiles.First().EqualsRedSuitedTileIncluding(Tile.CreateInstance<West>()))
                return true;
            else if (onesOwnWind == OnesOwnWind.North && Tiles.First().EqualsRedSuitedTileIncluding(Tile.CreateInstance<North>()))
                return true;
            else
                return false;
        }

        public bool HasYakuByWindOfTheRound(WindOfTheRound windOfTheRound)
        {
            if (windOfTheRound == WindOfTheRound.East && Tiles.First().EqualsRedSuitedTileIncluding(Tile.CreateInstance<East>()))
                return true;
            else if (windOfTheRound == WindOfTheRound.South && Tiles.First().EqualsRedSuitedTileIncluding(Tile.CreateInstance<South>()))
                return true;
            else if (windOfTheRound == WindOfTheRound.West && Tiles.First().EqualsRedSuitedTileIncluding(Tile.CreateInstance<West>()))
                return true;
            else if (windOfTheRound == WindOfTheRound.North && Tiles.First().EqualsRedSuitedTileIncluding(Tile.CreateInstance<North>()))
                return true;
            else
                return false;
        }

        public static Meld operator +(Meld meld, Tile tile)
        {
            if (meld is OpenWait || meld is EdgeWait || meld is ClosedWait)
            {
                var tiles = new Tile[] { meld.Tiles[0], meld.Tiles[1], tile }.ToList();
                tiles.Sort();
                return new Run(tiles[0], tiles[1], tiles[2]);
            }
            else if (meld is Double d)
            {
                return new Triple(d.Tiles[0], d.Tiles[1], tile);
            }
            else if (meld is Single s)
            {
                return new Double(s.Tiles[0], tile);
            }
            else if (meld is ThirteenWait t)
            {
                return new Single(tile);
            }
            else
            {
                throw new Exception("Something wrong!");
            }
        }
    }
}
