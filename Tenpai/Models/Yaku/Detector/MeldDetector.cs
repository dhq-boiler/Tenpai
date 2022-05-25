using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tenpai.Extensions;
using Tenpai.Models.Tiles;
using Tenpai.Utils;
using Tenpai.ViewModels;

namespace Tenpai.Models.Yaku.Meld.Detector
{
    public static class MeldDetector
    {
        public static CompletedHand[] FindCompletedHands(Tile[] hand, Meld[] exposed, int tileCount, ViewModels.AgariType agariType, Tile agariTile, ViewModels.WindOfTheRound windOfTheRound, ViewModels.OnesOwnWind onesOwnWind, DoraDisplayTileCollection doras)
        {
            List<CompletedHand> ret = new List<CompletedHand>();
            TileCollection allTiles = new TileCollection(hand, exposed);

            var handCollection = new TileCollection(hand);
            handCollection.RemoveTiles(agariTile, 1);
            var handArr = handCollection.ToArray();

            var incompletedHands = FindReadyHands(handArr, exposed, tileCount - 1, agariType, windOfTheRound, onesOwnWind, doras);

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
                        ConstructWaitForm(agariTile, ret, incompletedHand, completedHand);
                    }
                }
            }

            var runs = FindRuns(hand);
            var triples = FindTriples(hand);
            var heads = FindDoubles(hand);
            var singles = FindSingles(hand);

            AddYaku(ref ret, new TileCollection(hand), exposed, runs, triples, heads, singles, agariType, windOfTheRound, onesOwnWind);
            AddDora(ref ret, new TileCollection(hand), exposed, doras);
            CalcHu(ref ret, exposed, windOfTheRound, onesOwnWind, agariType);

            return ret.Distinct().ToArray();
        }

        private static void CalcHu<T>(ref List<T> readyHands, Meld[] exposed, WindOfTheRound windOfTheRound, OnesOwnWind onesOwnWind, AgariType agariType) where T : ReadyHand
        {
            foreach (var rh in readyHands)
            {
                int huSum = 20;

                //牌の構成
                foreach (var meld in rh.Melds)
                {
                    huSum += meld.Hu(windOfTheRound, onesOwnWind);
                }

                //待ちの形
                if (rh is CompletedHand ch)
                {
                    huSum += ch.WaitForm.Max(wait => wait.Hu(windOfTheRound, onesOwnWind));
                }
                else
                {
                    huSum += rh.Waiting.Max(wait => wait.Hu(windOfTheRound, onesOwnWind));
                }

                //あがり方
                if (agariType == AgariType.Tsumo)
                {
                    huSum += 2;
                }
                else if (agariType == AgariType.Ron)
                {
                    var isMenzen = exposed == null || exposed.Where(x => x is Run || x is Triple || (x is Quad quad && quad.Type != KongType.ConcealedKong)).Count() == 0;
                    if (isMenzen)
                    {
                        huSum += 10;
                    }
                }

                rh.HuSum.Value = (int)(Math.Ceiling(huSum / 10d) * 10);
            }
        }

        private static void ConstructWaitForm(Tile agariTile, List<CompletedHand> ret, ReadyHand incompletedHand, CompletedHand completedHand)
        {
            foreach (var meld in incompletedHand.Melds)
            {
                if (meld is IncompletedMeld im)
                {
                    if (im is Single s)
                    {
                        completedHand.WaitForm = new Meld[] { im };
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
                        completedHand.WaitForm = new Meld[] { im };
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
                            if (completedHand.WaitForm[0].Tiles[0].EqualsRedSuitedTileIncluding(d.Tiles[0]))
                            {
                                //5枚以上同種の牌は存在しない
                                return;
                            }
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
        public static ReadyHand[] FindReadyHands(Tile[] hand, Meld[] exposed, int tileCount, ViewModels.AgariType agariType, ViewModels.WindOfTheRound windOfTheRound, ViewModels.OnesOwnWind onesOwnWind, DoraDisplayTileCollection doras)
        {
            hand = hand.Where(x => !(x is Dummy)).ToArray();
            var tcount = hand.Count();
            if (tcount != tileCount)
                throw new Exception($"tcount({tcount}) != tileCount({tileCount})");

            List<ReadyHand> ret = new List<ReadyHand>();

            var runs = FindRuns(hand);
            var triples = FindTriples(hand);
            var heads = FindDoubles(hand);
            var singles = FindSingles(hand);
            var quads = exposed != null ? exposed.Where(x => x is Quad).ToArray() : Array.Empty<Meld>();

            //七対子
            ReadyHandsSevenPairs(hand, exposed, ref ret, heads, singles);

            //純正国士無双（国士無双13面待ち）
            ReadyHandsPureThirteenOrphans(hand, ref ret, runs, triples, heads, singles);

            //国士無双単騎待ち
            ReadyHandsThirteenOrphansSingleWait(hand, ref ret, runs, triples, heads, singles);
            
            //4面子1雀頭
            ReadyHandsBasicForm(hand, exposed, ref ret, runs, triples, heads, singles, quads);

            AddYaku(ref ret, new TileCollection(hand), exposed, runs, triples, heads, singles, agariType, windOfTheRound, onesOwnWind);
            AddDora(ref ret, new TileCollection(hand), exposed, doras);
            CalcHu(ref ret, exposed, windOfTheRound, onesOwnWind, agariType);

            return ret.Distinct(new DelegateComparer<ReadyHand, int>(x =>
            {
                var hash = x.GetHashCode();
                Console.WriteLine($"{x} {Convert.ToString(hash, 2).PadLeft(32, '0')}");
                return hash;
            })).ToArray();
        }

        private static void AddDora<T>(ref List<T> ret, TileCollection tileCollection, Meld[] exposed, DoraDisplayTileCollection doras) where T : ReadyHand
        {
            foreach (var rh in ret)
            {
                int doraCount = 0;
                foreach (var dora in doras)
                {
                    var next = Tile.Next(dora.Code, System.Windows.Visibility.Visible, null);
                    foreach (var z in rh.Melds.ToTileCollection().CloneAndUnion(rh.WaitingTiles))
                    {
                        if (next.EqualsRedSuitedTileIncluding(z))
                        {
                            doraCount++;
                        }
                    }
                }
                //ドラ
                if (doraCount > 0)
                {
                    rh.Yakus.Add(new Dora(doraCount));
                }

                int redDoraCount = 0;
                foreach (var z in rh.Melds.ToTileCollection().CloneAndUnion(rh.WaitingTiles))
                {
                    if (z is IRedSuitedTile r && r.IsRedSuited)
                    {
                        redDoraCount++;
                    }
                }
                //赤ドラ
                if (redDoraCount > 0)
                {
                    rh.Yakus.Add(new RedDora(redDoraCount));
                }
            }
        }

        private static void AddYaku<T>(ref List<T> ret, TileCollection hand, Meld[] exposed, Meld[] runs, Meld[] triples, Meld[] heads, Meld[] singles, ViewModels.AgariType agariType, ViewModels.WindOfTheRound windOfTheRound, ViewModels.OnesOwnWind onesOwnWind) where T : ReadyHand
        {
            foreach (var rh in ret)
            {
                #region 役満

                var exclusiveDoubles = FindDoubles(hand, runs, triples, exposed);
                var fourConcealedTriples = rh.Melds.Where(x => x is Triple || (x is Quad q && q.Type == KongType.ConcealedKong)).Count() == 4
                                        && (exposed == null || exposed.Where(x => x is Triple || (x is Quad quad && quad.Type != KongType.ConcealedKong)).Count() == 0)
                                        && rh is CompletedHand && (rh as CompletedHand).WaitForm.All(x => x is Double) && (rh as CompletedHand).WaitForm.Count() == 2
                                        && agariType == ViewModels.AgariType.Tsumo;
                if (fourConcealedTriples)
                {
                    //四暗刻
                    rh.Yakus.Add(new FourConcealedTriples());
                }

                var exclusiveSingles = FindSingles(hand, runs, triples, exposed);
                var fourConcealedTriplesSingleWait = rh.Melds.Where(x => x is Triple || (x is Quad q && q.Type == KongType.ConcealedKong)).Count() == 4
                                                  && (exposed == null || exposed.Where(x => x is Triple || (x is Quad quad && quad.Type != KongType.ConcealedKong)).Count() == 0)
                                                  && rh is CompletedHand && (rh as CompletedHand).WaitForm.Count() == 1;
                if (fourConcealedTriplesSingleWait)
                {
                    //四暗刻 単騎待ち
                    rh.Yakus.Add(new FourConcealedTriplesSingleWait());
                }

                var allTerminals = rh.ComplementAndGetCompletedHand().Any(z => z.Melds.All(x => x.Tiles.All(y => y is ITerminals)));
                if (allTerminals)
                {
                    //清老頭
                    rh.Yakus.Add(new AllTerminals());
                }

                var fourQuads = rh.Melds.Count(x => x is Quad) == 4;
                if (fourQuads)
                {
                    //四槓子
                    rh.Yakus.Add(new FourQuads());
                }

                var bigDragons = rh.ComplementAndGetCompletedHand().Any(z => z.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y is Dragons)) == 3);
                if (bigDragons)
                {
                    //大三元
                    rh.Yakus.Add(new BigDragons());
                }

                var bigFourWinds = rh.ComplementAndGetCompletedHand().Any(z => z.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y is Winds)) == 4);
                if (bigFourWinds)
                {
                    //大四喜
                    rh.Yakus.Add(new BigFourWinds());
                }

                var smallFourWinds = rh.ComplementAndGetCompletedHand().Any(z => z.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y is Winds)) == 3
                                                                              && z.Melds.Count(x => (x is Double && x.Tiles.All(y => y is Winds))) == 1);
                if (!bigFourWinds && smallFourWinds)
                {
                    //小四喜
                    rh.Yakus.Add(new SmallFourWinds());
                }

                var allHonors = rh.ComplementAndGetCompletedHand().Any(z => z.Melds.All(y => y.Tiles.All(x => x is Honors)));
                if (allHonors)
                {
                    //字一色
                    rh.Yakus.Add(new AllHonors());
                }

                var allGreen = rh.ComplementAndGetCompletedHand().Any(x => x.Melds.All(y => y.Tiles.All(z => z is Bamboo_2 || z is Bamboo_3 || z is Bamboo_4 || z is Bamboo_6 || z is Bamboo_8 || z is Green)));
                if (allGreen)
                {
                    //緑一色
                    rh.Yakus.Add(new AllGreen());
                }

                bool nineGatesNineWaits = false;

                if (nineGatesNineWaits = ((hand.All(x => x is Bamboos) &&
                                           hand.Enumerate<Bamboo_1>(3)
                                               .Enumerate<Bamboo_2>(1)
                                               .Enumerate<Bamboo_3>(1)
                                               .Enumerate<Bamboo_4>(1)
                                               .Enumerate<Bamboo_5>(1)
                                               .Enumerate<Bamboo_6>(1)
                                               .Enumerate<Bamboo_7>(1)
                                               .Enumerate<Bamboo_8>(1)
                                               .Enumerate<Bamboo_9>(3).Evaluate())
                                    || (hand.All(x => x is Characters) &&
                                        hand.Enumerate<Character_1>(3)
                                            .Enumerate<Character_2>(1)
                                            .Enumerate<Character_3>(1)
                                            .Enumerate<Character_4>(1)
                                            .Enumerate<Character_5>(1)
                                            .Enumerate<Character_6>(1)
                                            .Enumerate<Character_7>(1)
                                            .Enumerate<Character_8>(1)
                                            .Enumerate<Character_9>(3).Evaluate())
                                    || (hand.All(x => x is Dots) &&
                                        hand.Enumerate<Dot_1>(3)
                                            .Enumerate<Dot_2>(1)
                                            .Enumerate<Dot_3>(1)
                                            .Enumerate<Dot_4>(1)
                                            .Enumerate<Dot_5>(1)
                                            .Enumerate<Dot_6>(1)
                                            .Enumerate<Dot_7>(1)
                                            .Enumerate<Dot_8>(1)
                                            .Enumerate<Dot_9>(3).Evaluate())
                                    && (exposed == null || exposed.Length == 0)))
                {
                    //九蓮宝燈9面待ち
                    rh.Yakus.Add(new NineGatesNineWaits());
                }

                var bambooArr = new Tile[] { Tile.CreateInstance<Bamboo_1>(), Tile.CreateInstance<Bamboo_2>(), Tile.CreateInstance<Bamboo_3>(), Tile.CreateInstance<Bamboo_4>(), Tile.CreateInstance<Bamboo_5>(), Tile.CreateInstance<Bamboo_6>(), Tile.CreateInstance<Bamboo_7>(), Tile.CreateInstance<Bamboo_8>(), Tile.CreateInstance<Bamboo_9>() };
                var characterArr = new Tile[] { Tile.CreateInstance<Character_1>(), Tile.CreateInstance<Character_2>(), Tile.CreateInstance<Character_3>(), Tile.CreateInstance<Character_4>(), Tile.CreateInstance<Character_5>(), Tile.CreateInstance<Character_6>(), Tile.CreateInstance<Character_7>(), Tile.CreateInstance<Character_8>(), Tile.CreateInstance<Character_9>() };
                var dotArr = new Tile[] { Tile.CreateInstance<Dot_1>(), Tile.CreateInstance<Dot_2>(), Tile.CreateInstance<Dot_3>(), Tile.CreateInstance<Dot_4>(), Tile.CreateInstance<Dot_5>(), Tile.CreateInstance<Dot_6>(), Tile.CreateInstance<Dot_7>(), Tile.CreateInstance<Dot_8>(), Tile.CreateInstance<Dot_9>() };
                var nineGates = (rh.Melds.ToTileCollection().All(x => x is Bamboos)
                               && rh.WaitingTiles.Any(y =>
                                           bambooArr.Any(x => new TileCollection(new Tile[] { }, rh.Melds.ToArray()).LookIn()
                                                                                                                    .Add(y, 1)
                                                                                                                    .Reduce(x, 1)
                                                                                                                    .LookIn()
                                                                                                                    .Enumerate<Bamboo_1>(3)
                                                                                                                    .Enumerate<Bamboo_2>(1)
                                                                                                                    .Enumerate<Bamboo_3>(1)
                                                                                                                    .Enumerate<Bamboo_4>(1)
                                                                                                                    .Enumerate<Bamboo_5>(1)
                                                                                                                    .Enumerate<Bamboo_6>(1)
                                                                                                                    .Enumerate<Bamboo_7>(1)
                                                                                                                    .Enumerate<Bamboo_8>(1)
                                                                                                                    .Enumerate<Bamboo_9>(3)
                                                                                                                    .Evaluate())))
                                  || (rh.Melds.ToTileCollection().All(x => x is Characters)
                                   && rh.WaitingTiles.Any(y =>
                                           characterArr.Any(x => new TileCollection(new Tile[] { }, rh.Melds.ToArray()).LookIn()
                                                                                                                       .Add(y, 1)
                                                                                                                       .Reduce(x, 1)
                                                                                                                       .LookIn()
                                                                                                                       .Enumerate<Character_1>(3)
                                                                                                                       .Enumerate<Character_2>(1)
                                                                                                                       .Enumerate<Character_3>(1)
                                                                                                                       .Enumerate<Character_4>(1)
                                                                                                                       .Enumerate<Character_5>(1)
                                                                                                                       .Enumerate<Character_6>(1)
                                                                                                                       .Enumerate<Character_7>(1)
                                                                                                                       .Enumerate<Character_8>(1)
                                                                                                                       .Enumerate<Character_9>(3)
                                                                                                                       .Evaluate())))
                                  || (rh.Melds.ToTileCollection().All(x => x is Dots)
                                   && rh.WaitingTiles.Any(y =>
                                           dotArr.Any(x => new TileCollection(new Tile[] { }, rh.Melds.ToArray()).LookIn()
                                                                                                                 .Add(y, 1)
                                                                                                                 .Reduce(x, 1)
                                                                                                                 .LookIn()
                                                                                                                 .Enumerate<Dot_1>(3)
                                                                                                                 .Enumerate<Dot_2>(1)
                                                                                                                 .Enumerate<Dot_3>(1)
                                                                                                                 .Enumerate<Dot_4>(1)
                                                                                                                 .Enumerate<Dot_5>(1)
                                                                                                                 .Enumerate<Dot_6>(1)
                                                                                                                 .Enumerate<Dot_7>(1)
                                                                                                                 .Enumerate<Dot_8>(1)
                                                                                                                 .Enumerate<Dot_9>(3)
                                                                                                                 .Evaluate())));
                if (!nineGatesNineWaits && nineGates)
                {
                    //九蓮宝燈
                    rh.Yakus.Add(new NineGates());
                }

                bool thirteenOrphansThirteenWaits = false;

                if (thirteenOrphansThirteenWaits = (hand.All(x => x is ITerminals || x is Honors)
                                                 && hand.Enumerate<Bamboo_1>(1)
                                                        .Enumerate<Bamboo_9>(1)
                                                        .Enumerate<Character_1>(1)
                                                        .Enumerate<Character_9>(1)
                                                        .Enumerate<Dot_1>(1)
                                                        .Enumerate<Dot_9>(1)
                                                        .Enumerate<East>(1)
                                                        .Enumerate<South>(1)
                                                        .Enumerate<West>(1)
                                                        .Enumerate<North>(1)
                                                        .Enumerate<White>(1)
                                                        .Enumerate<Green>(1)
                                                        .Enumerate<Red>(1).Evaluate())
                                    && (exposed == null || exposed.Length == 0))
                {
                    //国士無双13面待ち
                    rh.Yakus.Add(new ThirteenOrphansThirteenWaits());
                }

                var thirteenArr = new Tile[] { Tile.CreateInstance<Bamboo_1>(),
                                               Tile.CreateInstance<Bamboo_9>(),
                                               Tile.CreateInstance<Character_1>(),
                                               Tile.CreateInstance<Character_9>(),
                                               Tile.CreateInstance<Dot_1>(),
                                               Tile.CreateInstance<Dot_9>(),
                                               Tile.CreateInstance<East>(),
                                               Tile.CreateInstance<South>(),
                                               Tile.CreateInstance<West>(),
                                               Tile.CreateInstance<North>(),
                                               Tile.CreateInstance<White>(),
                                               Tile.CreateInstance<Green>(),
                                               Tile.CreateInstance<Red>()
                };
                var thirteenOrphans = rh.Melds.ToTileCollection().All(x => x is ITerminals || x is Honors)
                                   && rh.WaitingTiles.Any(y =>
                                           thirteenArr.Any(x => new TileCollection(new Tile[] { }, rh.Melds.ToArray()).LookIn()
                                                                                                                      .Add(y, 1)
                                                                                                                      .Reduce(x, 1)
                                                                                                                      .LookIn()
                                                                                                                      .Enumerate<Bamboo_1>(1)
                                                                                                                      .Enumerate<Bamboo_9>(1)
                                                                                                                      .Enumerate<Character_1>(1)
                                                                                                                      .Enumerate<Character_9>(1)
                                                                                                                      .Enumerate<Dot_1>(1)
                                                                                                                      .Enumerate<Dot_9>(1)
                                                                                                                      .Enumerate<East>(1)
                                                                                                                      .Enumerate<South>(1)
                                                                                                                      .Enumerate<West>(1)
                                                                                                                      .Enumerate<North>(1)
                                                                                                                      .Enumerate<White>(1)
                                                                                                                      .Enumerate<Green>(1)
                                                                                                                      .Enumerate<Red>(1).Evaluate()))
                                    && (exposed == null || exposed.Length == 0);
                if (!thirteenOrphansThirteenWaits && thirteenOrphans)
                {
                    //国士無双
                    rh.Yakus.Add(new ThirteenOrphans());
                }

                #endregion //役満

                if (!fourConcealedTriples && !fourConcealedTriplesSingleWait && !allTerminals && !fourQuads && !bigDragons && !bigFourWinds && !smallFourWinds && !allHonors && !allGreen && !nineGatesNineWaits && !nineGates)
                {
                    var isMenzen = exposed == null || exposed.Where(x => x is Run || x is Triple || (x is Quad quad && quad.Type != KongType.ConcealedKong)).Count() == 0;
                    var isTumo = agariType == ViewModels.AgariType.Tsumo;
                    if (isMenzen && isTumo)
                    {
                        //門前清自摸和
                        rh.Yakus.Add(new ConcealedSelfDraw());
                    }

                    var runsAreThree = rh.Melds.Where(x => x is Run).Count() >= 3;
                    var allRuns = rh.Melds.Except(rh.Melds.Where(x => x is Double || x is Single)).All(x => x is Run || x is OpenWait);
                    var headIsNotYakuhai = (rh.Melds.Count(x => x is Double) == 1)
                                         ? !rh.Melds.First(x => x is Double).HasYaku(windOfTheRound, onesOwnWind)
                                         : (rh.Melds.Count(x => x is Single) == 1
                                         ? !rh.Melds.First(x => x is Single).HasYaku(windOfTheRound, onesOwnWind)
                                         : false);
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

                    if (isMenzen && doubleRunsCount == 1)
                    {
                        //一盃口
                        rh.Yakus.Add(new DoubleRun());
                    }
                    else if (isMenzen && doubleRunsCount == 2)
                    {
                        //二盃口
                        rh.Yakus.Add(new TwoDoubleRuns());
                    }

                    var allSimples = rh.Melds.All(x => PrepareTileCollectionAndEvaluate(x, rh, (collection) => collection.All(y => !(y is ITerminals) && !(y is Honors))));
                    if (allSimples)
                    {
                        //断么九
                        rh.Yakus.Add(new AllSimples());
                    }

                    var pureOutside = rh.Melds.All(x => PrepareTileCollectionAndEvaluate(x, rh, (collection) => collection.Has(y => y is ITerminals)));
                    if (pureOutside)
                    {
                        //純全帯么九
                        rh.Yakus.Add(new PureOutsideHand());
                    }

                    var allTerminalsAndHonors = rh.Melds.All(x => PrepareTileCollectionAndEvaluate(x, rh, (collection) => collection.All(y => y is Honors) || x.Tiles.All(y => y is ITerminals)));
                    if (allTerminalsAndHonors)
                    {
                        //混老頭
                        rh.Yakus.Add(new AllTerminalsAndHonors());
                    }

                    var mixedOutside = rh.Melds.All(x => PrepareTileCollectionAndEvaluate(x, rh, (collection) => collection.All(y => y is Honors) || x.Tiles.Has(y => y is ITerminals)));
                    if (!pureOutside && !allTerminalsAndHonors && mixedOutside)
                    {
                        //混全帯么九
                        rh.Yakus.Add(new MixedOutsideHand());
                    }

                    var allTriple = rh.ComplementAndGetCompletedHand().Any(x => x.Melds.All(y => y is not Run && y is not OpenWait && y is not ClosedWait && y is not EdgeWait)
                                                                             && x.Melds.Count(y => y is Double) == 1);
                    if (allTriple)
                    {
                        //対々和
                        rh.Yakus.Add(new AllTriples());
                    }

                    var threeConcealedTriples = rh.Melds.Where(x => (x is Triple t && (t.CallFrom == null || t.CallFrom == EOpponent.Unknown) && (rh is not CompletedHand || rh is CompletedHand ch && ch.WaitForm.Any(y => y.WaitTiles.Any(z => z.EqualsRedSuitedTileIncluding(t.Tiles[0]))))) || (x is Quad q && q.Type == KongType.ConcealedKong)).Count() == 3;
                    bool threeConcealedTriplesTsumo = IsThreeConcealedTriplesTsumo(agariType, rh);
                    if (threeConcealedTriples || threeConcealedTriplesTsumo)
                    {
                        //三暗刻
                        rh.Yakus.Add(new ThreeConcealedTriples());
                    }

                    var threeQuads = rh.Melds.Count(x => x is Quad) == 3;
                    if (threeQuads)
                    {
                        //三槓子
                        rh.Yakus.Add(new ThreeQuads());
                    }

                    var valueTilesWhite = rh.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y.EqualsRedSuitedTileIncluding(Tile.CreateInstance<White>()))) == 1
                                       || rh.Melds.Any(x => x is Double && x.Tiles.All(y => y is White) && x.Tiles.First().EqualsRedSuitedTileIncluding(rh.WaitingTiles.SingleOrDefault()));
                    if (valueTilesWhite)
                    {
                        //役牌白
                        rh.Yakus.Add(new ValueTiles<White>(ValueType.ThreeElementTille));
                    }

                    var valueTilesGreen = rh.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y.EqualsRedSuitedTileIncluding(Tile.CreateInstance<Green>()))) == 1
                                       || rh.Melds.Any(x => x is Double && x.Tiles.All(y => y is Green) && x.Tiles.First().EqualsRedSuitedTileIncluding(rh.WaitingTiles.SingleOrDefault()));
                    if (valueTilesGreen)
                    {
                        //役牌發
                        rh.Yakus.Add(new ValueTiles<Green>(ValueType.ThreeElementTille));
                    }

                    var valueTilesRed = rh.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y.EqualsRedSuitedTileIncluding(Tile.CreateInstance<Red>()))) == 1
                                       || rh.Melds.Any(x => x is Double && x.Tiles.All(y => y is Red) && x.Tiles.First().EqualsRedSuitedTileIncluding(rh.WaitingTiles.SingleOrDefault()));
                    if (valueTilesRed)
                    {
                        //役牌中
                        rh.Yakus.Add(new ValueTiles<Red>(ValueType.ThreeElementTille));
                    }

                    switch (windOfTheRound)
                    {
                        case WindOfTheRound.East:
                            var valueTilesEast = rh.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y.EqualsRedSuitedTileIncluding(Tile.CreateInstance<East>()))) == 1
                                              || rh.Melds.Any(x => x is Double && x.Tiles.All(y => y is East) && x.Tiles.First().EqualsRedSuitedTileIncluding(rh.WaitingTiles.SingleOrDefault()));
                            if (valueTilesEast)
                            {
                                //役牌 場風牌 東
                                rh.Yakus.Add(new ValueTiles<East>(ValueType.FiledStyleTiles));
                            }
                            break;
                        case WindOfTheRound.South:
                            var valueTilesSouth = rh.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y.EqualsRedSuitedTileIncluding(Tile.CreateInstance<South>()))) == 1
                                               || rh.Melds.Any(x => x is Double && x.Tiles.All(y => y is South) && x.Tiles.First().EqualsRedSuitedTileIncluding(rh.WaitingTiles.SingleOrDefault()));
                            if (valueTilesSouth)
                            {
                                //役牌 場風牌 南
                                rh.Yakus.Add(new ValueTiles<South>(ValueType.FiledStyleTiles));
                            }
                            break;
                        case WindOfTheRound.West:
                            var valueTilesWest = rh.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y.EqualsRedSuitedTileIncluding(Tile.CreateInstance<West>()))) == 1
                                              || rh.Melds.Any(x => x is Double && x.Tiles.All(y => y is West) && x.Tiles.First().EqualsRedSuitedTileIncluding(rh.WaitingTiles.SingleOrDefault()));
                            if (valueTilesWest)
                            {
                                //役牌 場風牌 西
                                rh.Yakus.Add(new ValueTiles<West>(ValueType.FiledStyleTiles));
                            }
                            break;
                        case WindOfTheRound.North:
                            var valueTilesNorth = rh.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y.EqualsRedSuitedTileIncluding(Tile.CreateInstance<North>()))) == 1
                                               || rh.Melds.Any(x => x is Double && x.Tiles.All(y => y is North) && x.Tiles.First().EqualsRedSuitedTileIncluding(rh.WaitingTiles.SingleOrDefault()));
                            if (valueTilesNorth)
                            {
                                //役牌 場風牌 北
                                rh.Yakus.Add(new ValueTiles<North>(ValueType.FiledStyleTiles));
                            }
                            break;
                    }

                    switch (onesOwnWind)
                    {
                        case OnesOwnWind.East:
                            var valueTilesEast = rh.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y.EqualsRedSuitedTileIncluding(Tile.CreateInstance<East>()))) == 1
                                              || rh.Melds.Any(x => x is Double && x.Tiles.All(y => y is East) && x.Tiles.First().EqualsRedSuitedTileIncluding(rh.WaitingTiles.SingleOrDefault()));
                            if (valueTilesEast)
                            {
                                //役牌 自風牌 東
                                rh.Yakus.Add(new ValueTiles<East>(ValueType.SelfStyledTile));
                            }
                            break;
                        case OnesOwnWind.South:
                            var valueTilesSouth = rh.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y.EqualsRedSuitedTileIncluding(Tile.CreateInstance<South>()))) == 1
                                               || rh.Melds.Any(x => x is Double && x.Tiles.All(y => y is South) && x.Tiles.First().EqualsRedSuitedTileIncluding(rh.WaitingTiles.SingleOrDefault()));
                            if (valueTilesSouth)
                            {
                                //役牌 自風牌 南
                                rh.Yakus.Add(new ValueTiles<South>(ValueType.SelfStyledTile));
                            }
                            break;
                        case OnesOwnWind.West:
                            var valueTilesWest = rh.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y.EqualsRedSuitedTileIncluding(Tile.CreateInstance<West>()))) == 1
                                              || rh.Melds.Any(x => x is Double && x.Tiles.All(y => y is West) && x.Tiles.First().EqualsRedSuitedTileIncluding(rh.WaitingTiles.SingleOrDefault()));
                            if (valueTilesWest)
                            {
                                //役牌 自風牌 西
                                rh.Yakus.Add(new ValueTiles<West>(ValueType.SelfStyledTile));
                            }
                            break;
                        case OnesOwnWind.North:
                            var valueTilesNorth = rh.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y.EqualsRedSuitedTileIncluding(Tile.CreateInstance<North>()))) == 1
                                               || rh.Melds.Any(x => x is Double && x.Tiles.All(y => y is North) && x.Tiles.First().EqualsRedSuitedTileIncluding(rh.WaitingTiles.SingleOrDefault()));
                            if (valueTilesNorth)
                            {
                                //役牌 自風牌 北
                                rh.Yakus.Add(new ValueTiles<North>(ValueType.SelfStyledTile));
                            }
                            break;
                    }

                    var littleDragons = rh.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y is Dragons)) == 2 && rh.Melds.Count(x => x is Double && x.Tiles.All(y => y is Dragons)) == 1
                                     || (rh.Melds.Count(x => (x is Triple || x is Quad) && x.Tiles.All(y => y is Dragons)) == 1 && rh.Melds.Count(x => x is Double && x.Tiles.All(y => y is Dragons)) == 2
                                         && rh.Melds.Any(x => x is Double && x.Tiles.All(y => y is Dragons) && x.Tiles.First().EqualsRedSuitedTileIncluding(rh.WaitingTiles.SingleOrDefault())));
                    if (littleDragons)
                    {
                        //小三元
                        rh.Yakus.Add(new LittleDragons());
                    }

                    var fullFlush = rh.ComplementAndGetCompletedHand().Any(x => x.Melds.All(y => y.Tiles.All(z => z is Bamboos)))
                                 || rh.ComplementAndGetCompletedHand().Any(x => x.Melds.All(y => y.Tiles.All(z => z is Characters)))
                                 || rh.ComplementAndGetCompletedHand().Any(x => x.Melds.All(y => y.Tiles.All(z => z is Dots)));
                    if (fullFlush)
                    {
                        //清一色
                        rh.Yakus.Add(new FullFlush());
                    }

                    var halfFlush = rh.ComplementAndGetCompletedHand().Any(x => x.Melds.All(y => y.Tiles.All(z => z is Bamboos || z is Honors)))
                                 || rh.ComplementAndGetCompletedHand().Any(x => x.Melds.All(y => y.Tiles.All(z => z is Characters || z is Honors)))
                                 || rh.ComplementAndGetCompletedHand().Any(x => x.Melds.All(y => y.Tiles.All(z => z is Dots || z is Honors)));
                    if (!fullFlush && halfFlush)
                    {
                        //混一色
                        rh.Yakus.Add(new HalfFlush());
                    }

                    var threeColorRuns = rh.ComplementAndGetCompletedHand().Any(x => Combination.Enumerate(x.Melds, 3, false).Any(y => y.All(z => z is Run) && IsThreeColor(y[0], y[1], y[2])));
                    if (threeColorRuns)
                    {
                        //三色同順
                        rh.Yakus.Add(new ThreeColorRuns());
                    }

                    var threeColorTriples = rh.ComplementAndGetCompletedHand().Any(x => Combination.Enumerate(x.Melds, 3, false).Any(y => y.All(z => (z is Triple || z is Quad) && z.Tiles.All(o => o is Suits)) && IsThreeColor(y[0], y[1], y[2])));
                    if (threeColorTriples)
                    {
                        //三色同刻
                        rh.Yakus.Add(new ThreeColorTriples());
                    }

                    var fullStraight = rh.ComplementAndGetCompletedHand().Any(hand => Combination.Enumerate(hand.Melds, 3, false).Any(meldArr => IsFullStraight(meldArr)));
                    if (fullStraight)
                    {
                        //一気通貫
                        rh.Yakus.Add(new FullStraight());
                    }

                    var sevenPairs = rh.ComplementAndGetCompletedHand().Any(x => x.Melds.Count(y => y is Double) == 7);
                    if (sevenPairs)
                    {
                        //七対子
                        rh.Yakus.Add(new SevenPairs());
                    }
                }
            }
        }

        private static bool IsFullStraight(Meld[] meldArr)
        {
            var bamboo1 = meldArr.All(meld => meld.Tiles.All(tile => tile is Bamboos));
            var bamboo2 = meldArr[0].Equals(new Run<Bamboo_1, Bamboo_2, Bamboo_3>());
            var bamboo3 = meldArr[1].Equals(new Run<Bamboo_4, Bamboo_5, Bamboo_6>()) || meldArr[1].Equals(new Run<Bamboo_4, Bamboo_5R, Bamboo_6>());
            var bamboo4 = meldArr[2].Equals(new Run<Bamboo_7, Bamboo_8, Bamboo_9>());
            var character1 = meldArr.All(meld => meld.Tiles.All(tile => tile is Characters));
            var character2 = meldArr[0].Equals(new Run<Character_1, Character_2, Character_3>());
            var character3 = meldArr[1].Equals(new Run<Character_4, Character_5, Character_6>()) || meldArr[1].Equals(new Run<Character_4, Character_5R, Character_6>());
            var character4 = meldArr[2].Equals(new Run<Character_7, Character_8, Character_9>());
            var dot1 = meldArr.All(meld => meld.Tiles.All(tile => tile is Dots));
            var dot2 = meldArr[0].Equals(new Run<Dot_1, Dot_2, Dot_3>());
            var dot3 = meldArr[1].Equals(new Run<Dot_4, Dot_5, Dot_6>()) || meldArr[1].Equals(new Run<Dot_4, Dot_5R, Dot_6>());
            var dot4 = meldArr[2].Equals(new Run<Dot_7, Dot_8, Dot_9>());
            return (bamboo1 && bamboo2 && bamboo3 && bamboo4) || (character1 && character2 && character3 && character4) || (dot1 && dot2 && dot3 && dot4);
        }

        private static bool IsThreeColor(Meld meld1, Meld meld2, Meld meld3)
        {
            var m1Avg = meld1.Tiles.Cast<Suits>().Average(x => x.Number);
            var m2Avg = meld2.Tiles.Cast<Suits>().Average(x => x.Number);
            var m3Avg = meld3.Tiles.Cast<Suits>().Average(x => x.Number);
            if (m1Avg == m2Avg && m1Avg == m3Avg)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private static bool PrepareTileCollectionAndEvaluate(Meld target, ReadyHand rh, Func<TileCollection, bool> evaluate)
        {
            TileCollection collection = null;
            if (target == rh.Waiting.FirstOrDefault())
            {
                //ターゲットのMeldが待ち状態である時
                collection = target.Tiles.CloneAndUnion(rh.WaitingTiles);
            }
            else
            {
                collection = target.Tiles;
            }
            return evaluate.Invoke(collection);
        }

        private static bool IsThreeConcealedTriplesTsumo<T>(AgariType agariType, T rh) where T : ReadyHand
        {
            var coll = rh.Melds.Where(x => IsConcealedTriples(rh, x));
            var count = coll.Count();
            var a = count == 3;
            var b = agariType.Equals(AgariType.Tsumo);
            return a && b;
        }

        private static bool IsConcealedTriples<T>(T rh, Meld x) where T : ReadyHand
        {
            var isNotRun = x is not Run;
            var isNotDouble = x is not Double;
            var isTriple = x is Triple;
            var isQuad = x is Quad;
            var t = x as Triple;
            var callFromIsNull = isTriple && t.CallFrom == null;
            var callFromIsUnknown = isTriple && t.CallFrom == EOpponent.Unknown;
            var q = x as Quad;
            var concealed = isQuad && q.Type == KongType.ConcealedKong;
            return isNotRun && isNotDouble && ((isTriple && (callFromIsNull || callFromIsUnknown)) || ((isQuad && concealed) || !isQuad));
        }

        private static void ReadyHandsBasicForm(Tile[] hand, Meld[] exposed, ref List<ReadyHand> ret, Meld[] runs, Meld[] triples, Meld[] heads, Meld[] singles, Meld[] quads)
        {
            var melds = new List<Meld>();
            melds.AddRange(quads);
            foreach (var triple in triples)
            {
                if (!quads.Any(x => x.Tiles.Any(y => triple.Tiles[0].EqualsRedSuitedTileIncluding(y))))
                {
                    melds.Add(triple);
                }
            }
            foreach (var run in runs)
            {
                if (!quads.Any(x => x.Tiles.Any(y => run.Tiles[0].EqualsRedSuitedTileIncluding(y))
                                 || x.Tiles.Any(y => run.Tiles[1].EqualsRedSuitedTileIncluding(y))
                                 || x.Tiles.Any(y => run.Tiles[2].EqualsRedSuitedTileIncluding(y))))
                {
                    melds.Add(run);
                }
            }

            var newHeads = new List<Meld>();
            foreach (var head in heads)
            {
                if (!quads.Any(x => x.Tiles.Any(y => head.Tiles[0].EqualsRedSuitedTileIncluding(y))))
                {
                    newHeads.Add(head);
                }
            }

            OneHeadCreated(hand, exposed, ret, newHeads.ToArray(), melds);
            OneHeadCreating(hand, exposed, ret, singles, melds);
            TwoHeadCreated(hand, exposed, ret, newHeads.ToArray(), melds);

            ret = ret.Distinct(new DelegateComparer<ReadyHand, Tile[]>(x => x.WaitingTiles)).ToList();
        }

        private static void TwoHeadCreated(Tile[] hand, Meld[] exposed, List<ReadyHand> ret, Meld[] heads, List<Meld> melds)
        {
            //2対子完成
            var listHeads = new List<Meld>();
            listHeads.AddRange(heads);
            var twoHeads = Combination.Enumerate(listHeads, 2, withRepetition: true).ToList();

            var selectedMelds2 = Combination.Enumerate(melds, 3, withRepetition: true).ToList();
            for (int i = 0; i < selectedMelds2.Count(); ++i)
            {
                var m = new List<Meld>();
                m.AddRange(selectedMelds2[i]);
                selectedMelds2[i] = m.ToArray();
            }
            TileCollection tiles2 = new TileCollection(hand);

            for (int j = 0; j < twoHeads.Count; j++)
            {
                var twoHead = twoHeads[j];

                if (twoHead[0].Equals(twoHead[1]))
                {
                    //同種4枚の牌で構成される、2対子ではあがれない
                    continue;
                }

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

                        GenerateHand(ret, twoHead[1], twoHead[0], selectedMeld, tiles2);
                        GenerateHand(ret, twoHead[0], twoHead[1], selectedMeld, tiles2);
                    }
                }
            }
        }

        private static void GenerateHand(List<ReadyHand> ret, Meld targetHead, Meld otherHead, Meld[] selectedMeld, TileCollection tiles2)
        {
            if (targetHead is IncompletedMeld y)
            {
                y.ComputeWaitTiles(tiles2);
                var ts = new TileCollection(y.WaitTiles, otherHead, targetHead, selectedMeld[0], selectedMeld[1], selectedMeld[2]);
                if (ts.IsMoreThan4TilesOfTheSameType())
                {
                    return;
                }
                foreach (var w in y.WaitTiles)
                {
                    if (w is IRedSuitedTile r)
                    {
                        var normalTilesCount = ts.Count(x => x.Code == w.Code && x is IRedSuitedTile xr && !xr.IsRedSuited);
                        var redTilesCount = ts.Count(x => x.Code == w.Code && x is IRedSuitedTile xr && xr.IsRedSuited);
                        if (r.IsRedSuited && redTilesCount > 1)
                        {
                            return;
                        }
                        else if (!r.IsRedSuited && normalTilesCount == 4)
                        {
                            return;
                        }
                    }
                    //1雀頭・3面子完成済み，1搭子
                    ret.Add(new ManualWaitReadyHand(w, (targetHead as IncompletedMeld).Clone(IncompletedMeld.MeldStatus.WAIT), (otherHead as IncompletedMeld).Clone(IncompletedMeld.MeldStatus.COMPLETED), selectedMeld[0], selectedMeld[1], selectedMeld[2]));
                }
            }

        }

        private static void OneHeadCreating(Tile[] hand, Meld[] exposed, List<ReadyHand> ret, Meld[] singles, List<Meld> melds)
        {
            //1雀頭完成待ち
            for (int l = 0; l < singles.Count(); ++l)
            {
                TileCollection tiles = new TileCollection(hand);
                var wait = singles[l];

                var selectedMelds = Combination.Enumerate(melds, 4, withRepetition: true).ToList();
                for (int i = 0; i < selectedMelds.Count(); ++i)
                {
                    var m = new List<Meld>();
                    m.AddRange(selectedMelds[i]);
                    selectedMelds[i] = m.ToArray();
                }

                foreach (var selectedMeld in selectedMelds)
                {
                    if (!tiles.IsAllContained(wait, selectedMeld[0], selectedMeld[1], selectedMeld[2], selectedMeld[3]))
                        continue;
                    CreateReadyHandWhenOneHeadCreating(ret, tiles, wait, selectedMeld);
                }
            }
        }

        private static void CreateReadyHandWhenOneHeadCreating(List<ReadyHand> ret, TileCollection tiles, Meld wait, Meld[] selectedMeld)
        {
            var ww = (wait as IncompletedMeld).Clone(IncompletedMeld.MeldStatus.WAIT);
            ww.ComputeWaitTiles();
            
            foreach (var wts in ww.WaitTiles)
            {
                var ts = new TileCollection(new Tile[] { wts }, ww, selectedMeld[0], selectedMeld[1], selectedMeld[2], selectedMeld[3]);
                if (ts.IsMoreThan4TilesOfTheSameType())
                    return;
                if (wts is IRedSuitedTile r)
                {
                    var normalTilesCount = ts.Count(x => x.Code == wts.Code && x is IRedSuitedTile xr && !xr.IsRedSuited);
                    var redTilesCount = ts.Count(x => x.Code == wts.Code && x is IRedSuitedTile xr && xr.IsRedSuited);
                    if (r.IsRedSuited && redTilesCount > 1)
                    {
                        return;
                    }
                    else if (!r.IsRedSuited && normalTilesCount == 4)
                    {
                        return;
                    }
                }
            }

            var odd = tiles.Odd(wait, selectedMeld[0], selectedMeld[1], selectedMeld[2], selectedMeld[3]);

            //4面子完成済み，1雀頭完成待ち
            ret.Add(new ManualWaitReadyHand(ww.WaitTiles, (wait as Single).Clone(IncompletedMeld.MeldStatus.WAIT), selectedMeld[0], selectedMeld[1], selectedMeld[2], selectedMeld[3]));
        }

        private static void OneHeadCreated(Tile[] hand, Meld[] exposed, List<ReadyHand> ret, Meld[] heads, List<Meld> melds)
        {
            //1雀頭完成済み
            for (int l = 0; l < heads.Count() && (exposed == null || exposed.Count() <= 3); ++l)
            {
                List<Meld> havingMelds = new List<Meld>();
                TileCollection tiles = new TileCollection(hand);

                var head = heads[l];

                var selectedMelds = Combination.Enumerate(melds, 3, withRepetition: true).ToList();
                for (int i = 0; i < selectedMelds.Count(); ++i)
                {
                    var m = new List<Meld>();
                    m.AddRange(selectedMelds[i]);
                    selectedMelds[i] = m.ToArray();
                }

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
                    a:
                    for (int p = 0; p < icMelds.Count(); ++p)
                    {
                        var wait = icMelds[p];
                        if (!tiles.IsAllContained(head, selectedMeld[0], selectedMeld[1], selectedMeld[2], wait))
                            continue;

                        foreach (var wts in wait.WaitTiles)
                        {
                            var ts = new TileCollection(new Tile[] { wts }, wait, selectedMeld[0], selectedMeld[1], selectedMeld[2]);
                            if (ts.IsMoreThan4TilesOfTheSameType())
                                continue;
                            if (wts is IRedSuitedTile r)
                            {
                                var normalTilesCount = ts.Count(x => x.Code == wts.Code && x is IRedSuitedTile xr && !xr.IsRedSuited);
                                var redTilesCount = ts.Count(x => x.Code == wts.Code && x is IRedSuitedTile xr && xr.IsRedSuited);
                                if (r.IsRedSuited && redTilesCount > 1)
                                {
                                    continue;
                                }
                                else if (!r.IsRedSuited && normalTilesCount == 4)
                                {
                                    continue;
                                }
                            }
                        }

                        if (head.Equals(wait))
                            continue;

                        CreateReadyHandWhenOneHeadCreated(ret, tiles, head, selectedMeld, wait);
                    }
                }
            }
        }

        private static void CreateReadyHandWhenOneHeadCreated(List<ReadyHand> ret, TileCollection tiles, Meld head, Meld[] selectedMeld, IncompletedMeld wait)
        {
            if (wait.MeldStatusType == IncompletedMeld.MeldStatus.WAIT)
            {
                wait.ComputeWaitTiles();
            }

            foreach (var wts in wait.WaitTiles)
            {
                var ts = new TileCollection(new Tile[] { wts }, wait, selectedMeld[0], selectedMeld[1], selectedMeld[2]);
                
                if (ts.IsMoreThan4TilesOfTheSameType())
                    continue;
                
                if (wts is IRedSuitedTile r)
                {
                    var normalTilesCount = ts.Count(x => x.Code == wts.Code && x is IRedSuitedTile xr && !xr.IsRedSuited);
                    var redTilesCount = ts.Count(x => x.Code == wts.Code && x is IRedSuitedTile xr && xr.IsRedSuited);
                    if (r.IsRedSuited && redTilesCount > 1)
                    {
                        continue;
                    }
                    else if (!r.IsRedSuited && normalTilesCount == 4)
                    {
                        continue;
                    }
                }

                //1雀頭・3面子完成済み，1搭子
                ret.Add(new ManualWaitReadyHand(wts, (head as IncompletedMeld).Clone(IncompletedMeld.MeldStatus.COMPLETED), selectedMeld[0], selectedMeld[1], selectedMeld[2], wait.Clone(IncompletedMeld.MeldStatus.WAIT)));
            }
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

        public static TileCollection ThirteenOrphansTiles()
        {
            return new TileCollection(new Tile[]
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

        public static Meld[] FindSingles(Tile[] hand, params Meld[][] meldsArray)
        {
            var h = hand.ToList();
            if (meldsArray != null)
            {
                foreach (var melds in meldsArray)
                {
                    if (melds != null)
                    {
                        foreach (var meld in melds)
                        {
                            foreach (var t in meld.Tiles)
                            {
                                h.Remove(t);
                            }
                        }
                    }
                }
            }
            return IncompletedMeldDetector.FindIncompletedDoubles(h.ToArray());
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

        public static Meld[] FindDoubles(Tile[] hand, params Meld[][] meldsArray)
        {
            var h = hand.ToList();
            if (meldsArray != null)
            {
                foreach (var melds in meldsArray)
                {
                    if (melds != null)
                    {
                        foreach (var meld in melds)
                        {
                            foreach (var t in meld.Tiles)
                            {
                                h.Remove(t);
                            }
                        }
                    }
                }
            }
            return IncompletedMeldDetector.FindIncompletedTriple(h.ToArray());
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
