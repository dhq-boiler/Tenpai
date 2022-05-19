using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Tenpai.Extensions;
using Tenpai.Models.Tiles;
using Tenpai.Models.Yaku.Meld;
using Tenpai.Models.Yaku.Meld.Detector;

namespace Tenpai.Test
{
    [TestFixture]
    public class MeldDetectorTest : TestFixtureBase
    {
        [Test]
        public void Distinct試験()
        {
            var melds = new List<IncompletedMeld>();
            melds.Add(new OpenWait<Character_1, Character_2, Character_3, Character_4>());
            melds.Add(new OpenWait<Character_2, Character_3, Character_4, Character_5>());
            melds.Add(new OpenWait<Character_1, Character_2, Character_3, Character_4>());
            melds.Add(new OpenWait<Character_1, Character_2, Character_3, Character_4>());
            //melds = melds.Distinct(new DelegateComparer<IncompletedMeld, TileCollection>(x => x.WaitTiles)).ToList();
            melds = melds.Distinct().ToList();
            Assert.That(melds, Has.Count.EqualTo(2));
            Assert.That(melds[0], Is.EqualTo(new OpenWait<Character_1, Character_2, Character_3, Character_4>()));
            Assert.That(melds[1], Is.EqualTo(new OpenWait<Character_2, Character_3, Character_4, Character_5>()));
        }

