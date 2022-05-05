using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tenpai.Models.Tiles;

namespace Tenpai.Models
{
    public class Call
    {
        public Tile Target { get; set; }
        public EOpponent CallFrom { get; set; }

        public Call(Tile target, EOpponent callFrom)
        {
            Target = target;
            CallFrom = callFrom;
        }
    }
}
