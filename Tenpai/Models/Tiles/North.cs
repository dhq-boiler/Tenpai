﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Tiles
{
    public class North : Winds
    {
        public override string FileName => "pe";

        public override string Display
        {
            get { return "北"; }
        }

        public override int GetHashCode()
        {
            return 30;
        }
    }
}
