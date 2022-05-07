using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenpai.Models.Tiles;
using Tenpai.Yaku.Meld;

namespace Tenpai.Models
{
    public class Call
    {
        public Tile Target { get; set; }
        public EOpponent CallFrom { get; set; }
        public Meld Meld { get; set; }


        public Call(Tile target, EOpponent callFrom)
        {
            Target = target;
            CallFrom = callFrom;
        }

        public Call(Tile target, EOpponent callFrom, Meld meld)
            : this(target, callFrom)
        {
            this.Meld = meld;
        }

        public Call(Tile target, Meld meld)
        {
            Target = target;
            this.Meld = meld;
        }

        public Call(Meld meld)
        {
            this.Meld = meld;
        }
    }
}
