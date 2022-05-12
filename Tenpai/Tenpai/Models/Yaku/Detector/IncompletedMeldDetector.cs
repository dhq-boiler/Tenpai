using System;
using System.Collections.Generic;
using System.Linq;
using Tenpai.Models.Tiles;

namespace Tenpai.Models.Yaku.Meld.Detector
{
    public static class IncompletedMeldDetector
    {
        /// <summary>
        /// targetが捨てられた時に自分の手牌から鳴ける牌の組み合わせを検索します．
        /// </summary>
        /// <param name="hand">手牌の牌リスト</param>
        /// <param name="target">捨て牌</param>
        /// <returns></returns>
        public static Meld[] FindCallable(Tile[] hand, Tile target)
        {
            var icRuns = FindIncompletedRuns(hand);
            var icTriples = FindIncompletedTriple(hand);
            var icQuads = FindIncompletedQuad(hand);

            var ret = new List<Meld>();

            foreach (var icRun in icRuns)
            {
                if (icRun.WaitTiles.Any(a => a.Equals(target)))
                    ret.Add(icRun);
            }

            foreach (var icTriple in icTriples)
            {
                if (icTriple.WaitTiles.Any(a => a.Equals(target)))
                    ret.Add(icTriple);
            }

            foreach (var icQuad in icQuads)
            {
                if (icQuad.Tiles[0].Equals(target))
                    ret.Add(icQuad);
            }

            return ret.ToArray();
        }

        /// <summary>
        /// 手牌からあと1つで対子が完成する1つの牌を検出します．
        /// </summary>
        /// <param name="hand">手牌の牌リスト</param>
        /// <returns></returns>
        public static IncompletedMeld[] FindIncompletedDoubles(Tile[] hand)
        {
            List<IncompletedMeld> ret = new List<IncompletedMeld>();
            TileCollection tiles = new TileCollection(hand.Where(a => !(a is Dummy)));

            foreach (var tile in tiles)
            {
                if (tiles.Count(a => a.Equals(tile)) == 1)
                    ret.Add(new Single(tile));
            }
            return ret.ToArray();
        }

        private static void Remove(TileCollection tiles, Meld[] melds)
        {
            foreach (var meld in melds)
            {
                foreach (var t in meld.Tiles)
                {
                    tiles.Remove(t);
                }
            }
        }

        /// <summary>
        /// 手牌からあと1つで刻子が完成する2つの牌の組み合わせを検出します．
        /// </summary>
        /// <param name="hand">手牌の牌リスト</param>
        /// <returns></returns>
        public static IncompletedMeld[] FindIncompletedTriple(Tile[] hand)
        {
            List<IncompletedMeld> ret = new List<IncompletedMeld>();
            var removeDummy = hand.Where(a => !(a is Dummy));
            var sorted = removeDummy.OrderBy(a => a.Code);
            int count = sorted.Count();
            for (int i = 0; i < count - 1; ++i)
            {
                var one = sorted.ElementAt(i);
                var another = sorted.ElementAt(i + 1);

                if (one.Code.Equals(another.Code))
                {
                    var pair = new Double();
                    pair.Add(one);
                    pair.Add(another);

                    if (!ret.Contains(pair))
                        ret.Add(pair);
                }
                else
                    continue;
            }

            return ret.ToArray();
        }

        /// <summary>
        /// 手牌からあと1つで順子が完成する2つの牌の組み合わせを検出します．
        /// </summary>
        /// <param name="hand">手牌の牌リスト</param>
        /// <returns></returns>
        public static IncompletedMeld[] FindIncompletedRuns(Tile[] hand)
        {
            List<IncompletedMeld> ret = new List<IncompletedMeld>();

            var distinctTiles = hand.Distinct().OrderBy(a => a);
            for (int i = 0; i < distinctTiles.Count(); ++i)
            {
                var one = distinctTiles.ElementAt(i);
                for (int j = i + 1; j < distinctTiles.Count(); ++j)
                {
                    var another = distinctTiles.ElementAt(j);
                    AddIncompletedMeldIfFound(ret, one, another);
                }
            }

            return ret.ToArray();
        }

        private static void AddIncompletedMeldIfFound(List<IncompletedMeld> ret, Tile one, Tile another)
        {
            if (one is Honors || another is Honors)
                return;
            else if (one.IsSameType(another))
            {
                if (Math.Abs(one.Code - another.Code) == 2)
                {
                    var wait = new ClosedWait(one, another);
                    wait.ComputeWaitTiles();

                    foreach (var waittile in wait.WaitTiles)
                    {
                        var newwait = new ClosedWait(one, another);
                        newwait.WaitTiles.Add(waittile);
                        ret.Add(newwait);
                    }
                }
                else if (Math.Abs(one.Code - another.Code) == 1)
                {
                    if (one is ITerminals || another is ITerminals)
                    {
                        var wait = new EdgeWait(one, another);
                        wait.ComputeWaitTiles();
                        ret.Add(wait);
                    }
                    else
                    {
                        var wait = new OpenWait(one, another);
                        wait.ComputeWaitTiles();

                        foreach (var waittile in wait.WaitTiles)
                        {
                            var newwait = new OpenWait(wait.Tiles[0], wait.Tiles[1]);
                            newwait.WaitTiles.Add(waittile);
                            ret.Add(newwait);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 手牌からあと１つで槓子が完成する3つの牌の組み合わせを検出します．
        /// </summary>
        /// <param name="hand">手牌の牌リスト</param>
        /// <returns></returns>
        public static Meld[] FindIncompletedQuad(Tile[] hand)
        {
            List<Meld> ret = new List<Meld>();
            var sorted = hand.OrderBy(a => a.Code);
            int count = hand.Count();
            for (int i = 0; i < count - 2; ++i)
            {
                var one   = sorted.ElementAt(i);

                if (hand.Count(a => a.Equals(one)) == 4)
                {
                    //既に手牌に4つそろっている牌については除外する
                    continue;
                }

                var two   = sorted.ElementAt(i + 1);
                var three = sorted.ElementAt(i + 2);

                if (one.EqualsRedSuitedTileIncluding(two) && one.EqualsRedSuitedTileIncluding(three))
                {
                    var triple = new Triple();
                    triple.Add(one);
                    triple.Add(two);
                    triple.Add(three);

                    if (!ret.Any(a => a.Equals(triple)))
                        ret.Add(triple);
                }
            }
            return ret.ToArray();
        }
    }
}
