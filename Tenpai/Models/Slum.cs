using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models
{
    /// <summary>
    /// 満貫
    /// </summary>
    public class Slum : Score
    {
        public Slum(ParentOrChild pOrC, int hu, int han) : base(pOrC, hu, han)
        {
        }
    }
}
