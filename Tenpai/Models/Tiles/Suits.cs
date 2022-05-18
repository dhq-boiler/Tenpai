using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public abstract class Suits : Tile
    {
        public abstract Tile[] Suji();
    }
}
