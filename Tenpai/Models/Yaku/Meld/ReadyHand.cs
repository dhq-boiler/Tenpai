using Reactive.Bindings;
using System.Collections.Generic;
using System.Linq;
using Tenpai.Extensions;
using Tenpai.Models.Tiles;
using Tenpai.Models.Yaku.Meld.Detector;

namespace Tenpai.Models.Yaku.Meld
{
    public class ReadyHand : WaitHand
    {
        public ReadyHand()
            : base()
        { }

        public ReadyHand(params Meld[] melds)
            : base(melds)
        { }

        public ReadyHand(Tile[] odd, params Meld[] melds)
            : base(melds)
        {
            Odd = odd;
        }

        /// <summary>
        /// 合計翻数
        /// </summary>
        public int SumHanCount
        {
            get
            {
                return Yakus.Sum(x => x.HanCount(Melds.Where(x => x.CallFrom != EOpponent.Unknown && (x is Run || x is Triple || (x is Quad quad && quad.Type != KongType.ConcealedKong))).Count() > 0));
            }
        }

        /// <summary>
        /// 役のコレクション
        /// </summary>
        public ReactiveCollection<Yaku> Yakus { get; set; } = new ReactiveCollection<Yaku>();

        /// <summary>
        /// 符の合計
        /// </summary>
        public ReactivePropertySlim<int> HuSum { get; } = new ReactivePropertySlim<int>();

        public Meld[] Waiting
        {
            get { return Melds.Where(a => 
                a is IncompletedMeld 
                && (a as IncompletedMeld).MeldStatusType == IncompletedMeld.MeldStatus.WAIT).ToArray(); }
        }

        public virtual Tile[] WaitingTiles
        {
            get
            {
                HashSet<Tile> wait = new HashSet<Tile>();
                foreach (var meld in Waiting.Cast<IncompletedMeld>())
                {
                    foreach (var waitTile in meld.ComputeWaitTiles(this.Melds.ToArray()))
                    {
                        wait.Add(waitTile);
                    }
                }
                return wait.ToArray();
            }
            set
            {
            }
        }

        private IEnumerable<Meld[]> MakeRoundRobinCombinationByThirteenOrphans(Meld[] @base)
        {
            var thirteenOrphansTiles = MeldDetector.ThirteenOrphansTiles();
            foreach (var thirteenOrphansTile in thirteenOrphansTiles)
            {
                var melds = new List<Meld>();
                foreach (var meld in @base)
                {
                    if (meld.Tiles.IsContained(new TileCollection(thirteenOrphansTile)))
                    {
                        if (meld is Quad)
                        {
                            melds.Add(meld);
                            continue;
                        }
                        melds.Add(meld + thirteenOrphansTile);
                    }
                    else
                    {
                        //melds.Add(meld);
                    }
                }
                yield return melds.ToArray();
            }
        }

        public CompletedHand[] ComplementAndGetCompletedHand()
        {
            var hands = new List<CompletedHand>();
            var list = new List<Meld>();
            list.AddRange(Melds);
            //国士無双13面待ちの完成形を作ろうとしている場合はlistから待ちを除外しない
            if (MeldDetector.ThirteenOrphansTiles().IsAllContained(list.ToArray()))
            { }
            //国士無双単騎待ちの完成形を作ろうとしている場合はlistから待ちを除外しない
            else if (MakeRoundRobinCombinationByThirteenOrphans(list.ToArray()).Any(x => new TileCollection(x.SelectMany(y => y.Tiles)).IsAllContained(Melds.ToArray())))
            { }
            else
            {
                foreach (var wait in Waiting)
                {
                    list.Remove(wait);
                }
            }
            foreach (var wait in Waiting)
            {
                (wait as IncompletedMeld).ComputeWaitTiles();
                foreach (var wtile in WaitingTiles)
                {
                    if (wait.WaitTiles.ContainsRedSuitedTileIncluding(wtile))
                    {
                        var w = new TileCollection(Waiting.SelectMany(x => x.Tiles));
                        var a = wait is Single;
                        var b = w.Equals(MeldDetector.ThirteenOrphansTiles());
                        if (a && b)
                        {
                            var l = list.ToList();
                            l.Remove(wait);
                            var ret = new CompletedHand(wait + wtile, l[0], l[1], l[2], l[3], l[4], l[5], l[6], l[7], l[8], l[9], l[10], l[11]);
                            ret.WaitForm = new Meld[] { wait };
                            ret.AgariTile = wtile;
                            hands.Add(ret);
                        }
                        else if (MakeRoundRobinCombinationByThirteenOrphans(list.ToArray()).Any(x => new TileCollection(Melds.SelectMany(x => x.Tiles).ToArray()).IsAllContained(x)) && list.Count() == 13)
                        {
                            var l = list.ToList();
                            l.Remove(wait);
                            var ret = new CompletedHand(wait + wtile, l[0], l[1], l[2], l[3], l[4], l[5], l[6], l[7], l[8], l[9], l[10], l[11]);
                            ret.WaitForm = new Meld[] { wait };
                            ret.AgariTile = wtile;
                            hands.Add(ret);
                        }
                        else if (list.Count(x => x is Double) == 6)
                        {
                            var ret = new CompletedHand(wait + wtile, list[0], list[1], list[2], list[3], list[4], list[5]);
                            ret.WaitForm = new Meld[] { wait };
                            ret.AgariTile = wtile;
                            hands.Add(ret);
                        }
                        else
                        {
                            var ret = new CompletedHand(wait + wtile, list[0], list[1], list[2], list[3]);
                            ret.WaitForm = new Meld[] { wait };
                            ret.AgariTile = wtile;
                            hands.Add(ret);
                        }
                    }
                }
            }
            return hands.ToArray();
        }

        public Tile[] Odd { get; private set; }

        public override bool Equals(object obj)
        {
            if (!(obj is ReadyHand))
                return false;
            var other = obj as ReadyHand;
            foreach (var meld in Melds)
            {
                if (!other.Melds.Contains(meld))
                    return false;
            }
            if (!WaitingTiles.SequenceEqualDoNotConsiderRotationAndOrder(other.WaitingTiles))
                return false;
            return true;
        }
        public override int GetHashCode()
        {
            int hash = 0;
            foreach (var meld in Melds)
            {
                hash ^= meld.GetHashCode();
            }
            foreach (var wait in WaitingTiles)
            {
                if (wait is IRedSuitedTile r)
                {
                    hash ^= wait.GetHashCode() ^ (r.IsRedSuited ? 1 : 0);
                }
                else
                {
                    hash ^= wait.GetHashCode();
                }
            }
            return hash;
        }

        public override string ToString()
        {
            string str = base.ToString() + " 待ち：";
            foreach (var wait in WaitingTiles)
            {
                str += wait;
            }
            if (Odd != null)
            {
                str += " 余り：";
                foreach (var odd in Odd)
                {
                    str += odd;
                }
            }
            return str;
        }
    }
}