        [Test]
        public void FindCompletedHandsTest1()
        {
            TileCollection tiles = new TileCollection(new Tile[]
                {
                    Tile.CreateInstance<Character_1>(),
                    Tile.CreateInstance<Character_2>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Character_5>(),
                    Tile.CreateInstance<Character_6>(),
                    Tile.CreateInstance<Dot_1>(),
                    Tile.CreateInstance<Dot_1>(),
                    Tile.CreateInstance<Dot_3>(),
                    Tile.CreateInstance<Dot_4>(),
                    Tile.CreateInstance<Dot_5>(),
                    Tile.CreateInstance<Bamboo_4>(),
                    Tile.CreateInstance<Bamboo_5>(),
                    Tile.CreateInstance<Bamboo_6>(),
                });
            var completedHands = MeldDetector.FindCompletedHands(tiles, null, 14, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Bamboo_6>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.AreEqual(1, completedHands.Count());
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Run<Character_1,Character_2,Character_3>(),
                                                           new Run<Character_4,Character_5,Character_6>(),
                                                           new Run<Dot_3,Dot_4,Dot_5>(),
                                                           new Double<Dot_1>(),
                                                           new Run<Bamboo_4, Bamboo_5, Bamboo_6>())))
                { }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void FindCompletedHandsTest2()
        {
            //Wait Dot_6, Dot_7
            TileCollection tiles = new TileCollection(new Tile[]
                {
                    Tile.CreateInstance<Character_2>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Dot_5>(),
                    Tile.CreateInstance<Dot_6>(),
                    Tile.CreateInstance<Dot_7>(),
                    Tile.CreateInstance<Bamboo_6>(),
                    Tile.CreateInstance<Bamboo_6>(),
                    Tile.CreateInstance<Bamboo_7>(),
                    Tile.CreateInstance<Bamboo_7>(),
                    Tile.CreateInstance<Bamboo_7>(),
                    Tile.CreateInstance<Character_6>(),
                    Tile.CreateInstance<Character_7>(),
                    Tile.CreateInstance<Character_8>(),
                });
            var completedHands = MeldDetector.FindCompletedHands(tiles, null, 14, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Bamboo_6>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Run<Character_2, Character_3, Character_4>(),
                                                        new Run<Dot_5, Dot_6, Dot_7>(),
                                                        new Double<Bamboo_6>(),
                                                        new Triple<Bamboo_7>(),
                                                        new Run<Character_6, Character_7, Character_8>())))
                { }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void FindCompletedHandsTest3()
        {
            TileCollection tiles = new TileCollection(new Tile[]
                {
                    Tile.CreateInstance<Character_2>(),
                    Tile.CreateInstance<Character_2>(),
                    Tile.CreateInstance<Character_2>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Dot_5>(),
                    Tile.CreateInstance<Dot_5>(),
                    Tile.CreateInstance<Dot_5>(),
                    Tile.CreateInstance<Character_8>(),
                    Tile.CreateInstance<Character_8>(),
                });
            var completedHands = MeldDetector.FindCompletedHands(tiles, null, 14, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Character_8>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            foreach (var completedHand in completedHands)
            {
                Console.WriteLine(completedHand);
                if (completedHand.Equals(new CompletedHand(new Triple<Character_2>(),
                                                           new Triple<Character_3>(),
                                                           new Triple<Character_4>(),
                                                           new Triple<Dot_5>(),
                                                           new Double<Character_8>())))
                { }
                else if (completedHand.Equals(new CompletedHand(new Run<Character_2, Character_3, Character_4>(),
                                                                new Run<Character_2, Character_3, Character_4>(),
                                                                new Run<Character_2, Character_3, Character_4>(),
                                                                new Triple<Dot_5>(),
                                                                new Double<Character_8>())))
                { }
                else
                {
                    Assert.Fail();
                }
            }
            Assert.That(completedHands, Has.Length.EqualTo(2));
        }

        [Test]
        public void FindCompletedHandsTest4()
        {
            //Wait White
            TileCollection tiles = new TileCollection(new Tile[]
                {
                    Tile.CreateInstance<Character_2>(),
                    Tile.CreateInstance<Character_2>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Dot_5>(),
                    Tile.CreateInstance<Dot_5>(),
                    Tile.CreateInstance<Character_8>(),
                    Tile.CreateInstance<Character_8>(),
                    Tile.CreateInstance<East>(),
                    Tile.CreateInstance<East>(),
                    Tile.CreateInstance<White>(),
                    Tile.CreateInstance<White>(),
                });
            var completedHands = MeldDetector.FindCompletedHands(tiles, null, 14, ViewModels.AgariType.Tsumo, Tile.CreateInstance<White>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Double<Character_2>(),
                                                           new Double<Character_3>(),
                                                           new Double<Character_4>(),
                                                           new Double<Dot_5>(),
                                                           new Double<Character_8>(),
                                                           new Double<East>(),
                                                           new Double<White>())))
                { }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void FindCompletedHandsTest5()
        {
            //Wait Bamboo_1, Bamboo_2, Bamboo_3, Bamboo_4, Bamboo_5, Bamboo_6, Bamboo_7, Bamboo_8, Bamboo_9
            TileCollection tiles = new TileCollection(new Tile[]
                {
                    Tile.CreateInstance<Bamboo_1>(),
                    Tile.CreateInstance<Bamboo_1>(),
                    Tile.CreateInstance<Bamboo_1>(),
                    Tile.CreateInstance<Bamboo_2>(),
                    Tile.CreateInstance<Bamboo_3>(),
                    Tile.CreateInstance<Bamboo_4>(),
                    Tile.CreateInstance<Bamboo_5>(),
                    Tile.CreateInstance<Bamboo_6>(),
                    Tile.CreateInstance<Bamboo_7>(),
                    Tile.CreateInstance<Bamboo_8>(),
                    Tile.CreateInstance<Bamboo_9>(),
                    Tile.CreateInstance<Bamboo_9>(),
                    Tile.CreateInstance<Bamboo_9>(),
                });
            TileCollection adds = new TileCollection(new Tile[]
                {
                    Tile.CreateInstance<Bamboo_1>(),
                    Tile.CreateInstance<Bamboo_2>(),
                    Tile.CreateInstance<Bamboo_3>(),
                    Tile.CreateInstance<Bamboo_4>(),
                    Tile.CreateInstance<Bamboo_5>(),
                    Tile.CreateInstance<Bamboo_6>(),
                    Tile.CreateInstance<Bamboo_7>(),
                    Tile.CreateInstance<Bamboo_8>(),
                    Tile.CreateInstance<Bamboo_9>(),
                });
            foreach (var add in adds)
            {
                TileCollection addedTiles = new TileCollection(tiles);
                addedTiles.Add(add);
                var completedHands = MeldDetector.FindCompletedHands(addedTiles, null, 14, ViewModels.AgariType.Tsumo, add, ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
                foreach (var completedHand in completedHands)
                {
                    if (completedHand.Equals(new CompletedHand(new Double<Bamboo_1>(),
                                                       new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                       new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                       new Run<Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                       new Triple<Bamboo_9>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Triple<Bamboo_1>(),
                                                            new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                            new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                            new Run<Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                            new Double<Bamboo_9>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Double<Bamboo_1>(),
                                                            new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                            new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                            new Run<Bamboo_6, Bamboo_7, Bamboo_8>(),
                                                            new Triple<Bamboo_9>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Double<Bamboo_1>(),
                                                            new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                            new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                            new Run<Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                            new Triple<Bamboo_9>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Double<Bamboo_1>(),
                                                            new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                            new Run<Bamboo_3, Bamboo_4, Bamboo_5>(),
                                                            new Run<Bamboo_6, Bamboo_7, Bamboo_8>(),
                                                            new Triple<Bamboo_9>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Double<Bamboo_1>(),
                                                            new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                            new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                            new Run<Bamboo_6, Bamboo_7, Bamboo_8>(),
                                                            new Triple<Bamboo_9>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Double<Bamboo_1>(),
                                                            new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                            new Run<Bamboo_3, Bamboo_4, Bamboo_5>(),
                                                            new Run<Bamboo_6, Bamboo_7, Bamboo_8>(),
                                                            new Triple<Bamboo_9>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Triple<Bamboo_1>(),
                                                            new Run<Bamboo_2, Bamboo_3, Bamboo_4>(),
                                                            new Run<Bamboo_5, Bamboo_6, Bamboo_7>(),
                                                            new Run<Bamboo_8, Bamboo_9, Bamboo_7>(),
                                                            new Double<Bamboo_9>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Triple<Bamboo_1>(),
                                                            new Run<Bamboo_2, Bamboo_3, Bamboo_4>(),
                                                            new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                            new Run<Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                            new Double<Bamboo_9>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Triple<Bamboo_1>(),
                                                            new Run<Bamboo_2, Bamboo_3, Bamboo_4>(),
                                                            new Run<Bamboo_5, Bamboo_6, Bamboo_7>(),
                                                            new Run<Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                            new Double<Bamboo_9>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Triple<Bamboo_1>(),
                                                            new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                            new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                            new Run<Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                            new Double<Bamboo_9>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Triple<Bamboo_1>(),
                                                            new Run<Bamboo_2, Bamboo_3, Bamboo_4>(),
                                                            new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                            new Run<Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                            new Double<Bamboo_9>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Triple<Bamboo_1>(),
                                                            new Double<Bamboo_2>(),
                                                            new Run<Bamboo_3, Bamboo_4, Bamboo_5>(),
                                                            new Run<Bamboo_6, Bamboo_7, Bamboo_8>(),
                                                            new Triple<Bamboo_9>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Triple<Bamboo_1>(),
                                                            new Run<Bamboo_2, Bamboo_3, Bamboo_4>(),
                                                            new Double<Bamboo_5>(),
                                                            new Run<Bamboo_6, Bamboo_7, Bamboo_8>(),
                                                            new Triple<Bamboo_9>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Triple<Bamboo_1>(),
                                                            new Run<Bamboo_2, Bamboo_3, Bamboo_4>(),
                                                            new Run<Bamboo_5, Bamboo_6, Bamboo_7>(),
                                                            new Double<Bamboo_8>(),
                                                            new Triple<Bamboo_9>())))
                    { }
                    else
                    {
                        Assert.Fail();
                    }
                }
            }
        }

        [Test]
        public void FindCompletedHandsTest6()
        {
            //Wait Character_1, Character_9, Dot_1, Dot_9, Bamboo_1, Bamboo_9, East, South, West, North, White, Green, Red
            TileCollection tiles = new TileCollection(new Tile[]
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
                    Tile.CreateInstance<Red>(),
                });
            TileCollection addTiles = new TileCollection(new Tile[]
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
                    Tile.CreateInstance<Red>(),
                });
            foreach (var add in addTiles)
            {
                TileCollection addedTiles = new TileCollection(tiles);
                addedTiles.Add(add);
                Console.WriteLine(addedTiles);
                var completedHands = MeldDetector.FindCompletedHands(addedTiles, null, 14, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Bamboo_1>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
                Assert.AreEqual(1, completedHands.Count());
                foreach (var completedHand in completedHands)
                {
                    if (completedHand.Equals(new CompletedHand(new Double<Character_1>(),
                                                           new Single<Character_9>(),
                                                           new Single<Dot_1>(),
                                                           new Single<Dot_9>(),
                                                           new Single<Bamboo_1>(),
                                                           new Single<Bamboo_9>(),
                                                           new Single<East>(),
                                                           new Single<South>(),
                                                           new Single<West>(),
                                                           new Single<North>(),
                                                           new Single<White>(),
                                                           new Single<Green>(),
                                                           new Single<Red>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Single<Character_1>(),
                                                               new Double<Character_9>(),
                                                               new Single<Dot_1>(),
                                                               new Single<Dot_9>(),
                                                               new Single<Bamboo_1>(),
                                                               new Single<Bamboo_9>(),
                                                               new Single<East>(),
                                                               new Single<South>(),
                                                               new Single<West>(),
                                                               new Single<North>(),
                                                               new Single<White>(),
                                                               new Single<Green>(),
                                                               new Single<Red>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Single<Character_1>(),
                                                           new Single<Character_9>(),
                                                           new Double<Dot_1>(),
                                                           new Single<Dot_9>(),
                                                           new Single<Bamboo_1>(),
                                                           new Single<Bamboo_9>(),
                                                           new Single<East>(),
                                                           new Single<South>(),
                                                           new Single<West>(),
                                                           new Single<North>(),
                                                           new Single<White>(),
                                                           new Single<Green>(),
                                                           new Single<Red>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Single<Character_1>(),
                                                           new Single<Character_9>(),
                                                           new Single<Dot_1>(),
                                                           new Double<Dot_9>(),
                                                           new Single<Bamboo_1>(),
                                                           new Single<Bamboo_9>(),
                                                           new Single<East>(),
                                                           new Single<South>(),
                                                           new Single<West>(),
                                                           new Single<North>(),
                                                           new Single<White>(),
                                                           new Single<Green>(),
                                                           new Single<Red>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Single<Character_1>(),
                                                           new Single<Character_9>(),
                                                           new Single<Dot_1>(),
                                                           new Single<Dot_9>(),
                                                           new Double<Bamboo_1>(),
                                                           new Single<Bamboo_9>(),
                                                           new Single<East>(),
                                                           new Single<South>(),
                                                           new Single<West>(),
                                                           new Single<North>(),
                                                           new Single<White>(),
                                                           new Single<Green>(),
                                                           new Single<Red>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Single<Character_1>(),
                                                           new Single<Character_9>(),
                                                           new Single<Dot_1>(),
                                                           new Single<Dot_9>(),
                                                           new Single<Bamboo_1>(),
                                                           new Double<Bamboo_9>(),
                                                           new Single<East>(),
                                                           new Single<South>(),
                                                           new Single<West>(),
                                                           new Single<North>(),
                                                           new Single<White>(),
                                                           new Single<Green>(),
                                                           new Single<Red>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Single<Character_1>(),
                                                           new Single<Character_9>(),
                                                           new Single<Dot_1>(),
                                                           new Single<Dot_9>(),
                                                           new Single<Bamboo_1>(),
                                                           new Single<Bamboo_9>(),
                                                           new Double<East>(),
                                                           new Single<South>(),
                                                           new Single<West>(),
                                                           new Single<North>(),
                                                           new Single<White>(),
                                                           new Single<Green>(),
                                                           new Single<Red>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Single<Character_1>(),
                                                           new Single<Character_9>(),
                                                           new Single<Dot_1>(),
                                                           new Single<Dot_9>(),
                                                           new Single<Bamboo_1>(),
                                                           new Single<Bamboo_9>(),
                                                           new Single<East>(),
                                                           new Double<South>(),
                                                           new Single<West>(),
                                                           new Single<North>(),
                                                           new Single<White>(),
                                                           new Single<Green>(),
                                                           new Single<Red>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Single<Character_1>(),
                                                           new Single<Character_9>(),
                                                           new Single<Dot_1>(),
                                                           new Single<Dot_9>(),
                                                           new Single<Bamboo_1>(),
                                                           new Single<Bamboo_9>(),
                                                           new Single<East>(),
                                                           new Single<South>(),
                                                           new Double<West>(),
                                                           new Single<North>(),
                                                           new Single<White>(),
                                                           new Single<Green>(),
                                                           new Single<Red>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Single<Character_1>(),
                                                           new Single<Character_9>(),
                                                           new Single<Dot_1>(),
                                                           new Single<Dot_9>(),
                                                           new Single<Bamboo_1>(),
                                                           new Single<Bamboo_9>(),
                                                           new Single<East>(),
                                                           new Single<South>(),
                                                           new Single<West>(),
                                                           new Double<North>(),
                                                           new Single<White>(),
                                                           new Single<Green>(),
                                                           new Single<Red>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Single<Character_1>(),
                                                           new Single<Character_9>(),
                                                           new Single<Dot_1>(),
                                                           new Single<Dot_9>(),
                                                           new Single<Bamboo_1>(),
                                                           new Single<Bamboo_9>(),
                                                           new Single<East>(),
                                                           new Single<South>(),
                                                           new Single<West>(),
                                                           new Single<North>(),
                                                           new Double<White>(),
                                                           new Single<Green>(),
                                                           new Single<Red>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Single<Character_1>(),
                                                           new Single<Character_9>(),
                                                           new Single<Dot_1>(),
                                                           new Single<Dot_9>(),
                                                           new Single<Bamboo_1>(),
                                                           new Single<Bamboo_9>(),
                                                           new Single<East>(),
                                                           new Single<South>(),
                                                           new Single<West>(),
                                                           new Single<North>(),
                                                           new Single<White>(),
                                                           new Double<Green>(),
                                                           new Single<Red>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Single<Character_1>(),
                                                           new Single<Character_9>(),
                                                           new Single<Dot_1>(),
                                                           new Single<Dot_9>(),
                                                           new Single<Bamboo_1>(),
                                                           new Single<Bamboo_9>(),
                                                           new Single<East>(),
                                                           new Single<South>(),
                                                           new Single<West>(),
                                                           new Single<North>(),
                                                           new Single<White>(),
                                                           new Single<Green>(),
                                                           new Double<Red>())))
                    { }
                    else
                    {
                        Assert.Fail();
                    }
                }
            }
        }

        [Test]
        public void FindCompletedHandsTest7()
        {
            //Wait Bamboo_4, Bamboo_7
            TileCollection tiles = new TileCollection(new Tile[]
                {
                    Tile.CreateInstance<Bamboo_5>(),
                    Tile.CreateInstance<Bamboo_6>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Character_5>(),
                    Tile.CreateInstance<Dot_1>(),
                    Tile.CreateInstance<Dot_1>(),
                    Tile.CreateInstance<Dot_6>(),
                    Tile.CreateInstance<Dot_7>(),
                    Tile.CreateInstance<Dot_8>(),
                });
            Meld[] exposed = new Meld[]
                {
                    new Run<Bamboo_6, Bamboo_7, Bamboo_8>()
                };
            TileCollection adds = new TileCollection(new Tile[]
                {
                    Tile.CreateInstance<Bamboo_4>(),
                    Tile.CreateInstance<Bamboo_7>()
                });
            foreach (var add in adds)
            {
                var ts = new TileCollection(tiles);
                ts.Add(add);
                var completedHands = MeldDetector.FindCompletedHands(ts, exposed, 14, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Bamboo_6>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
                Assert.AreEqual(1, completedHands.Count());
                foreach (var completedHand in completedHands)
                {
                    if (completedHand.Equals(new CompletedHand(new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                               new Run<Character_3, Character_4, Character_5>(),
                                                               new Double<Dot_1>(),
                                                               new Run<Dot_6, Dot_7, Dot_8>(),
                                                               new Run<Bamboo_6, Bamboo_7, Bamboo_8>())))
                    { }
                    else if (completedHand.Equals(new CompletedHand(new Run<Bamboo_5, Bamboo_6, Bamboo_7>(),
                                                                    new Run<Character_3, Character_4, Character_5>(),
                                                                    new Double<Dot_1>(),
                                                                    new Run<Dot_6, Dot_7, Dot_8>(),
                                                                    new Run<Bamboo_6, Bamboo_7, Bamboo_8>())))
                    { }
                    else
                    {
                        Assert.Fail();
                    }
                }
            }
        }

        [Test]
        public void FindReadyHandsTest1()
        {
            //Wait Bamboo_4, Bamboo_7
            TileCollection tiles = new TileCollection(new Tile[]
                {
                    Tile.CreateInstance<Character_1>(),
                    Tile.CreateInstance<Character_2>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Character_5>(),
                    Tile.CreateInstance<Character_6>(),
                    Tile.CreateInstance<Dot_1>(),
                    Tile.CreateInstance<Dot_1>(),
                    Tile.CreateInstance<Dot_3>(),
                    Tile.CreateInstance<Dot_4>(),
                    Tile.CreateInstance<Dot_5>(),
                    Tile.CreateInstance<Dummy>(),
                    Tile.CreateInstance<Bamboo_5>(),
                    Tile.CreateInstance<Bamboo_6>(),
                    Tile.CreateInstance<Dummy>(),
                });
            var readyHands = MeldDetector.FindReadyHands(tiles, null, 13, ViewModels.AgariType.Tsumo, ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            foreach (var readyHand in readyHands)
            {
                if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_4>(),
                                                             new Run<Character_1, Character_2, Character_3>(),
                                                             new Run<Character_4, Character_5, Character_6>(),
                                                             new Run<Dot_3, Dot_4, Dot_5>(),
                                                             new Double<Dot_1>(),
                                                             new OpenWait<Bamboo_4, Bamboo_5, Bamboo_6, Bamboo_7>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_7>(),
                                                                  new Run<Character_1, Character_2, Character_3>(),
                                                                  new Run<Character_4, Character_5, Character_6>(),
                                                                  new Run<Dot_3, Dot_4, Dot_5>(),
                                                                  new Double<Dot_1>(),
                                                                  new OpenWait<Bamboo_4, Bamboo_5, Bamboo_6, Bamboo_7>())))
                { }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void FindReadyHandsTest2()
        {
            //Wait Dot_6, Dot_7
            TileCollection tiles = new TileCollection(new Tile[]
                {
                    Tile.CreateInstance<Character_2>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Dot_5>(),
                    Tile.CreateInstance<Dot_6>(),
                    Tile.CreateInstance<Dot_7>(),
                    Tile.CreateInstance<Bamboo_6>(),
                    Tile.CreateInstance<Bamboo_6>(),
                    Tile.CreateInstance<Dummy>(),
                    Tile.CreateInstance<Bamboo_7>(),
                    Tile.CreateInstance<Bamboo_7>(),
                    Tile.CreateInstance<Dummy>(),
                    Tile.CreateInstance<Character_6>(),
                    Tile.CreateInstance<Character_7>(),
                    Tile.CreateInstance<Character_8>(),
                });
            var readyHands = MeldDetector.FindReadyHands(tiles, null, 13, ViewModels.AgariType.Tsumo, ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            foreach (var readyHand in readyHands)
            {
                if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_6>(),
                                                             new Run<Character_2, Character_3, Character_4>(),
                                                             new Run<Dot_5, Dot_6, Dot_7>(),
                                                             new Double<Bamboo_6>(),
                                                             new Double<Bamboo_7>(),
                                                             new Run<Character_6, Character_7, Character_8>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_7>(),
                                                                  new Run<Character_2, Character_3, Character_4>(),
                                                                  new Run<Dot_5, Dot_6, Dot_7>(),
                                                                  new Double<Bamboo_6>(),
                                                                  new Double<Bamboo_7>(),
                                                                  new Run<Character_6, Character_7, Character_8>())))
                { }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void FindReadyHandsTest3()
        {
            //Wait Dot_5, Character_8
            TileCollection tiles = new TileCollection(new Tile[]
                {
                    Tile.CreateInstance<Character_2>(),
                    Tile.CreateInstance<Character_2>(),
                    Tile.CreateInstance<Character_2>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Dot_5>(),
                    Tile.CreateInstance<Dot_5>(),
                    Tile.CreateInstance<Dummy>(),
                    Tile.CreateInstance<Character_8>(),
                    Tile.CreateInstance<Character_8>(),
                    Tile.CreateInstance<Dummy>(),
                });
            var readyHands = MeldDetector.FindReadyHands(tiles, null, 13, ViewModels.AgariType.Tsumo, ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            foreach (var readyHand in readyHands)
            {
                if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Dot_5>(),
                                                             new Triple<Character_2>(),
                                                             new Triple<Character_3>(),
                                                             new Triple<Character_4>(),
                                                             new Double<Dot_5>(),
                                                             new Double<Character_8>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Dot_5>(),
                                                             new Run<Character_2, Character_3, Character_4>(),
                                                             new Run<Character_2, Character_3, Character_4>(),
                                                             new Run<Character_2, Character_3, Character_4>(),
                                                             new Double<Dot_5>(),
                                                             new Double<Character_8>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Character_8>(),
                                                                  new Triple<Character_2>(),
                                                                  new Triple<Character_3>(),
                                                                  new Triple<Character_4>(),
                                                                  new Double<Dot_5>(),
                                                                  new Double<Character_8>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Character_8>(),
                                                             new Run<Character_2, Character_3, Character_4>(),
                                                             new Run<Character_2, Character_3, Character_4>(),
                                                             new Run<Character_2, Character_3, Character_4>(),
                                                             new Double<Dot_5>(),
                                                             new Double<Character_8>())))
                { }
                else
                {
                    Assert.Fail(readyHand.ToString());
                }
            }
        }

        [Test]
        public void FindReadyHandsTest4()
        {
            //Wait White
            TileCollection tiles = new TileCollection(new Tile[]
                {
                    Tile.CreateInstance<Character_2>(),
                    Tile.CreateInstance<Character_2>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_3>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Character_4>(),
                    Tile.CreateInstance<Dot_5>(),
                    Tile.CreateInstance<Dot_5>(),
                    Tile.CreateInstance<Character_8>(),
                    Tile.CreateInstance<Character_8>(),
                    Tile.CreateInstance<East>(),
                    Tile.CreateInstance<East>(),
                    Tile.CreateInstance<White>(),
                    Tile.CreateInstance<Dummy>(),
                });
            var readyHands = MeldDetector.FindReadyHands(tiles, null, 13, ViewModels.AgariType.Tsumo, ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            foreach (var readyHand in readyHands)
            {
                if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<White>(),
                                                             new Double<Character_2>(),
                                                             new Double<Character_3>(),
                                                             new Double<Character_4>(),
                                                             new Double<Dot_5>(),
                                                             new Double<Character_8>(),
                                                             new Double<East>(),
                                                             new Single<White>())))
                { }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void 純正九蓮宝燈9面待ち()
        {
            //Wait Bamboo_1, Bamboo_2, Bamboo_3, Bamboo_4, Bamboo_5, Bamboo_6, Bamboo_7, Bamboo_8, Bamboo_9
            TileCollection tiles = new TileCollection(new Tile[]
                {
                    Tile.CreateInstance<Bamboo_1>(),
                    Tile.CreateInstance<Bamboo_1>(),
                    Tile.CreateInstance<Bamboo_1>(),
                    Tile.CreateInstance<Bamboo_2>(),
                    Tile.CreateInstance<Bamboo_3>(),
                    Tile.CreateInstance<Bamboo_4>(),
                    Tile.CreateInstance<Bamboo_5>(),
                    Tile.CreateInstance<Bamboo_6>(),
                    Tile.CreateInstance<Bamboo_7>(),
                    Tile.CreateInstance<Bamboo_8>(),
                    Tile.CreateInstance<Bamboo_9>(),
                    Tile.CreateInstance<Bamboo_9>(),
                    Tile.CreateInstance<Bamboo_9>(),
                });
            var readyHands = MeldDetector.FindReadyHands(tiles, null, 13, ViewModels.AgariType.Tsumo, ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East).OrderBy(x => x.WaitingTiles.ElementAt(0).ToString());
            foreach (var readyHand in readyHands)
            {
                if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_1>(),
                                                             new Double<Bamboo_1>(),
                                                             new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                             new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                             new Run<Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                             new Double<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_9>(),
                                                                  new Double<Bamboo_1>(),
                                                                  new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                                  new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                                  new Run<Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                                  new Double<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_6>(),
                                                                  new Double<Bamboo_1>(),
                                                                  new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                                  new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                                  new OpenWait<Bamboo_6, Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                                  new Triple<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_9>(),
                                                                  new Double<Bamboo_1>(),
                                                                  new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                                  new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                                  new OpenWait<Bamboo_6, Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                                  new Triple<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_3>(),
                                                                  new Double<Bamboo_1>(),
                                                                  new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                                  new OpenWait<Bamboo_3, Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                                  new Run<Bamboo_6, Bamboo_7, Bamboo_8>(),
                                                                  new Triple<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_6>(),
                                                                  new Double<Bamboo_1>(),
                                                                  new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                                  new OpenWait<Bamboo_3, Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                                  new Run<Bamboo_6, Bamboo_7, Bamboo_8>(),
                                                                  new Triple<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_3>(),
                                                                  new Double<Bamboo_1>(),
                                                                  new EdgeWait<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                                  new Run<Bamboo_3, Bamboo_4, Bamboo_5>(),
                                                                  new Run<Bamboo_6, Bamboo_7, Bamboo_8>(),
                                                                  new Triple<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_7>(),
                                                                  new Triple<Bamboo_1>(),
                                                                  new Run<Bamboo_2, Bamboo_3, Bamboo_4>(),
                                                                  new Run<Bamboo_5, Bamboo_6, Bamboo_7>(),
                                                                  new EdgeWait<Bamboo_8, Bamboo_9, Bamboo_7>(),
                                                                  new Double<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_4>(),
                                                                  new Triple<Bamboo_1>(),
                                                                  new Run<Bamboo_2, Bamboo_3, Bamboo_4>(),
                                                                  new OpenWait<Bamboo_4, Bamboo_5, Bamboo_6, Bamboo_7>(),
                                                                  new Run<Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                                  new Double<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_7>(),
                                                                  new Triple<Bamboo_1>(),
                                                                  new Run<Bamboo_2, Bamboo_3, Bamboo_4>(),
                                                                  new OpenWait<Bamboo_4, Bamboo_5, Bamboo_6, Bamboo_7>(),
                                                                  new Run<Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                                  new Double<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_1>(),
                                                                  new Triple<Bamboo_1>(),
                                                                  new OpenWait<Bamboo_1, Bamboo_2, Bamboo_3, Bamboo_4>(),
                                                                  new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                                  new Run<Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                                  new Double<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_4>(),
                                                                  new Triple<Bamboo_1>(),
                                                                  new OpenWait<Bamboo_1, Bamboo_2, Bamboo_3, Bamboo_4>(),
                                                                  new Run<Bamboo_4, Bamboo_5, Bamboo_6>(),
                                                                  new Run<Bamboo_7, Bamboo_8, Bamboo_9>(),
                                                                  new Double<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_2>(),
                                                                  new Triple<Bamboo_1>(),
                                                                  new Single<Bamboo_2>(),
                                                                  new Run<Bamboo_3, Bamboo_4, Bamboo_5>(),
                                                                  new Run<Bamboo_6, Bamboo_7, Bamboo_8>(),
                                                                  new Triple<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_5>(),
                                                                  new Triple<Bamboo_1>(),
                                                                  new Run<Bamboo_2, Bamboo_3, Bamboo_4>(),
                                                                  new Single<Bamboo_5>(),
                                                                  new Run<Bamboo_6, Bamboo_7, Bamboo_8>(),
                                                                  new Triple<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_8>(),
                                                                  new Triple<Bamboo_1>(),
                                                                  new Run<Bamboo_2, Bamboo_3, Bamboo_4>(),
                                                                  new Run<Bamboo_5, Bamboo_6, Bamboo_7>(),
                                                                  new Single<Bamboo_8>(),
                                                                  new Triple<Bamboo_9>())))
                {
                    Console.WriteLine(readyHand);
                }
                else
                {
                    Console.WriteLine($"FAILED:{readyHand}");
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void 国士無双十三面待ち()
        {
            //Wait Character_1, Character_9, Dot_1, Dot_9, Bamboo_1, Bamboo_9, East, South, West, North, White, Green, Red
            TileCollection tiles = new TileCollection(new Tile[]
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
                    Tile.CreateInstance<Red>(),
                });
            var readyHands = MeldDetector.FindReadyHands(tiles, null, 13, ViewModels.AgariType.Tsumo, ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.AreEqual(13, readyHands.Count());
            foreach (var readyHand in readyHands)
            {
                if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Character_1>(),
                                                             new Single<Character_1>(),
                                                             new Single<Character_9>(),
                                                             new Single<Dot_1>(),
                                                             new Single<Dot_9>(),
                                                             new Single<Bamboo_1>(),
                                                             new Single<Bamboo_9>(),
                                                             new Single<East>(),
                                                             new Single<South>(),
                                                             new Single<West>(),
                                                             new Single<North>(),
                                                             new Single<White>(),
                                                             new Single<Green>(),
                                                             new Single<Red>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Character_9>(),
                                                                  new Single<Character_1>(),
                                                                  new Single<Character_9>(),
                                                                  new Single<Dot_1>(),
                                                                  new Single<Dot_9>(),
                                                                  new Single<Bamboo_1>(),
                                                                  new Single<Bamboo_9>(),
                                                                  new Single<East>(),
                                                                  new Single<South>(),
                                                                  new Single<West>(),
                                                                  new Single<North>(),
                                                                  new Single<White>(),
                                                                  new Single<Green>(),
                                                                  new Single<Red>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Dot_1>(),
                                                                  new Single<Character_1>(),
                                                                  new Single<Character_9>(),
                                                                  new Single<Dot_1>(),
                                                                  new Single<Dot_9>(),
                                                                  new Single<Bamboo_1>(),
                                                                  new Single<Bamboo_9>(),
                                                                  new Single<East>(),
                                                                  new Single<South>(),
                                                                  new Single<West>(),
                                                                  new Single<North>(),
                                                                  new Single<White>(),
                                                                  new Single<Green>(),
                                                                  new Single<Red>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Dot_9>(),
                                                                  new Single<Character_1>(),
                                                                  new Single<Character_9>(),
                                                                  new Single<Dot_1>(),
                                                                  new Single<Dot_9>(),
                                                                  new Single<Bamboo_1>(),
                                                                  new Single<Bamboo_9>(),
                                                                  new Single<East>(),
                                                                  new Single<South>(),
                                                                  new Single<West>(),
                                                                  new Single<North>(),
                                                                  new Single<White>(),
                                                                  new Single<Green>(),
                                                                  new Single<Red>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_1>(),
                                                                  new Single<Character_1>(),
                                                                  new Single<Character_9>(),
                                                                  new Single<Dot_1>(),
                                                                  new Single<Dot_9>(),
                                                                  new Single<Bamboo_1>(),
                                                                  new Single<Bamboo_9>(),
                                                                  new Single<East>(),
                                                                  new Single<South>(),
                                                                  new Single<West>(),
                                                                  new Single<North>(),
                                                                  new Single<White>(),
                                                                  new Single<Green>(),
                                                                  new Single<Red>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_9>(),
                                                                  new Single<Character_1>(),
                                                                  new Single<Character_9>(),
                                                                  new Single<Dot_1>(),
                                                                  new Single<Dot_9>(),
                                                                  new Single<Bamboo_1>(),
                                                                  new Single<Bamboo_9>(),
                                                                  new Single<East>(),
                                                                  new Single<South>(),
                                                                  new Single<West>(),
                                                                  new Single<North>(),
                                                                  new Single<White>(),
                                                                  new Single<Green>(),
                                                                  new Single<Red>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<East>(),
                                                                  new Single<Character_1>(),
                                                                  new Single<Character_9>(),
                                                                  new Single<Dot_1>(),
                                                                  new Single<Dot_9>(),
                                                                  new Single<Bamboo_1>(),
                                                                  new Single<Bamboo_9>(),
                                                                  new Single<East>(),
                                                                  new Single<South>(),
                                                                  new Single<West>(),
                                                                  new Single<North>(),
                                                                  new Single<White>(),
                                                                  new Single<Green>(),
                                                                  new Single<Red>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<South>(),
                                                                  new Single<Character_1>(),
                                                                  new Single<Character_9>(),
                                                                  new Single<Dot_1>(),
                                                                  new Single<Dot_9>(),
                                                                  new Single<Bamboo_1>(),
                                                                  new Single<Bamboo_9>(),
                                                                  new Single<East>(),
                                                                  new Single<South>(),
                                                                  new Single<West>(),
                                                                  new Single<North>(),
                                                                  new Single<White>(),
                                                                  new Single<Green>(),
                                                                  new Single<Red>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<West>(),
                                                                  new Single<Character_1>(),
                                                                  new Single<Character_9>(),
                                                                  new Single<Dot_1>(),
                                                                  new Single<Dot_9>(),
                                                                  new Single<Bamboo_1>(),
                                                                  new Single<Bamboo_9>(),
                                                                  new Single<East>(),
                                                                  new Single<South>(),
                                                                  new Single<West>(),
                                                                  new Single<North>(),
                                                                  new Single<White>(),
                                                                  new Single<Green>(),
                                                                  new Single<Red>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<North>(),
                                                                  new Single<Character_1>(),
                                                                  new Single<Character_9>(),
                                                                  new Single<Dot_1>(),
                                                                  new Single<Dot_9>(),
                                                                  new Single<Bamboo_1>(),
                                                                  new Single<Bamboo_9>(),
                                                                  new Single<East>(),
                                                                  new Single<South>(),
                                                                  new Single<West>(),
                                                                  new Single<North>(),
                                                                  new Single<White>(),
                                                                  new Single<Green>(),
                                                                  new Single<Red>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<White>(),
                                                                  new Single<Character_1>(),
                                                                  new Single<Character_9>(),
                                                                  new Single<Dot_1>(),
                                                                  new Single<Dot_9>(),
                                                                  new Single<Bamboo_1>(),
                                                                  new Single<Bamboo_9>(),
                                                                  new Single<East>(),
                                                                  new Single<South>(),
                                                                  new Single<West>(),
                                                                  new Single<North>(),
                                                                  new Single<White>(),
                                                                  new Single<Green>(),
                                                                  new Single<Red>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Green>(),
                                                                  new Single<Character_1>(),
                                                                  new Single<Character_9>(),
                                                                  new Single<Dot_1>(),
                                                                  new Single<Dot_9>(),
                                                                  new Single<Bamboo_1>(),
                                                                  new Single<Bamboo_9>(),
                                                                  new Single<East>(),
                                                                  new Single<South>(),
                                                                  new Single<West>(),
                                                                  new Single<North>(),
                                                                  new Single<White>(),
                                                                  new Single<Green>(),
                                                                  new Single<Red>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Red>(),
                                                                  new Single<Character_1>(),
                                                                  new Single<Character_9>(),
                                                                  new Single<Dot_1>(),
                                                                  new Single<Dot_9>(),
                                                                  new Single<Bamboo_1>(),
                                                                  new Single<Bamboo_9>(),
                                                                  new Single<East>(),
                                                                  new Single<South>(),
                                                                  new Single<West>(),
                                                                  new Single<North>(),
                                                                  new Single<White>(),
                                                                  new Single<Green>(),
                                                                  new Single<Red>())))
                { }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void FindReadyHandsTest7()
        {
            //Wait Dot_1
            TileCollection tiles = new TileCollection(new Tile[]
                {
                    Tile.CreateInstance<Character_1>(),
                    Tile.CreateInstance<Character_9>(),
                    Tile.CreateInstance<Dummy>(),
                    Tile.CreateInstance<Dot_9>(),
                    Tile.CreateInstance<Bamboo_1>(),
                    Tile.CreateInstance<Bamboo_9>(),
                    Tile.CreateInstance<East>(),
                    Tile.CreateInstance<South>(),
                    Tile.CreateInstance<West>(),
                    Tile.CreateInstance<North>(),
                    Tile.CreateInstance<White>(),
                    Tile.CreateInstance<Green>(),
                    Tile.CreateInstance<Red>(),
                    Tile.CreateInstance<Red>(),
                });
            var readyHands = MeldDetector.FindReadyHands(tiles, null, 13, ViewModels.AgariType.Tsumo, ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.AreEqual(1, readyHands.Count());
            foreach (var readyHand in readyHands)
            {
                if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Dot_1>(),
                                                             new Single<Character_1>(),
                                                             new Single<Character_9>(),
                                                             new ThirteenWait<Dot_1>(),
                                                             new Single<Dot_9>(),
                                                             new Single<Bamboo_1>(),
                                                             new Single<Bamboo_9>(),
                                                             new Single<East>(),
                                                             new Single<South>(),
                                                             new Single<West>(),
                                                             new Single<North>(),
                                                             new Single<White>(),
                                                             new Single<Green>(),
                                                             new Double<Red>())))
                { }
                else
                {
                    Assert.Fail();
                }
            }
        }

        [Test]
        public void FirstSinglesTest1()
        {
            TileCollection tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Dot_4>(),
                Tile.CreateInstance<Dot_5>(),
                Tile.CreateInstance<Dot_6>(),
                Tile.CreateInstance<Dot_7>(),
                Tile.CreateInstance<White>(),
                Tile.CreateInstance<White>(),
            });
            var singles = MeldDetector.FindSingles(tiles.ToArray());
            Assert.AreEqual(4, singles.Count());
            Assert.AreEqual(new Single<Dot_4>(), singles[0]);
            Assert.AreEqual(new Single<Dot_5>(), singles[1]);
            Assert.AreEqual(new Single<Dot_6>(), singles[2]);
            Assert.AreEqual(new Single<Dot_7>(), singles[3]);
        }

        [Test]
        public void FirstSinglesTest2()
        {
            TileCollection tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Dot_4>(),
                Tile.CreateInstance<Dot_5>(),
                Tile.CreateInstance<Dot_6>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<White>(),
                Tile.CreateInstance<White>(),
            });
            var singles = MeldDetector.FindSingles(tiles.ToArray());
            Assert.AreEqual(4, singles.Count());
            Assert.AreEqual(new Single<Dot_4>(), singles[0]);
            Assert.AreEqual(new Single<Dot_5>(), singles[1]);
            Assert.AreEqual(new Single<Dot_6>(), singles[2]);
            Assert.AreEqual(new Single<Dot_9>(), singles[3]);
        }

        [Test]
        public void FirstSinglesTest3()
        {
            TileCollection tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_2>(),
                Tile.CreateInstance<Bamboo_3>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_5>(),
                Tile.CreateInstance<Bamboo_6>(),
                Tile.CreateInstance<Bamboo_7>(),
                Tile.CreateInstance<Bamboo_8>(),
                Tile.CreateInstance<Bamboo_9>(),
                Tile.CreateInstance<Bamboo_9>(),
                Tile.CreateInstance<Bamboo_9>(),
            });
            var singles = MeldDetector.FindSingles(tiles.ToArray());
            Assert.AreEqual(7, singles.Count());
            Assert.AreEqual(new Single<Bamboo_2>(), singles[0]);
            Assert.AreEqual(new Single<Bamboo_3>(), singles[1]);
            Assert.AreEqual(new Single<Bamboo_4>(), singles[2]);
            Assert.AreEqual(new Single<Bamboo_5>(), singles[3]);
            Assert.AreEqual(new Single<Bamboo_6>(), singles[4]);
            Assert.AreEqual(new Single<Bamboo_7>(), singles[5]);
            Assert.AreEqual(new Single<Bamboo_8>(), singles[6]);
        }

        

        [Test]
        public void FindDoublesTest()
        {
            TileCollection tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Dot_4>(),
                Tile.CreateInstance<Dot_5>(),
                Tile.CreateInstance<Dot_6>(),
                Tile.CreateInstance<Dot_7>(),
                Tile.CreateInstance<White>(),
                Tile.CreateInstance<White>(),
            });
            var doubles = MeldDetector.FindDoubles(tiles.ToArray());
            Assert.AreEqual(3, doubles.Count());
            Assert.AreEqual(Tile.CreateInstance<Character_3>(), doubles[0].Tiles[0]);
            Assert.AreEqual(Tile.CreateInstance<Bamboo_4>(), doubles[1].Tiles[0]);
            Assert.AreEqual(Tile.CreateInstance<White>(), doubles[2].Tiles[0]);
        }

        [Test]
        public void FindTriplesTest()
        {
            TileCollection tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Dot_4>(),
                Tile.CreateInstance<Dot_5>(),
                Tile.CreateInstance<Dot_6>(),
                Tile.CreateInstance<Dot_7>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<White>(),
                Tile.CreateInstance<White>(),
            });
            var triples = MeldDetector.FindTriples(tiles.ToArray());
            Assert.AreEqual(2, triples.Count());
            Assert.AreEqual(Tile.CreateInstance<Character_3>(), triples[0].Tiles[0]);
            Assert.AreEqual(Tile.CreateInstance<Bamboo_4>(), triples[1].Tiles[0]);
        }

        [Test]
        public void FindRunsTest()
        {
            TileCollection tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_2>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_4>(),
                Tile.CreateInstance<Character_5>(),
                Tile.CreateInstance<Character_6>(),
                Tile.CreateInstance<Character_7>(),
                Tile.CreateInstance<Character_8>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
            });
            var runs = MeldDetector.FindRuns(tiles.ToArray());
            Assert.AreEqual(7, runs.Count());
            Internal_FindRunsTest<Character_1, Character_2, Character_3>(runs[0]);
            Internal_FindRunsTest<Character_2, Character_3, Character_4>(runs[1]);
            Internal_FindRunsTest<Character_3, Character_4, Character_5>(runs[2]);
            Internal_FindRunsTest<Character_4, Character_5, Character_6>(runs[3]);
            Internal_FindRunsTest<Character_5, Character_6, Character_7>(runs[4]);
            Internal_FindRunsTest<Character_6, Character_7, Character_8>(runs[5]);
            Internal_FindRunsTest<Character_7, Character_8, Character_9>(runs[6]);
        }

        private void Internal_FindRunsTest<T1, T2, T3>(Meld meld)
            where T1 : Tile, new()
            where T2 : Tile, new()
            where T3 : Tile, new()
        {
            Assert.AreEqual(new Run(Tile.CreateInstance<T1>(),
                Tile.CreateInstance<T2>(),
                Tile.CreateInstance<T3>()), meld);
        }

        [Test]
        public void FindQuadsTest()
        {
            TileCollection tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Dot_4>(),
                Tile.CreateInstance<Dot_5>(),
                Tile.CreateInstance<Dot_6>(),
                Tile.CreateInstance<Dot_7>(),
                Tile.CreateInstance<White>(),
                Tile.CreateInstance<White>(),
            });
            var quads = MeldDetector.FindQuads(tiles.ToArray());
            Assert.AreEqual(1, quads.Count());
            Assert.AreEqual(Tile.CreateInstance<Bamboo_4>(), quads[0].Tiles[0]);
        }

        [Test]
        public void 一盃口を含む形()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Character_2>(),
                Tile.CreateInstance<Character_2>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_4>(),
                Tile.CreateInstance<Character_4>(),
                Tile.CreateInstance<Character_4>(),
                Tile.CreateInstance<Dot_7>(),
                Tile.CreateInstance<Dot_8>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Bamboo_6>(),
                Tile.CreateInstance<Bamboo_6>(),
            });
            var completedHands = MeldDetector.FindCompletedHands(tiles, null, 14, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Character_5>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Run<Character_2, Character_3, Character_4>(),
                                                           new Run<Character_2, Character_3, Character_4>(),
                                                           new Run<Character_3, Character_4, Character_5>(),
                                                           new Run<Dot_7, Dot_8, Dot_9>(),
                                                           new Double<Bamboo_6>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 一盃口を含む形_ReadyHand()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Character_2>(),
                Tile.CreateInstance<Character_2>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_3>(),
                Tile.CreateInstance<Character_4>(),
                Tile.CreateInstance<Character_4>(),
                Tile.CreateInstance<Character_4>(),
                Tile.CreateInstance<Dot_7>(),
                Tile.CreateInstance<Dot_8>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Bamboo_6>(),
                Tile.CreateInstance<Bamboo_6>(),
            });
            var readyHands = MeldDetector.FindReadyHands(tiles, null, 13, ViewModels.AgariType.Tsumo, ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(readyHands, Has.Length.EqualTo(5));
            foreach (var readyHand in readyHands)
            {
                if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Character_2>(),
                                                             new Run<Character_2, Character_3, Character_4>(),
                                                             new Run<Character_2, Character_3, Character_4>(),
                                                             new OpenWait<Character_2, Character_3, Character_4, Character_5>(),
                                                             new Run<Dot_7, Dot_8, Dot_9>(),
                                                             new Double<Bamboo_6>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Character_2>(),
                                                                  new Double<Character_2>(),
                                                                  new Triple<Character_3>(),
                                                                  new Triple<Character_4>(),
                                                                  new Run<Dot_7, Dot_8, Dot_9>(),
                                                                  new Double<Bamboo_6>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Character_5>(),
                                                                  new Run<Character_2, Character_3, Character_4>(),
                                                                  new Run<Character_2, Character_3, Character_4>(),
                                                                  new OpenWait<Character_2, Character_3, Character_4, Character_5>(),
                                                                  new Run<Dot_7, Dot_8, Dot_9>(),
                                                                  new Double<Bamboo_6>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateRedInstance<Character_5>(),
                                                                  new Run<Character_2, Character_3, Character_4>(),
                                                                  new Run<Character_2, Character_3, Character_4>(),
                                                                  new OpenWait<Character_2, Character_3, Character_4, Character_5>(),
                                                                  new Run<Dot_7, Dot_8, Dot_9>(),
                                                                  new Double<Bamboo_6>())))
                { }
                else if (readyHand.Equals(new ManualWaitReadyHand(Tile.CreateInstance<Bamboo_6>(),
                                                                  new Double<Character_2>(),
                                                                  new Triple<Character_3>(),
                                                                  new Triple<Character_4>(),
                                                                  new Run<Dot_7, Dot_8, Dot_9>(),
                                                                  new Double<Bamboo_6>())))
                { }
                else
                {
                    Assert.Fail(readyHand.ToString());
                }
            }
        }

        [Test]
        public void 混全帯么九()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_2>(),
                Tile.CreateInstance<Dot_3>(),
                Tile.CreateInstance<Character_7>(),
                Tile.CreateInstance<Character_8>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Red>(),
                Tile.CreateInstance<Red>(),
                Tile.CreateInstance<Red>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<East>(),
                Tile.CreateInstance<East>(),
            });
            var completedHands = MeldDetector.FindCompletedHands(tiles, null, 14, ViewModels.AgariType.Tsumo, Tile.CreateInstance<East>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Run<Dot_1, Dot_2, Dot_3>(),
                                                           new Run<Character_7, Character_8, Character_9>(),
                                                           new Triple<Red>(),
                                                           new Triple<North>(),
                                                           new Double<East>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 混全帯么九_大明槓()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_2>(),
                Tile.CreateInstance<Dot_3>(),
                Tile.CreateInstance<Character_7>(),
                Tile.CreateInstance<Character_8>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Red>(),
                Tile.CreateInstance<Red>(),
                Tile.CreateInstance<Red>(),
                Tile.CreateInstance<East>(),
                Tile.CreateInstance<East>(),
            });
            var melds = new Meld[]
            {
                new Quad<North>(KongType.LargeMeldedKong)
            };
            var completedHands = MeldDetector.FindCompletedHands(tiles, melds, 15, ViewModels.AgariType.Tsumo, Tile.CreateInstance<East>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Run<Dot_1, Dot_2, Dot_3>(),
                                                           new Run<Character_7, Character_8, Character_9>(),
                                                           new Triple<Red>(),
                                                           new Quad<North>(KongType.LargeMeldedKong),
                                                           new Double<East>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 混全帯么九_暗槓()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_2>(),
                Tile.CreateInstance<Dot_3>(),
                Tile.CreateInstance<Character_7>(),
                Tile.CreateInstance<Character_8>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Red>(),
                Tile.CreateInstance<Red>(),
                Tile.CreateInstance<Red>(),
                Tile.CreateInstance<East>(),
                Tile.CreateInstance<East>(),
            });
            var melds = new Meld[]
            {
                new Quad<North>(KongType.ConcealedKong)
            };
            var completedHands = MeldDetector.FindCompletedHands(tiles, melds, 15, ViewModels.AgariType.Tsumo, Tile.CreateInstance<East>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Run<Dot_1, Dot_2, Dot_3>(),
                                                           new Run<Character_7, Character_8, Character_9>(),
                                                           new Triple<Red>(),
                                                           new Quad<North>(KongType.ConcealedKong),
                                                           new Double<East>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 純全帯么九_門前()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_2>(),
                Tile.CreateInstance<Dot_3>(),
                Tile.CreateInstance<Character_7>(),
                Tile.CreateInstance<Character_8>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_2>(),
                Tile.CreateInstance<Bamboo_3>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),

            });
            var completedHands = MeldDetector.FindCompletedHands(tiles, null, 14, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Dot_9>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Run<Dot_1, Dot_2, Dot_3>(),
                                                           new Run<Character_7, Character_8, Character_9>(),
                                                           new Triple<Character_1>(),
                                                           new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                           new Double<Dot_9>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 純全帯么九_ポン()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_2>(),
                Tile.CreateInstance<Dot_3>(),
                Tile.CreateInstance<Character_7>(),
                Tile.CreateInstance<Character_8>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_2>(),
                Tile.CreateInstance<Bamboo_3>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),

            });
            var melds = new Meld[]
            {
                new Triple<Character_1>(),
            };
            var completedHands = MeldDetector.FindCompletedHands(tiles, melds, 14, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Dot_9>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Run<Dot_1, Dot_2, Dot_3>(),
                                                           new Run<Character_7, Character_8, Character_9>(),
                                                           new Triple<Character_1>(),
                                                           new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                           new Double<Dot_9>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 純全帯么九_暗槓()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_2>(),
                Tile.CreateInstance<Dot_3>(),
                Tile.CreateInstance<Character_7>(),
                Tile.CreateInstance<Character_8>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_2>(),
                Tile.CreateInstance<Bamboo_3>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),

            });
            var melds = new Meld[]
            {
                new Quad<Character_1>(KongType.ConcealedKong),
            };
            var completedHands = MeldDetector.FindCompletedHands(tiles, melds, 15, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Dot_9>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Run<Dot_1, Dot_2, Dot_3>(),
                                                           new Run<Character_7, Character_8, Character_9>(),
                                                           new Quad<Character_1>(KongType.ConcealedKong),
                                                           new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                           new Double<Dot_9>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 純全帯么九_大明槓()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_2>(),
                Tile.CreateInstance<Dot_3>(),
                Tile.CreateInstance<Character_7>(),
                Tile.CreateInstance<Character_8>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_2>(),
                Tile.CreateInstance<Bamboo_3>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),

            });
            var melds = new Meld[]
            {
                new Quad<Character_1>(KongType.LargeMeldedKong),
            };
            var completedHands = MeldDetector.FindCompletedHands(tiles, melds, 15, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Dot_9>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Run<Dot_1, Dot_2, Dot_3>(),
                                                           new Run<Character_7, Character_8, Character_9>(),
                                                           new Quad<Character_1>(KongType.LargeMeldedKong),
                                                           new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                           new Double<Dot_9>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 純全帯么九_加槓()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_2>(),
                Tile.CreateInstance<Dot_3>(),
                Tile.CreateInstance<Character_7>(),
                Tile.CreateInstance<Character_8>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_2>(),
                Tile.CreateInstance<Bamboo_3>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),

            });
            var melds = new Meld[]
            {
                new Quad<Character_1>(KongType.SmallMeldedKong),
            };
            var completedHands = MeldDetector.FindCompletedHands(tiles, melds, 15, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Dot_9>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Run<Dot_1, Dot_2, Dot_3>(),
                                                           new Run<Character_7, Character_8, Character_9>(),
                                                           new Quad<Character_1>(KongType.SmallMeldedKong),
                                                           new Run<Bamboo_1, Bamboo_2, Bamboo_3>(),
                                                           new Double<Dot_9>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 混老頭_門前()
        {

            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Red>(),
                Tile.CreateInstance<Red>(),
                Tile.CreateInstance<Red>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<East>(),
                Tile.CreateInstance<East>(),
            });
            var completedHands = MeldDetector.FindCompletedHands(tiles, null, 14, ViewModels.AgariType.Tsumo, Tile.CreateInstance<East>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Triple<Dot_1>(),
                                                           new Triple<Character_9>(),
                                                           new Triple<Red>(),
                                                           new Triple<North>(),
                                                           new Double<East>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 混老頭_ポン()
        {

            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<East>(),
                Tile.CreateInstance<East>(),
            });
            var melds = new Meld[]
            {
                new Triple<Red>(),
            };
            var completedHands = MeldDetector.FindCompletedHands(tiles, melds, 14, ViewModels.AgariType.Tsumo, Tile.CreateInstance<East>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Triple<Dot_1>(),
                                                           new Triple<Character_9>(),
                                                           new Triple<Red>(),
                                                           new Triple<North>(),
                                                           new Double<East>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 混老頭_暗槓()
        {

            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<East>(),
                Tile.CreateInstance<East>(),
            });
            var melds = new Meld[]
            {
                new Quad<Red>(KongType.ConcealedKong),
            };
            var completedHands = MeldDetector.FindCompletedHands(tiles, melds, 15, ViewModels.AgariType.Tsumo, Tile.CreateInstance<East>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Triple<Dot_1>(),
                                                           new Triple<Character_9>(),
                                                           new Quad<Red>(KongType.ConcealedKong),
                                                           new Triple<North>(),
                                                           new Double<East>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 混老頭_大明槓()
        {

            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<East>(),
                Tile.CreateInstance<East>(),
            });
            var melds = new Meld[]
            {
                new Quad<Red>(KongType.LargeMeldedKong),
            };
            var completedHands = MeldDetector.FindCompletedHands(tiles, melds, 15, ViewModels.AgariType.Tsumo, Tile.CreateInstance<East>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Triple<Dot_1>(),
                                                           new Triple<Character_9>(),
                                                           new Quad<Red>(KongType.LargeMeldedKong),
                                                           new Triple<North>(),
                                                           new Double<East>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 混老頭_加槓()
        {

            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<North>(),
                Tile.CreateInstance<East>(),
                Tile.CreateInstance<East>(),
            });
            var melds = new Meld[]
            {
                new Quad<Red>(KongType.SmallMeldedKong),
            };
            var completedHands = MeldDetector.FindCompletedHands(tiles, melds, 15, ViewModels.AgariType.Tsumo, Tile.CreateInstance<East>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Triple<Dot_1>(),
                                                           new Triple<Character_9>(),
                                                           new Quad<Red>(KongType.SmallMeldedKong),
                                                           new Triple<North>(),
                                                           new Double<East>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 清老頭_門前()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
            });
            var completedHands = MeldDetector.FindCompletedHands(tiles, null, 14, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Character_9>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Triple<Dot_1>(),
                                                           new Triple<Dot_9>(),
                                                           new Triple<Bamboo_1>(),
                                                           new Triple<Character_1>(),
                                                           new Double<Character_9>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 清老頭_ポン()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
            });
            var melds = new Meld[]
            {
                new Triple<Bamboo_1>(),
            };
            var completedHands = MeldDetector.FindCompletedHands(tiles, melds, 14, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Character_9>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Triple<Dot_1>(),
                                                           new Triple<Dot_9>(),
                                                           new Triple<Bamboo_1>(),
                                                           new Triple<Character_1>(),
                                                           new Double<Character_9>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 清老頭_暗槓()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
            });
            var melds = new Meld[]
            {
                new Quad<Bamboo_1>(KongType.ConcealedKong),
            };
            var completedHands = MeldDetector.FindCompletedHands(tiles, melds, 15, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Character_9>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Triple<Dot_1>(),
                                                           new Triple<Dot_9>(),
                                                           new Quad<Bamboo_1>(KongType.ConcealedKong),
                                                           new Triple<Character_1>(),
                                                           new Double<Character_9>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 清老頭_大明槓()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
            });
            var melds = new Meld[]
            {
                new Quad<Bamboo_1>(KongType.LargeMeldedKong),
            };
            var completedHands = MeldDetector.FindCompletedHands(tiles, melds, 15, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Character_9>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Triple<Dot_1>(),
                                                           new Triple<Dot_9>(),
                                                           new Quad<Bamboo_1>(KongType.LargeMeldedKong),
                                                           new Triple<Character_1>(),
                                                           new Double<Character_9>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }

        [Test]
        public void 清老頭_加槓()
        {
            var tiles = new TileCollection(new Tile[]
            {
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_1>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Dot_9>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_1>(),
                Tile.CreateInstance<Character_9>(),
                Tile.CreateInstance<Character_9>(),
            });
            var melds = new Meld[]
            {
                new Quad<Bamboo_1>(KongType.SmallMeldedKong),
            };
            var completedHands = MeldDetector.FindCompletedHands(tiles, melds, 15, ViewModels.AgariType.Tsumo, Tile.CreateInstance<Character_9>(), ViewModels.WindOfTheRound.East, ViewModels.OnesOwnWind.East);
            Assert.That(completedHands, Has.Length.EqualTo(1));
            foreach (var completedHand in completedHands)
            {
                if (completedHand.Equals(new CompletedHand(new Triple<Dot_1>(),
                                                           new Triple<Dot_9>(),
                                                           new Quad<Bamboo_1>(KongType.SmallMeldedKong),
                                                           new Triple<Character_1>(),
                                                           new Double<Character_9>())))
                { }
                else
                {
                    Assert.Fail(completedHand.ToString());
                }
            }
        }
    }
}
