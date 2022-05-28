using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Models
{
    /// <summary>
    /// 点数
    /// </summary>
    public class Score
    {
        /// <summary>
        /// 親か子か
        /// </summary>
        public ParentOrChild POrC { get; private set; }

        /// <summary>
        /// 符
        /// </summary>
        public int Hu { get; private set; }

        /// <summary>
        /// 翻
        /// </summary>
        public int Han { get; private set; }

        /// <summary>
        /// 点数
        /// </summary>
        public int Scr { get; private set; }

        public enum ParentOrChild
        {
            /// <summary>
            /// 親
            /// </summary>
            Parent,

            /// <summary>
            /// 子
            /// </summary>
            Child,
        }

        public Score(ParentOrChild pOrC, int hu, int han)
        {
            POrC = pOrC;
            Hu = hu;
            Han = han;
        }

        public Score(ParentOrChild pOrC, int hu, int han, int score)
        {
            POrC = pOrC;
            Hu = hu;
            Han = han;
            Scr = score;
        }

        public override bool Equals(object? obj)
        {
            if (!(obj is Score score))
                return false;
            return POrC == score.POrC && Hu == score.Hu && Han == score.Han;
        }

        public override int GetHashCode()
        {
            return POrC.GetHashCode() ^ Hu.GetHashCode() ^ Han.GetHashCode();
        }

        public static readonly Score[] Scores = new Score[]
        {
            //子
            new Score(ParentOrChild.Child, 30,  1, 1000),
            new Score(ParentOrChild.Child, 40,  1, 1300),
            new Score(ParentOrChild.Child, 50,  1, 1600),
            new Score(ParentOrChild.Child, 60,  1, 2000),
            new Score(ParentOrChild.Child, 70,  1, 2300),
            new Score(ParentOrChild.Child, 80,  1, 2600),
            new Score(ParentOrChild.Child, 90,  1, 2900),
            new Score(ParentOrChild.Child, 100, 1, 3200),
            new Score(ParentOrChild.Child, 110, 1, 3600),
            new Score(ParentOrChild.Child, 20,  2, 1300),
            new Score(ParentOrChild.Child, 25,  2, 1600),
            new Score(ParentOrChild.Child, 30,  2, 2000),
            new Score(ParentOrChild.Child, 40,  2, 2600),
            new Score(ParentOrChild.Child, 50,  2, 3200),
            new Score(ParentOrChild.Child, 60,  2, 3900),
            new Score(ParentOrChild.Child, 70,  2, 4500),
            new Score(ParentOrChild.Child, 80,  2, 5200),
            new Score(ParentOrChild.Child, 90,  2, 5800),
            new Score(ParentOrChild.Child, 100, 2, 6400),
            new Score(ParentOrChild.Child, 110, 2, 7100),
            new Score(ParentOrChild.Child, 20,  3, 2600),
            new Score(ParentOrChild.Child, 25,  3, 3200),
            new Score(ParentOrChild.Child, 30,  3, 3900),
            new Score(ParentOrChild.Child, 40,  3, 5200),
            new Score(ParentOrChild.Child, 50,  3, 6400),
            new Score(ParentOrChild.Child, 60,  3, 7700),
            new Slum(ParentOrChild.Child, 70,  3),
            new Slum(ParentOrChild.Child, 80,  3),
            new Slum(ParentOrChild.Child, 90,  3),
            new Slum(ParentOrChild.Child, 100, 3),
            new Slum(ParentOrChild.Child, 110, 3),
            new Score(ParentOrChild.Child, 20,  4, 5200),
            new Score(ParentOrChild.Child, 25,  4, 6400),
            new Score(ParentOrChild.Child, 30,  4, 7700),
            new Slum(ParentOrChild.Child, 40,  4),
            new Slum(ParentOrChild.Child, 50,  4),
            new Slum(ParentOrChild.Child, 60,  4),
            new Slum(ParentOrChild.Child, 70,  4),
            new Slum(ParentOrChild.Child, 80,  4),
            new Slum(ParentOrChild.Child, 90,  4),
            new Slum(ParentOrChild.Child, 100, 4),
            new Slum(ParentOrChild.Child, 110, 4),
            new Slum(ParentOrChild.Child, 20,  5),
            new Slum(ParentOrChild.Child, 25,  5),
            new Slum(ParentOrChild.Child, 30,  5),
            new Slum(ParentOrChild.Child, 40,  5),
            new Slum(ParentOrChild.Child, 50,  5),
            new Slum(ParentOrChild.Child, 60,  5),
            new Slum(ParentOrChild.Child, 70,  5),
            new Slum(ParentOrChild.Child, 80,  5),
            new Slum(ParentOrChild.Child, 90,  5),
            new Slum(ParentOrChild.Child, 100, 5),
            new Slum(ParentOrChild.Child, 110, 5),

            //親
            new Score(ParentOrChild.Parent, 30,  1, 1500),
            new Score(ParentOrChild.Parent, 40,  1, 2000),
            new Score(ParentOrChild.Parent, 50,  1, 2400),
            new Score(ParentOrChild.Parent, 60,  1, 2900),
            new Score(ParentOrChild.Parent, 70,  1, 3400),
            new Score(ParentOrChild.Parent, 80,  1, 3900),
            new Score(ParentOrChild.Parent, 90,  1, 4400),
            new Score(ParentOrChild.Parent, 100, 1, 4800),
            new Score(ParentOrChild.Parent, 110, 1, 5300),
            new Score(ParentOrChild.Parent, 20,  2, 2000),
            new Score(ParentOrChild.Parent, 25,  2, 2400),
            new Score(ParentOrChild.Parent, 30,  2, 2900),
            new Score(ParentOrChild.Parent, 40,  2, 3900),
            new Score(ParentOrChild.Parent, 50,  2, 4800),
            new Score(ParentOrChild.Parent, 60,  2, 5800),
            new Score(ParentOrChild.Parent, 70,  2, 6800),
            new Score(ParentOrChild.Parent, 80,  2, 7700),
            new Score(ParentOrChild.Parent, 90,  2, 8700),
            new Score(ParentOrChild.Parent, 100, 2, 9600),
            new Score(ParentOrChild.Parent, 110, 2, 10600),
            new Score(ParentOrChild.Parent, 20,  3, 3900),
            new Score(ParentOrChild.Parent, 25,  3, 4800),
            new Score(ParentOrChild.Parent, 30,  3, 5800),
            new Score(ParentOrChild.Parent, 40,  3, 7700),
            new Score(ParentOrChild.Parent, 50,  3, 9600),
            new Score(ParentOrChild.Parent, 60,  3, 11600),
            new Slum(ParentOrChild.Parent, 70,  3),
            new Slum(ParentOrChild.Parent, 80,  3),
            new Slum(ParentOrChild.Parent, 90,  3),
            new Slum(ParentOrChild.Parent, 100, 3),
            new Slum(ParentOrChild.Parent, 110, 3),
            new Score(ParentOrChild.Parent, 20,  4, 7700),
            new Score(ParentOrChild.Parent, 25,  4, 9600),
            new Score(ParentOrChild.Parent, 30,  4, 11600),
            new Slum(ParentOrChild.Parent, 40,  4),
            new Slum(ParentOrChild.Parent, 50,  4),
            new Slum(ParentOrChild.Parent, 60,  4),
            new Slum(ParentOrChild.Parent, 70,  4),
            new Slum(ParentOrChild.Parent, 80,  4),
            new Slum(ParentOrChild.Parent, 90,  4),
            new Slum(ParentOrChild.Parent, 100, 4),
            new Slum(ParentOrChild.Parent, 110, 4),
            new Slum(ParentOrChild.Parent, 20,  5),
            new Slum(ParentOrChild.Parent, 25,  5),
            new Slum(ParentOrChild.Parent, 30,  5),
            new Slum(ParentOrChild.Parent, 40,  5),
            new Slum(ParentOrChild.Parent, 50,  5),
            new Slum(ParentOrChild.Parent, 60,  5),
            new Slum(ParentOrChild.Parent, 70,  5),
            new Slum(ParentOrChild.Parent, 80,  5),
            new Slum(ParentOrChild.Parent, 90,  5),
            new Slum(ParentOrChild.Parent, 100, 5),
            new Slum(ParentOrChild.Parent, 110, 5),
        };
    }
}
