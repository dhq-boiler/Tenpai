using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenpai.Models.Tiles;

namespace Tenpai.Test
{
    [TestFixture]
    public class TileCollectionTest
    {
        [Test]
        public void Enumerate_True()
        {
            var tileCollection = new TileCollection(new Tile[] {
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
                Tile.CreateInstance<Bamboo_9>()
            });
            var evaluationValue = tileCollection.Enumerate<Bamboo_1>(3)
                                                .Enumerate<Bamboo_2>(1)
                                                .Enumerate<Bamboo_3>(1)
                                                .Enumerate<Bamboo_4>(1)
                                                .Enumerate<Bamboo_5>(1)
                                                .Enumerate<Bamboo_6>(1)
                                                .Enumerate<Bamboo_7>(1)
                                                .Enumerate<Bamboo_8>(1)
                                                .Enumerate<Bamboo_9>(3)
                                                .Evaluate();
            Assert.That(evaluationValue, Is.True);
        }

        [Test]
        public void Enumerate_False()
        {
            var tileCollection = new TileCollection(new Tile[] {
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
                Tile.CreateInstance<Bamboo_9>()
            });
            var evaluationValue = tileCollection.Enumerate<Bamboo_1>(4)
                                                .Enumerate<Bamboo_2>(1)
                                                .Enumerate<Bamboo_3>(1)
                                                .Enumerate<Bamboo_4>(1)
                                                .Enumerate<Bamboo_5>(1)
                                                .Enumerate<Bamboo_6>(1)
                                                .Enumerate<Bamboo_7>(1)
                                                .Enumerate<Bamboo_8>(1)
                                                .Enumerate<Bamboo_9>(3)
                                                .Evaluate();
            Assert.That(evaluationValue, Is.False);
        }

        [Test]
        public void ReduceAndAdd_True()
        {
            var tileCollection = new TileCollection(new Tile[] {
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_2>(),
                Tile.CreateInstance<Bamboo_2>(),
                Tile.CreateInstance<Bamboo_3>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_5>(),
                Tile.CreateInstance<Bamboo_6>(),
                Tile.CreateInstance<Bamboo_7>(),
                Tile.CreateInstance<Bamboo_8>(),
                Tile.CreateInstance<Bamboo_9>(),
                Tile.CreateInstance<Bamboo_9>(),
                Tile.CreateInstance<Bamboo_9>()
            });
            var bambooArr = new Tile[] {
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_2>(),
                Tile.CreateInstance<Bamboo_3>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_5>(),
                Tile.CreateInstance<Bamboo_6>(),
                Tile.CreateInstance<Bamboo_7>(),
                Tile.CreateInstance<Bamboo_8>(),
                Tile.CreateInstance<Bamboo_9>()
            };
            var evaluationValue = bambooArr.Any(x => bambooArr.Any(y =>
                                                            tileCollection.Add(x, 1)
                                                                          .Reduce(y, 1)
                                                                          .Enumerate<Bamboo_1>(3)
                                                                          .Enumerate<Bamboo_2>(1)
                                                                          .Enumerate<Bamboo_3>(1)
                                                                          .Enumerate<Bamboo_4>(1)
                                                                          .Enumerate<Bamboo_5>(1)
                                                                          .Enumerate<Bamboo_6>(1)
                                                                          .Enumerate<Bamboo_7>(1)
                                                                          .Enumerate<Bamboo_8>(1)
                                                                          .Enumerate<Bamboo_9>(3)
                                                                          .Evaluate()));
            Assert.That(evaluationValue, Is.True);
        }

        [Test]
        public void 九蓮宝燈()
        {
            var tileCollection = new TileCollection(new Tile[] {
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_2>(),
                Tile.CreateInstance<Bamboo_2>(),
                Tile.CreateInstance<Bamboo_3>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_5>(),
                Tile.CreateInstance<Bamboo_6>(),
                Tile.CreateInstance<Bamboo_7>(),
                Tile.CreateInstance<Bamboo_8>(),
                Tile.CreateInstance<Bamboo_9>(),
                Tile.CreateInstance<Bamboo_9>(),
                Tile.CreateInstance<Bamboo_9>()
            });
            var bambooArr = new Tile[] {
                Tile.CreateInstance<Bamboo_1>(),
                Tile.CreateInstance<Bamboo_2>(),
                Tile.CreateInstance<Bamboo_3>(),
                Tile.CreateInstance<Bamboo_4>(),
                Tile.CreateInstance<Bamboo_5>(),
                Tile.CreateInstance<Bamboo_6>(),
                Tile.CreateInstance<Bamboo_7>(),
                Tile.CreateInstance<Bamboo_8>(),
                Tile.CreateInstance<Bamboo_9>()
            };
            var waits = new Tile[]
            {
                Tile.CreateInstance<Bamboo_2>(),
            };
            var evaluationValue = bambooArr.Any(y => waits.Any(x => 
                                                            tileCollection.Add(x, 1)
                                                                          .Reduce(y, 1)
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
                                                                          .Evaluate()));
            Assert.That(evaluationValue, Is.True);
        }
    }
}
