using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku.Meld
{
    public abstract class WaitHand
    {
        public WaitHand()
        {
        }

        public WaitHand(params Meld[] melds)
            : this()
        {
            Melds.AddRange(melds.OrderBy(a => a.Tiles.Average(b => b.Code)));
        }

        public ReactiveCollection<Meld> Melds { get; } = new ReactiveCollection<Meld>();

        public override int GetHashCode()
        {
            int hashcode = 0;
            foreach (var meld in Melds)
            {
                hashcode ^= meld.GetHashCode();
            }
            return hashcode;
        }

        public override string ToString()
        {
            string ret = "";
            for (int i = 0; i < Melds.Count(); ++i)
            {
                ret += Melds[i].ToString();
                if (i + 1 < Melds.Count())
                    ret += ",";
            }
            return ret;
        }
    }
}
