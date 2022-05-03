using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class Bamboo_3 : Bamboos, IPenchanWaitable
    {
        public override string Display
        {
            get { return "s3"; }
        }

        public override int GetHashCode()
        {
            return 2;
        }

        public override Tile[] Suji()
        {
            return new Tile[] { new Bamboo_6() };
        }

        public Tile[] Outward
        {
            get { return new Tile[] {new Bamboo_1(), new Bamboo_2()}; }
        }
    }
}
