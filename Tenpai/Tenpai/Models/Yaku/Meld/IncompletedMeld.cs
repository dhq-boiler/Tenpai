using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tenpai.Models.Tiles;
using Tenpai.Tiles;

namespace Tenpai.Yaku.Meld
{
    public abstract class IncompletedMeld : Meld
    {
        protected TileCollection _Waiting;
        public readonly MeldStatus MeldStatusType;

        public enum MeldStatus
        {
            UNKNOWN,
            OPEN,
            COMPLETED,
            WAIT
        }

        public IncompletedMeld()
            : base()
        {
            MeldStatusType = MeldStatus.WAIT;
            _Waiting = new TileCollection();
        }

        public IncompletedMeld(MeldStatus status)
            : base()
        {
            MeldStatusType = status;
            _Waiting = new TileCollection();
        }

        //public IncompletedMeld(Tile have1, Tile have2, Tile have3, EOpponent from)
        //    : base(have1, have2, have3, from)
        //{
        //    MeldStatusType = MeldStatus.OPEN;
        //    _Waiting = new TileCollection();
        //}

        public TileCollection WaitTiles
        {
            get
            {
                _Waiting.Clear();
                ComputeWaitTiles();
                return _Waiting;
            }
        }

        public virtual void ComputeWaitTiles()
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            int hash = 0;
            foreach (var waittile in WaitTiles)
            {
                hash ^= waittile.GetHashCode();
            }
            return hash;
        }

        public override string ToString()
        {
            switch (MeldStatusType)
            {
                case MeldStatus.OPEN:
                    return base.ToString();
                case MeldStatus.COMPLETED:
                    return ToStringCompleted();
                case MeldStatus.WAIT:
                    return ToStringIncompleted();
                default:
                    {
                        StackFrame sf = new StackFrame(1, true);
                        return string.Format("Method:{0}\nFilename:{1}\nLineNumber:{2}", sf.GetMethod().ToString(), sf.GetFileName(), sf.GetFileLineNumber());
                    }
            }
        }

        private string ToStringCompleted()
        {
            string ret = "";
            foreach (var tile in Tiles)
            {
                ret += tile.ToString();
            }
            return ret;
        }

        public string ToStringIncompleted()
        {
            string str = "";
            Queue<Tile> set = new Queue<Tile>(Tiles);
            Queue<Tile> wait = new Queue<Tile>(WaitTiles);

            while (set.Count() + wait.Count() > 0)
            {
                Tile x = null, y = null;
                try
                {
                    x = set.Peek();
                    y = wait.Peek();

                    if (x.Code < y.Code)
                    {
                        x = set.Dequeue();
                        str += x.ToString();
                    }
                    else
                    {
                        y = wait.Dequeue();
                        str += "(" + y.ToString() + ")";
                    }
                }
                catch (InvalidOperationException)
                {
                    if (x != null)
                    {
                        x = set.Dequeue();
                        str += x.ToString();
                    }
                    else
                    {
                        y = wait.Dequeue();
                        str += "(" + y.ToString() + ")";
                    }
                }
            }

            return str;
        }
    }
}
