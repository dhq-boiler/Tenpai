using System;
using System.Collections.Generic;
using System.Linq;
using Tenpai.Extensions;
using Tenpai.Models.Tiles;
using Tenpai.Models.Yaku.Meld;

namespace Tenpai.Models.Yaku.Meld.Detector
{
    public static class MeldDetector
    {
        public static CompletedHand[] FindCompletedHands(Tile[] hand, Meld[] exposed, int tileCount, ViewModels.AgariType agariType, Tile agariTile, ViewModels.WindOfTheRound windOfTheRound, ViewModels.OnesOwnWind onesOwnWind)
        {
            List<CompletedHand> ret = new List<CompletedHand>();
            TileCollection allTiles = new TileCollection(hand, exposed);

            var runs = FindRuns(hand);
            var triples = FindTriples(hand);
            var heads = FindDoubles(hand);
            var singles = FindSingles(hand);

            ////七対子
            //CompletedHandsSevenPairs(exposed, ret, heads, singles);

            ////国士無双
            //CompletedHandsThirteenOrphans(ret, runs, triples, heads, singles);

            ////4面子1雀頭
            //CompletedHandsBasicForm(hand, exposed, ret, runs, triples, heads, agariTile);

            //AddYaku(ret, hand, exposed, runs, triples, heads, singles, agariType);

            var incompletedHands = FindReadyHands(hand.Except(new Tile[] { agariTile }).ToArray(), exposed, tileCount, agariType, windOfTheRound, onesOwnWind);

            foreach (var incompletedHand in incompletedHands)
            {
                if (incompletedHand.WaitingTiles.ContainsRedSuitedTileIncluding(agariTile))
                {
                    var completedHand = new CompletedHand(incompletedHand.Melds.ToArray());
                    foreach (var meld in incompletedHand.Melds)
                    {
                        if (meld is IncompletedMeld im)
                        {
                            if (im is OpenWait || im is EdgeWait || im is ClosedWait)
                            {
                                completedHand.WaitForm = new IncompletedMeld[] { im };
                            }
                            else if (im is Double d)
                            {
                                if (completedHand.WaitForm is null)
                                {
                                    im = im.Clone(IncompletedMeld.MeldStatus.WAIT) as Double;
                                    im.ComputeWaitTiles();
                                    completedHand.WaitForm = new IncompletedMeld[] { im };
                                }
                                else if (completedHand.WaitForm[0] is Double)
                                {
                                    im = im.Clone(IncompletedMeld.MeldStatus.WAIT) as Double;
                                    im.ComputeWaitTiles();
                                    completedHand.WaitForm = new IncompletedMeld[] { completedHand.WaitForm[0], im };
                                    var replace = completedHand.WaitForm.FirstOrDefault(x => x.WaitTiles.ContainsRedSuitedTileIncluding(agariTile));
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
                                completedHand.WaitForm = new IncompletedMeld[] { im };
                            }
                        }
                    }
                    ret.Add(completedHand);
                }
            }

            AddYaku(ret, hand, exposed, runs, triples, heads, singles, agariType, windOfTheRound, onesOwnWind);

            return ret.ToArray();
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
            if (hand.Count() + (exposed != null ? exposed.Select(x => x.Tiles.Count()).Sum() : 0) != tileCount)
                return Array.Empty<ReadyHand>();

            List<ReadyHand> ret = new List<ReadyHand>();

            var runs = FindRuns(hand);
            var triples = FindTriples(hand);
            var heads = FindDoubles(hand);
            var singles = FindSingles(hand);

            //七対子
            ReadyHandsSevenPairs(hand, exposed, ret, heads, singles);

            //純正国士無双（国士無双13面待ち）
            ReadyHandsPureThirteenOrphans(hand, ret, runs, triples, heads, singles);

            //国士無双単騎待ち
            ReadyHandsThirteenOrphansSingleWait(hand, ret, runs, triples, heads, singles);
            
            //4面子1雀頭
            ReadyHandsBasicForm(hand, exposed, ret, runs, triples, heads, singles);

            AddYaku(ret, hand, exposed, runs, triples, heads, singles, agariType, windOfTheRound, onesOwnWind);

            return ret.Distinct(new DelegateComparer<ReadyHand, Tile[]>(x => x.WaitingTiles)).ToArray();
        }

        private static void AddYaku<T>(List<T> ret, Tile[] hand, Meld[] exposed, Meld[] runs, Meld[] triples, Meld[] heads, Meld[] singles, ViewModels.AgariType agariType, ViewModels.WindOfTheRound windOfTheRound, ViewModels.OnesOwnWind onesOwnWind) where T : ReadyHand
        {
            foreach (var rh in ret)
            {
                var isMenzen = exposed.Where(x => x is Run || x is Triple || (x is Quad quad && quad.Type != KongType.ConcealedKong)).Count() == 0;
                var isTumo = agariType == ViewModels.AgariType.Tsumo;
                if (isMenzen && isTumo)
                {
                    //門前清自摸和
                    rh.Yakus.Add(new ConcealedSelfDraw());
                }

                var runsAreThree = rh.Melds.Where(x => x is Run).Count() == 3;
                var allRuns = rh.Melds.Except(rh.Melds.Where(x => x is Double)).All(x => x is Run || x is OpenWait);
                //var headIsNotYakuhai = (rh.Melds.Count(x => x is Double) == 1) ? rh.Melds.First(x => x is Double).HasYaku()
            }
        }

        private static void ReadyHandsBasicForm(Tile[] hand, Meld[] exposed, List<ReadyHand> ret, Meld[] runs, Meld[] triples, Meld[] heads, Meld[] singles)
        {
            var melds = new List<Meld>();
            melds.AddRange(runs);
            melds.AddRange(triples);

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

            var waitpool = new TileCollection();

            //1雀頭完成済み
            for (int l = 0; l < heads.Count(); ++l)
            {
                TileCollection tiles = new TileCollection(hand);
                if (exposed != null)
                {
                    foreach (var one in exposed)
                    {
                        tiles.AddRange(one.Tiles);
                    }
                }
                var head = heads[l];

                for (int m = 0; m < melds.Count(); ++m)
                {
                    var firstMeld = melds[m];
                    if (!tiles.IsAllContained(head, firstMeld))
                        continue;

                    for (int n = m + 1; n < melds.Count(); ++n)
                    {
                        var secondMeld = melds[n];
                        if (!tiles.IsAllContained(head, firstMeld, secondMeld))
                            continue;

                        for (int o = n + 1; o < melds.Count(); ++o)
                        {
                            var thirdMeld = melds[o];
                            if (!tiles.IsAllContained(head, firstMeld, secondMeld, thirdMeld))
                                continue;

                            //1雀頭・3面子完成済み
                            var icRuns = IncompletedMeldDetector.FindIncompletedRuns(hand);
                            var icTriples = IncompletedMeldDetector.FindIncompletedTriple(hand);
                            var icMelds = new List<IncompletedMeld>();
                            icMelds.AddRange(icRuns);
                            icMelds.AddRange(icTriples);
                            for (int p = 0; p < icMelds.Count(); ++p)
                            {
                                var wait = icMelds[p];
                                if (!tiles.IsAllContained(head, firstMeld, secondMeld, thirdMeld, wait))
                                    continue;

                                var odd = tiles.Odd(head, firstMeld, secondMeld, thirdMeld, wait);

                                if (wait.MeldStatusType == IncompletedMeld.MeldStatus.WAIT)
                                {
                                    wait.ComputeWaitTiles();
                                }

                                foreach (var w in wait.WaitTiles)
                                {
                                    if (waitpool.ContainsRedSuitedTileIncluding(w))
                                        continue;
                                    //1雀頭・3面子完成済み，1搭子
                                    ret.Add(new ManualWaitReadyHand(w, (head as IncompletedMeld).Clone(IncompletedMeld.MeldStatus.COMPLETED), firstMeld, secondMeld, thirdMeld, wait.Clone(IncompletedMeld.MeldStatus.WAIT)));

                                    waitpool.Add(w);
                                }
                            }
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

                for (int m = 0; m < melds.Count(); ++m)
                {
                    var firstMeld = melds[m];
                    if (!tiles.IsAllContained(wait, firstMeld))
                        continue;

                    for (int n = m + 1; n < melds.Count(); ++n)
                    {
                        var secondMeld = melds[n];
                        if (!tiles.IsAllContained(wait, firstMeld, secondMeld))
                            continue;

                        for (int o = n + 1; o < melds.Count(); ++o)
                        {
                            var thirdMeld = melds[o];
                            if (!tiles.IsAllContained(wait, firstMeld, secondMeld, thirdMeld))
                                continue;

                            for (int p = o + 1; p < melds.Count(); ++p)
                            {
                                var fourthMeld = melds[p];
                                if (!tiles.IsAllContained(wait, firstMeld, secondMeld, thirdMeld, fourthMeld))
                                    continue;

                                var odd = tiles.Odd(wait, firstMeld, secondMeld, thirdMeld, fourthMeld);

                                if (waitpool.ContainsRedSuitedTileIncluding(odd[0]))
                                    continue;

                                //4面子完成済み，1雀頭完成待ち
                                ret.Add(new ReadyHand(odd, (wait as Single).Clone(IncompletedMeld.MeldStatus.WAIT), firstMeld, secondMeld, thirdMeld, fourthMeld));

                                waitpool.Add(odd[0]);
                            }
                        }
                    }
                }
            }
        }

        private static void ReadyHandsSevenPairs(Tile[] hand, Meld[] exposed, List<ReadyHand> ret, Meld[] heads, Meld[] singles)
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

        private static void ReadyHandsThirteenOrphansSingleWait(Tile[] hand, List<ReadyHand> ret, Meld[] runs, Meld[] triples, Meld[] heads, Meld[] singles)
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

        private static void ReadyHandsPureThirteenOrphans(Tile[] hand, List<ReadyHand> ret, Meld[] runs, Meld[] triples, Meld[] heads, Meld[] singles)
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
            var sorted = removeDummy.OrderBy(a => a.Code).Distinct();
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
