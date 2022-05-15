using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models.Yaku.Meld
{
    public class CompletedHand : ReadyHand
    {
        public CompletedHand()
            : base()
        { }

        public CompletedHand(params Meld[] melds)
            : base(melds)
        { }

        public IncompletedMeld[] WaitForm { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is CompletedHand))
                return false;
            var other = obj as CompletedHand;
            foreach (var meld in Melds)
            {
                if (!other.Melds.Contains(meld))
                    return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
