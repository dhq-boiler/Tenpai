using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Tenpai.Models.Tiles;

namespace Tenpai.Models.Yaku.Meld
{
    public static class WaitPatternDetector
    {
        /// <summary>
        /// 上家が捨てた牌からチーでき得る2枚の牌の組み合わせを返す．
        /// </summary>
        /// <param name="comingTile">直前の上家の捨て牌</param>
        /// <returns></returns>
        public static IncompletedMeld[] FindChiWaitingPattern(Tile comingTile)
        {
            List<IncompletedMeld> ret = new List<IncompletedMeld>();
            if (comingTile is Suits)
            {
                //両面待ち
                var suji_ar = (comingTile as Suits).Suji();
                var sort = suji_ar.OrderBy(a => a.Code);
                foreach (var suji in suji_ar)
                {
                    OpenWait ryanmen = new OpenWait();
                    if (comingTile.Code < suji.Code)
                    {
                        for (int i = comingTile.Code + 1; i < suji.Code; ++i)
                        {
                            ryanmen.Add(Tile.CreateInstance(i, Visibility.Visible, null));
                        }
                    }
                    else
                    {
                        for (int i = suji.Code + 1; i < comingTile.Code; ++i)
                        {
                            ryanmen.Add(Tile.CreateInstance(i, Visibility.Visible, null));
                        }
                    }
                    ret.Add(ryanmen);
                }

                //カンチャン待ち
                if (comingTile is ITerminals)
                { }
                else
                {
                    ClosedWait kanchan = new ClosedWait();
                    kanchan.Add(Tile.CreateInstance(comingTile.Code - 1, Visibility.Visible, null));
                    kanchan.Add(Tile.CreateInstance(comingTile.Code + 1, Visibility.Visible, null));
                    ret.Add(kanchan);
                }

                //ペンチャン待ち
                if (comingTile is IPenchanWaitable)
                {
                    IPenchanWaitable wait = comingTile as IPenchanWaitable;
                    EdgeWait penchan = new EdgeWait();
                    foreach (var t in wait.Outward)
                    {
                        penchan.Add(t);
                    }
                    ret.Add(penchan);
                }
            }

            return ret.ToArray();
        }

        /// <summary>
        /// 他家から捨てられた牌からポンでき得る2枚の牌の組み合わせを返す．
        /// </summary>
        /// <param name="comingTile">他家の捨て牌</param>
        /// <returns></returns>
        public static IncompletedMeld[] FindPonWaintingPattern(Tile comingTile)
        {
            List<IncompletedMeld> ret = new List<IncompletedMeld>();
            Double pair = new Double();
            for (int i = 0; i < 2; ++i)
            {
                pair.Tiles.Add(comingTile);
            }
            ret.Add(pair);
            return ret.ToArray();
        }
    }
}
