﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tenpai.Extensions;
using Tenpai.Models.Tiles;
using Tenpai.Models.Yaku.Meld;
using Tenpai.Utils;

namespace Tenpai.Models.Yaku.Meld.Detector
{
    public static class MeldDetector
    {
        public static CompletedHand[] FindCompletedHands(Tile[] hand, Meld[] exposed, int tileCount, ViewModels.AgariType agariType, Tile agariTile, ViewModels.WindOfTheRound windOfTheRound, ViewModels.OnesOwnWind onesOwnWind)
        {
            List<CompletedHand> ret = new List<CompletedHand>();
            TileCollection allTiles = new TileCollection(hand, exposed);

            var handCollection = new TileCollection(hand);
            handCollection.RemoveTiles(agariTile, 1);
            var handArr = handCollection.ToArray();

            var incompletedHands = FindReadyHands(handArr, exposed, tileCount, agariType, windOfTheRound, onesOwnWind);

            foreach (var incompletedHand in incompletedHands)
            {
                if (incompletedHand.WaitingTiles.ContainsRedSuitedTileIncluding(agariTile))
                {
                    var completedHand = new CompletedHand(incompletedHand.Melds.ToArray());

                    //国士無双型
                    if (IsThirteenOrphans(completedHand, out var singleCount, out var doubleCount))
                    {
                        //国士無双単騎待ち
                        if (singleCount == 11 && doubleCount == 1)
                        {
                            Console.WriteLine($"国士無双単騎待ち {completedHand}");
                            foreach (var meld in incompletedHand.Melds)
                            {
                                if (meld is IncompletedMeld im)
                                {
                                    if (im is ThirteenWait tw)
                                    {
                                        completedHand.WaitForm = new Meld[] { tw };
                                        var replace = completedHand.WaitForm.FirstOrDefault(x => x.WaitTiles.ContainsRedSuitedTileIncluding(agariTile)) as IncompletedMeld;
                                        int index = completedHand.Melds.ToList().IndexOf(replace);
                                        completedHand.Melds[index] = replace + agariTile;
                                    }
                                }
                            }
                            ret.Add(completedHand);
                        }
                        //国士無双十三面待ち
                        else if (singleCount == 13 && doubleCount == 0)
                        {
                            Console.WriteLine($"国士無双十三面待ち {completedHand}");
                            var waitForms = new List<Meld>();
                            foreach (var meld in incompletedHand.Melds)
                            {
                                waitForms.Add((meld as IncompletedMeld).Clone(IncompletedMeld.MeldStatus.WAIT));
                            }

                            foreach (var meld in incompletedHand.Melds)
                            {
                                if (meld.WaitTiles.ContainsRedSuitedTileIncluding(agariTile))
                                {
                                    completedHand = new CompletedHand(incompletedHand.Melds.ToArray());
                                    completedHand.WaitForm = waitForms.ToArray();
                                    var replace = meld;
                                    int index = completedHand.Melds.ToList().IndexOf(replace);
                                    completedHand.Melds[index] = replace + agariTile;
                                    completedHand.Melds.Where(x => !x.Equals(replace + agariTile)).ToList().ForEach(x => (x as IncompletedMeld).MeldStatusType = IncompletedMeld.MeldStatus.COMPLETED);
                                    ret.Add(completedHand);
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var meld in incompletedHand.Melds)
                        {
                            if (meld is IncompletedMeld im)
                            {
                                if (im is Single s)
                                {
                                    completedHand.WaitForm = new Meld[] { im };
                                    //var replace = completedHand.WaitForm.First(x => x.WaitTiles.ContainsRedSuitedTileIncluding(agariTile));
                                    var replace = im;
                                    int index = completedHand.Melds.ToList().IndexOf(replace);
                                    completedHand.Melds[index] = replace + agariTile;
                                }
                                else if (im is Double && incompletedHand.Melds.Count(x => x is Double) == 6)
                                {
                                    //七対子の形、対子にヒット
                                }
                                else if (im is OpenWait || im is EdgeWait || im is ClosedWait)
                                {
                                    Debug.Assert(im.WaitTiles.ContainsRedSuitedTileIncluding(agariTile));
                                    //im = im.Clone(IncompletedMeld.MeldStatus.WAIT) as IncompletedMeld;
                                    completedHand.WaitForm = new Meld[] { im /*+ agariTile*/ };
                                    var replace = completedHand.WaitForm.FirstOrDefault(x => x.WaitTiles.ContainsRedSuitedTileIncluding(agariTile)) as IncompletedMeld;
                                    int index = completedHand.Melds.ToList().IndexOf(replace);
                                    completedHand.Melds[index] = replace + agariTile;
                                }
                                else if (im is Double d)
                                {
                                    if (completedHand.WaitForm is null)
                                    {
                                        im = im.Clone(IncompletedMeld.MeldStatus.WAIT) as Double;
                                        im.ComputeWaitTiles();
                                        completedHand.WaitForm = new Meld[] { im };
                                    }
                                    else if (completedHand.WaitForm[0] is Double)
                                    {
                                        im = im.Clone(IncompletedMeld.MeldStatus.WAIT) as Double;
                                        im.ComputeWaitTiles();
                                        completedHand.WaitForm = new Meld[] { completedHand.WaitForm[0], im };
                                        var replace = completedHand.WaitForm.FirstOrDefault(x => x.WaitTiles.ContainsRedSuitedTileIncluding(agariTile)) as IncompletedMeld;
                                        int index = completedHand.Melds.ToList().IndexOf(replace);
                                        completedHand.Melds[index] = replace + agariTile;
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                else
                                {
                                    completedHand.WaitForm = new Meld[] { im };
                                }
                            }
                        }
                        ret.Add(completedHand);
                    }
                }
            }

            var runs = FindRuns(hand);
            var triples = FindTriples(hand);
            var heads = FindDoubles(hand);
            var singles = FindSingles(hand);

            AddYaku(ref ret, hand, exposed, runs, triples, heads, singles, agariType, windOfTheRound, onesOwnWind);

            return ret.Distinct().ToArray();
        }

        private static bool IsThirteenOrphans(CompletedHand completedHand, out int singleCount, out int doubleCount)
        {
            singleCount = 0;
            doubleCount = 0;
            foreach (var meld in completedHand.Melds)
            {
                if (meld is Single s)
                {
                    if (!s.Tiles.Any(x => x is ITerminals || x is Honors))
                    {
                        Console.WriteLine($"一九字牌でない牌を検出しました");
                        return false;
                    }
                    singleCount++;
                }
                else if (meld is Double d)
                {
                    if (!d.Tiles.Any(x => x is ITerminals || x is Honors))
                    {
                        Console.WriteLine($"一九字牌でない牌を検出しました");
                        return false;
                    }
                    doubleCount++;
                }
            }
            var isThirteenWait = singleCount == 13 && doubleCount == 0;
            var isSingleWait = singleCount == 11 && doubleCount == 1;
            return isThirteenWait || isSingleWait;
        }

        private static void CompletedHandsBasicForm(Tile[] hand, Meld[] exposed, List<CompletedHand> ret, Meld[] runs, Meld[] triples, Meld[] heads, Tile agariTile)
        {
            int meldCount = triples.Count() + runs.Count();
            if (exposed != null)
                meldCount += exposed.Count();

            if (heads.Count() >= 1 && meldCount >= 4)
            {
                var melds = new List<Meld>();
                melds.AddRange(triples);
                melds.AddRange(runs);

                TileCollection tiles = new TileCollection(hand);
                if (exposed != null)
                {
                    melds.AddRange(exposed);
                    foreach (var one in exposed)
                    {
                        tiles.AddRange(one.Tiles);
                    }
                }

                for (int l = 0; l < heads.Count(); ++l)
                {
                    var head = (heads[l] as Double).Clone(IncompletedMeld.MeldStatus.COMPLETED);
                    if (!tiles.IsAllContained(head))
                        continue;

                    for (int m = 0; m < melds.Count(); ++m)
                    {
                        var first = melds[m];
                        if (!tiles.IsAllContained(head, first))
                            continue;

                        for (int n = m + 1; n < melds.Count(); ++n)
                        {
                            var second = melds[n];
                            if (!tiles.IsAllContained(head, first, second))
                                continue;

                            for (int o = n + 1; o < melds.Count(); ++o)
                            {
                                var third = melds[o];
                                if (!tiles.IsAllContained(head, first, second, third))
                                    continue;

                                for (int p = o + 1; p < melds.Count(); ++p)
                                {
                                    var fourth = melds[p];
                                    if (!tiles.IsAllContained(head, first, second, third, fourth))
                                        continue;

                                    var completeHand = new CompletedHand(head, first, second, third, fourth);
                                    if (first.Tiles.Contains(agariTile))
                                    {
                                        completeHand.WaitForm = new IncompletedMeld[] { first - agariTile };
                                    }
                                    else if (second.Tiles.Contains(agariTile))
                                    {
                                        completeHand.WaitForm = new IncompletedMeld[] { second - agariTile };
                                    }
                                    else if (third.Tiles.Contains(agariTile))
                                    {
                                        completeHand.WaitForm = new IncompletedMeld[] { third - agariTile };
                                    }
                                    else if (fourth.Tiles.Contains(agariTile))
                                    {
                                        completeHand.WaitForm = new IncompletedMeld[] { fourth - agariTile };
                                    }

                                    ret.Add(completeHand);
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void CompletedHandsThirteenOrphans(List<CompletedHand> ret, Meld[] runs, Meld[] triples, Meld[] heads, Meld[] singles)
        {
            List<Tile> ThirteenOrphans = ThirteenOrphansTiles();
            if (runs.Count() == 0 && triples.Count() == 0 && heads.Count() == 1 && singles.Count() == 12)
            {
                List<Meld> x = new List<Meld>();
                foreach (var single in singles)
                {
                    if (!ThirteenOrphans.Contains(single.Tiles.Single()))
                        return;

                    x.Add((single as IncompletedMeld).Clone(IncompletedMeld.MeldStatus.COMPLETED));
                    ThirteenOrphans.Remove(single.Tiles.Single());
                }

                if (ThirteenOrphans.Count() != 1)
                    return;

                var to = ThirteenOrphans.Single();
                var head = heads.Single();
                if (!head.Tiles.First().Equals(to))
                    return;

                x.Add((head as Double).Clone(IncompletedMeld.MeldStatus.COMPLETED));
                ret.Add(new CompletedHand(x.ToArray()));
            }
        }

        private static void CompletedHandsSevenPairs(Meld[] exposed, List<CompletedHand> ret, Meld[] heads, Meld[] singles)
        {
            if (heads.Count() == 7 && singles.Count() == 0 && (exposed == null || exposed.Count() == 0))
            {
                List<Meld> form = new List<Meld>();
                foreach (var head in heads)
                {
                    form.Add((head as Double).Clone(IncompletedMeld.MeldStatus.COMPLETED));
                }
                ret.Add(new CompletedHand(form.ToArray()));
            }
        }

        /// <summary>
        /// 聴牌形を列挙します．
        /// </summary>
        /// <param name="hand">手牌の牌リスト</param>
        /// <param name="exposed">晒し牌のリスト</param>
        /// <returns></returns>
        public static ReadyHand[] FindReadyHands(Tile[] hand, Meld[] exposed, int tileCount, ViewModels.AgariType agariType, ViewModels.WindOfTheRound windOfTheRound, ViewModels.OnesOwnWind onesOwnWind)
        {
            hand = hand.Where(x => !(x is Dummy)).ToArray();
            var tcount = hand.Count() + (exposed != null ? exposed.Select(x => x.Tiles.Count()).Sum() : 0);
            if (tcount != tileCount)
                return Array.Empty<ReadyHand>();

            List<ReadyHand> ret = new List<ReadyHand>();

            var runs = FindRuns(hand);
            var triples = FindTriples(hand);
            var heads = FindDoubles(hand);
            var singles = FindSingles(hand);

            //七対子
            ReadyHandsSevenPairs(hand, exposed, ref ret, heads, singles);

            //純正国士無双（国士無双13面待ち）
            ReadyHandsPureThirteenOrphans(hand, ref ret, runs, triples, heads, singles);

            //国士無双単騎待ち
            ReadyHandsThirteenOrphansSingleWait(hand, ref ret, runs, triples, heads, singles);
            
            //4面子1雀頭
            ReadyHandsBasicForm(hand, exposed, ref ret, runs, triples, heads, singles);

            AddYaku(ref ret, hand, exposed, runs, triples, heads, singles, agariType, windOfTheRound, onesOwnWind);

            return ret.Distinct().ToArray();
        }

        private static void AddYaku<T>(ref List<T> ret, Tile[] hand, Meld[] exposed, Meld[] runs, Meld[] triples, Meld[] heads, Meld[] singles, ViewModels.AgariType agariType, ViewModels.WindOfTheRound windOfTheRound, ViewModels.OnesOwnWind onesOwnWind) where T : ReadyHand
        {
            foreach (var rh in ret)
            {
                var isMenzen = exposed == null || exposed.Where(x => x is Run || x is Triple || (x is Quad quad && quad.Type != KongType.ConcealedKong)).Count() == 0;
                var isTumo = agariType == ViewModels.AgariType.Tsumo;
                if (isMenzen && isTumo)
                {
                    //門前清自摸和
                    rh.Yakus.Add(new ConcealedSelfDraw());
                }

                var runsAreThree = rh.Melds.Where(x => x is Run).Count() == 3;
                var allRuns = rh.Melds.Except(rh.Melds.Where(x => x is Double)).All(x => x is Run || x is OpenWait);
                var headIsNotYakuhai = (rh.Melds.Count(x => x is Double) == 1) ? !rh.Melds.First(x => x is Double).HasYaku(windOfTheRound, onesOwnWind) : false;
                if (isMenzen && runsAreThree && allRuns && headIsNotYakuhai)
                {
                    //平和
                    rh.Yakus.Add(new AllRuns());
                }

                int doubleRunsCount = 0;
                var except = new List<Meld>();
                foreach (var meld in rh.Melds.Except(except))
                {
                    if (rh.Melds.Where(x => x.Equals(meld)).Count() == 2)
                    {
                        except.Add(meld);
                        doubleRunsCount++;
                    }
                }
                
                if (doubleRunsCount == 1)
                {
                    //一盃口

                }
                else if (doubleRunsCount == 2)
                {
                    //二盃口
                }
            }
        }

        private static void ReadyHandsBasicForm(Tile[] hand, Meld[] exposed, ref List<ReadyHand> ret, Meld[] runs, Meld[] triples, Meld[] heads, Meld[] singles)
        {
            var melds = new List<Meld>();
            melds.AddRange(triples);
            melds.AddRange(runs);

            if (exposed != null)
            {
                foreach (var one in exposed.Where(a => !(a is IncompletedMeld)).Cast<Meld>())
                {
                    melds.Add(one);
                }
                foreach (var one in exposed.Where(a => a is IncompletedMeld).Cast<IncompletedMeld>())
                {
                    melds.Add(one.Clone(IncompletedMeld.MeldStatus.COMPLETED));
                }
            }

            //1雀頭完成済み
            for (int l = 0; l < heads.Count(); ++l)
            {
                List<Meld> havingMelds = new List<Meld>();
                TileCollection tiles = new TileCollection(hand);
                if (exposed != null)
                {
                    foreach (var one in exposed)
                    {
                        tiles.AddRange(one.Tiles);
                    }
                }

                var head = heads[l];

                var selectedMelds = Combination.Enumerate(melds, 3, withRepetition: true).ToList();

                for (int i = 0; i < selectedMelds.Count; ++i)
                {
                    var selectedMeld = selectedMelds[i];
                    if (!tiles.IsAllContained(head, selectedMeld[0], selectedMeld[1], selectedMeld[2]))
                        continue;

                    var icRuns = IncompletedMeldDetector.FindIncompletedRuns(hand);
                    var icTriples = IncompletedMeldDetector.FindIncompletedTriple(hand);
                    var icMelds = new List<IncompletedMeld>();
                    icMelds.AddRange(icRuns);
                    icMelds.AddRange(icTriples);
                    icMelds = icMelds.Distinct(new IncompletedMeldComparer()).ToList();
                    for (int p = 0; p < icMelds.Count(); ++p)
                    {
                        var wait = icMelds[p];
                        if (!tiles.IsAllContained(head, selectedMeld[0], selectedMeld[1], selectedMeld[2], wait))
                            continue;

                        var odd = tiles.Odd(head, selectedMeld[0], selectedMeld[1], selectedMeld[2], wait);

                        if (wait.MeldStatusType == IncompletedMeld.MeldStatus.WAIT)
                        {
                            wait.ComputeWaitTiles();
                        }

                        foreach (var w in wait.WaitTiles)
                        {
                            //1雀頭・3面子完成済み，1搭子
                            ret.Add(new ManualWaitReadyHand(w, (head as IncompletedMeld).Clone(IncompletedMeld.MeldStatus.COMPLETED), selectedMeld[0], selectedMeld[1], selectedMeld[2], wait.Clone(IncompletedMeld.MeldStatus.WAIT)));
                        }
                    }
                }
            }

            //1雀頭完成待ち
            for (int l = 0; l < singles.Count(); ++l)
            {
                TileCollection tiles = new TileCollection(hand);
                if (exposed != null)
                {
                    foreach (var one in exposed)
                    {
                        tiles.AddRange(one.Tiles);
                    }
                }
                var wait = singles[l];

                var selectedMelds = Combination.Enumerate(melds, 4, withRepetition: true);

                foreach (var selectedMeld in selectedMelds)
                {
                    if (!tiles.IsAllContained(wait, selectedMeld[0], selectedMeld[1], selectedMeld[2], selectedMeld[3]))
                        continue;

                    var odd = tiles.Odd(wait, selectedMeld[0], selectedMeld[1], selectedMeld[2], selectedMeld[3]);

                    //4面子完成済み，1雀頭完成待ち
                    ret.Add(new ReadyHand(odd, (wait as Single).Clone(IncompletedMeld.MeldStatus.WAIT), selectedMeld[0], selectedMeld[1], selectedMeld[2], selectedMeld[3]));
                }
            }

            //2対子完成
            var listHeads = new List<Meld>();
            listHeads.AddRange(heads);
            var twoHeads = Combination.Enumerate(listHeads, 2, withRepetition: true).ToList();

            var selectedMelds2 = Combination.Enumerate(melds, 3, withRepetition: true).ToList();
            TileCollection tiles2 = new TileCollection(hand);

            for (int j = 0; j < twoHeads.Count; j++)
            {
                var twoHead = twoHeads[j];

                for (int i = 0; i < selectedMelds2.Count; ++i)
                {
                    var selectedMeld = selectedMelds2[i];
                    if (!tiles2.IsAllContained(twoHead[0], twoHead[1], selectedMeld[0], selectedMeld[1]))
                        continue;

                    var icRuns = IncompletedMeldDetector.FindIncompletedRuns(hand);
                    var icTriples = IncompletedMeldDetector.FindIncompletedTriple(hand);
                    var icMelds = new List<IncompletedMeld>();
                    icMelds.AddRange(icRuns);
                    icMelds.AddRange(icTriples);
                    icMelds = icMelds.Distinct(new IncompletedMeldComparer()).ToList();
                    for (int p = 0; p < icMelds.Count(); ++p)
                    {
                        if (!tiles2.IsAllContained(twoHead[0], twoHead[1], selectedMeld[0], selectedMeld[1], selectedMeld[2]))
                            continue;

                        //1雀頭・3面子完成済み，1搭子
                        ret.Add(new ManualWaitReadyHand(twoHead[1].Tiles[0], (twoHead[1] as IncompletedMeld).Clone(IncompletedMeld.MeldStatus.WAIT), (twoHead[0] as IncompletedMeld).Clone(IncompletedMeld.MeldStatus.COMPLETED), selectedMeld[0], selectedMeld[1], selectedMeld[2]));

                        //1雀頭・3面子完成済み，1搭子
                        ret.Add(new ManualWaitReadyHand(twoHead[0].Tiles[0], (twoHead[0] as IncompletedMeld).Clone(IncompletedMeld.MeldStatus.WAIT), (twoHead[1] as IncompletedMeld).Clone(IncompletedMeld.MeldStatus.COMPLETED), selectedMeld[0], selectedMeld[1], selectedMeld[2]));
                    }
                }
            }

            ret = ret.Distinct(new DelegateComparer<ReadyHand, Tile[]>(x => x.WaitingTiles)).ToList();
        }

        private static void ReadyHandsSevenPairs(Tile[] hand, Meld[] exposed, ref List<ReadyHand> ret, Meld[] heads, Meld[] singles)
        {
            if (heads.Count() == 6 && singles.Count() == 1 && (exposed == null || exposed.Count() == 0))
            {
                List<Meld> form = new List<Meld>();
                IncompletedMeld s = null;
                foreach (var head in heads)
                {
                    form.Add((head as Double).Clone(IncompletedMeld.MeldStatus.COMPLETED));
                }

                foreach (var single in singles)
                {
                    s = (single as Single).Clone(IncompletedMeld.MeldStatus.WAIT);
                    form.Add(s);
                }

                var odd = new TileCollection(hand).Odd(form.ToArray());
                ret.Add(new ManualWaitReadyHand(s.Tiles[0], form.ToArray()));
            }
        }

        private static void ReadyHandsThirteenOrphansSingleWait(Tile[] hand, ref List<ReadyHand> ret, Meld[] runs, Meld[] triples, Meld[] heads, Meld[] singles)
        {
            List<Tile> ThirteenOrphans = ThirteenOrphansTiles();
            List<Meld> x = new List<Meld>();

            if (singles.Count() == 11 && heads.Count() == 1 && triples.Count() == 0 && runs.Count() == 0)
            {
                foreach (var single in singles)
                {
                    if (!ThirteenOrphans.Contains(single.Tiles.Single()))
                        return;

                    x.Add((single as Single).Clone(IncompletedMeld.MeldStatus.COMPLETED));
                }

                foreach (var xx in x)
                {
                    ThirteenOrphans.Remove(xx.Tiles.Single());
                }

                var head = heads.Single();
                if (!ThirteenOrphans.Contains(head.Tiles.First()))
                    return;

                x.Add((head as IncompletedMeld).Clone(IncompletedMeld.MeldStatus.COMPLETED));
                ThirteenOrphans.Remove(head.Tiles.First());

                var wait = new ThirteenWait(ThirteenOrphans.Single());
                x.Add(wait.Clone(IncompletedMeld.MeldStatus.WAIT));

                var odd = new TileCollection(hand).Odd(x.ToArray());
                ret.Add(new ReadyHand(odd, x.ToArray()));
            }
        }

        private static List<Tile> ThirteenOrphansTiles()
        {
            return new List<Tile>(new Tile[]
            {
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_9>(),
                Tile.CreateInstance<East>(),
                Tile.CreateInstance<South>(),
                Tile.CreateInstance<West>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<White>(),
                Tile.CreateInstance<Green>(),
                Tile.CreateInstance<Red>()
            });
        }

        private static void ReadyHandsPureThirteenOrphans(Tile[] hand, ref List<ReadyHand> ret, Meld[] runs, Meld[] triples, Meld[] heads, Meld[] singles)
        {
            if (runs.Count() == 0 && triples.Count() == 0 && heads.Count() == 0 && singles.Count() == 13)
            {
                if (!AllThirteenOrphansContains(singles))
                    return;
                foreach (var s in singles)
                {
                    var h = new ManualWaitReadyHand(singles);
                    h.WaitingTiles = s.Tiles;
                    ret.Add(h);
                }
            }
        }

        private static bool AllThirteenOrphansContains(Meld[] singles)
        {
            List<Tile> ThirteenOrphans = ThirteenOrphansTiles();
            foreach (var tile in ThirteenOrphans)
            {
                if (!singles.Contains(new Single(tile)))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 手牌から1枚しかない牌を検出します．
        /// </summary>
        /// <param name="hand">手牌の牌リスト</param>
        /// <returns></returns>
        public static Meld[] FindSingles(Tile[] hand)
        {
            return IncompletedMeldDetector.FindIncompletedDoubles(hand);
        }

        /// <summary>
        /// 手牌から対子を検出します．
        /// </summary>
        /// <param name="hand">手牌の牌リスト</param>
        /// <returns></returns>
        public static Meld[] FindDoubles(Tile[] hand)
        {
            return IncompletedMeldDetector.FindIncompletedTriple(hand);
        }

        /// <summary>
        /// 手牌から刻子を検出します．
        /// </summary>
        /// <param name="hand">手牌の牌リスト</param>
        /// <returns></returns>
        public static Meld[] FindTriples(Tile[] hand)
        {
            List<Meld> ret = new List<Meld>();
            var removeDummy = hand.Where(a => !(a is Dummy));
            var sorted = removeDummy.OrderBy(a => a.Code);
            int count = removeDummy.Count();
            for (int i = 0; i < count - 2; ++i)
            {
                var one = sorted.ElementAt(i);
                var two = sorted.ElementAt(i + 1);
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

        /// <summary>
        /// 手牌から順子を検出します．
        /// </summary>
        /// <param name="hand">手牌の牌リスト</param>
        /// <returns></returns>
        public static Meld[] FindRuns(Tile[] hand)
        {
            List<Meld> ret = new List<Meld>();
            var removeDummy = hand.Where(a => !(a is Dummy));
            var sorted = removeDummy.OrderBy(a => a.Code).Distinct(new TileComparer());
            int count = sorted.Count();
            for (int i = 0; i < count - 2; ++i)
            {
                var first = sorted.ElementAt(i);
                var second = sorted.ElementAt(i + 1);
                var third = sorted.ElementAt(i + 2);
                
                if (!(first is Suits) || !(second is Suits) || !(third is Suits))
                    continue;
                else if (!first.IsSameType(second) || !first.IsSameType(third))
                    continue;
                else if (first.Code == second.Code - 1 && second.Code == third.Code - 1)
                {
                    var run = new Run(first, second, third);
                    ret.Add(run);
                }
            }
            return ret.ToArray();
        }

        /// <summary>
        /// 手牌から槓子を検出します．
        /// </summary>
        /// <param name="hand">手牌の牌リスト</param>
        /// <returns></returns>
        public static Meld[] FindQuads(Tile[] hand)
        {
            List<Meld> ret = new List<Meld>();
            var sorted = hand.OrderBy(a => a.Code);
            int count = hand.Count();
            for (int i = 0; i < count - 3; ++i)
            {
                var first = sorted.ElementAt(i);
                var second = sorted.ElementAt(i + 1);
                var third = sorted.ElementAt(i + 2);
                var fourth = sorted.ElementAt(i + 3);

                if (first.EqualsRedSuitedTileIncluding(second) && first.EqualsRedSuitedTileIncluding(third) && first.EqualsRedSuitedTileIncluding(fourth))
                {
                    var quad = new Quad(first, second, third, fourth);
                    ret.Add(quad);
                }
            }
            return ret.ToArray();
        }
    }
}
