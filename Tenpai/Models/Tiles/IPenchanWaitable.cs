using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    interface IPenchanWaitable
    {
        Tile[] Outward { get; }
    }
}
