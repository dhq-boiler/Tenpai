using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku.Meld
{
    public abstract class WaitHand
    {
        private List<Meld> _Melds;

        public WaitHand()
        {
            _Melds = new List<Meld>();
        }

        public WaitHand(params Meld[] melds)
            : this()
        {
            _Melds = new List<Meld>(melds.OrderBy(a => a.Tiles.Average(b => b.Code)));
        }

        public Meld[] Melds
        {
            get { return _Melds.ToArray(); }
        }

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
