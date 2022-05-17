using Reactive.Bindings;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;
using Tenpai.Converters;

#pragma warning disable 659
namespace Tenpai.Models.Tiles
{
    [TypeConverter(typeof(TileTypeConverter))]
    public abstract class Tile : IComparable<Tile>, ICloneable
    {
        private int _Rand;

        internal Guid _Guid = Guid.NewGuid();

        public bool IsLocked { get; private set; }

        public int Order { get; set; }

        public void Lock()
        {
            IsLocked = true;
        }

        public int Rand
        {
            get { return _Rand; }
            set
            {
                if (IsLocked) return;
                _Rand = value;
            }
        }

        public EOpponent CallFrom { get; set; }

        public RotateTransform Rotate { get; set; }

        public ReactivePropertySlim<Visibility> Visibility { get; } = new ReactivePropertySlim<Visibility>();

        public abstract string Display { get; }

        public int Code { get { return GetHashCode(); } }

        public string Description
        {
            get
            {
                return $"({Display}, {Visibility}, {(Rotate != null ? "Rotate(90)" : "null")}, {Order})";
            }
        }

        public override string ToString()
        {
            return Display;
        }

        public abstract bool IsSameType(Tile other);

        public static Tile CreateInstance(int hashCode, Visibility visibility, RotateTransform rotate, int order = 0)
        {
            switch (hashCode)
            {
                case 0:
                    return CreateInstance<Character_1>(visibility, rotate, order);
                case 1:
                    return CreateInstance<Character_2>(visibility, rotate, order);
                case 2:
                    return CreateInstance<Character_3>(visibility, rotate, order);
                case 3:
                    return CreateInstance<Character_4>(visibility, rotate, order);
                case 4:
                    return CreateInstance<Character_5>(visibility, rotate, order);
                case 5:
                    return CreateInstance<Character_6>(visibility, rotate, order);
                case 6:
                    return CreateInstance<Character_7>(visibility, rotate, order);
                case 7:
                    return CreateInstance<Character_8>(visibility, rotate, order);
                case 8:
                    return CreateInstance<Character_9>(visibility, rotate, order);
                case 9:
                    return CreateInstance<Dot_1>(visibility, rotate, order);
                case 10:
                    return CreateInstance<Dot_2>(visibility, rotate, order);
                case 11:
                    return CreateInstance<Dot_3>(visibility, rotate, order);
                case 12:
                    return CreateInstance<Dot_4>(visibility, rotate, order);
                case 13:
                    return CreateInstance<Dot_5>(visibility, rotate, order);
                case 14:
                    return CreateInstance<Dot_6>(visibility, rotate, order);
                case 15:
                    return CreateInstance<Dot_7>(visibility, rotate, order);
                case 16:
                    return CreateInstance<Dot_8>(visibility, rotate, order);
                case 17:
                    return CreateInstance<Dot_9>(visibility, rotate, order);
                case 18:
                    return CreateInstance<Bamboo_1>(visibility, rotate, order);
                case 19:
                    return CreateInstance<Bamboo_2>(visibility, rotate, order);
                case 20:
                    return CreateInstance<Bamboo_3>(visibility, rotate, order);
                case 21:
                    return CreateInstance<Bamboo_4>(visibility, rotate, order);
                case 22:
                    return CreateInstance<Bamboo_5>(visibility, rotate, order);
                case 23:
                    return CreateInstance<Bamboo_6>(visibility, rotate, order);
                case 24:
                    return CreateInstance<Bamboo_7>(visibility, rotate, order);
                case 25:
                    return CreateInstance<Bamboo_8>(visibility, rotate, order);
                case 26:
                    return CreateInstance<Bamboo_9>(visibility, rotate, order);
                case 27:
                    return CreateInstance<East>(visibility, rotate, order);
                case 28:
                    return CreateInstance<South>(visibility, rotate, order);
                case 29:
                    return CreateInstance<West>(visibility, rotate, order);
                case 30:
                    return CreateInstance<North>(visibility, rotate, order);
                case 31:
                    return CreateInstance<White>(visibility, rotate, order);
                case 32:
                    return CreateInstance<Green>(visibility, rotate, order);
                case 33:
                    return CreateInstance<Red>(visibility, rotate, order);
                case int.MaxValue:
                    return CreateInstance<Dummy>(visibility, rotate, order);
                default:
                    throw new NotSupportedException("No corresponding Tile");
            }
        }

        public static Tile CreateInstance(string code, Visibility visibility, RotateTransform rotate, int order = 0)
        {
            switch (code)
            {
                case "s1":
                    return CreateInstance<Bamboo_1>(visibility, rotate, order);
                case "s2":
                    return CreateInstance<Bamboo_2>(visibility, rotate, order);
                case "s3":
                    return CreateInstance<Bamboo_3>(visibility, rotate, order);
                case "s4":
                    return CreateInstance<Bamboo_4>(visibility, rotate, order);
                case "s5":
                    return CreateInstance<Bamboo_5>(visibility, rotate, order);
                case "s5r":
                    return CreateRedInstance<Bamboo_5>(visibility, rotate, order);
                case "s6":
                    return CreateInstance<Bamboo_6>(visibility, rotate, order);
                case "s7":
                    return CreateInstance<Bamboo_7>(visibility, rotate, order);
                case "s8":
                    return CreateInstance<Bamboo_8>(visibility, rotate, order);
                case "s9":
                    return CreateInstance<Bamboo_9>(visibility, rotate, order);
                case "m1":
                    return CreateInstance<Character_1>(visibility, rotate, order);
                case "m2":
                    return CreateInstance<Character_2>(visibility, rotate, order);
                case "m3":
                    return CreateInstance<Character_3>(visibility, rotate, order);
                case "m4":
                    return CreateInstance<Character_4>(visibility, rotate, order);
                case "m5":
                    return CreateInstance<Character_5>(visibility, rotate, order);
                case "m5r":
                    return CreateRedInstance<Character_5>(visibility, rotate, order);
                case "m6":
                    return CreateInstance<Character_6>(visibility, rotate, order);
                case "m7":
                    return CreateInstance<Character_7>(visibility, rotate, order);
                case "m8":
                    return CreateInstance<Character_8>(visibility, rotate, order);
                case "m9":
                    return CreateInstance<Character_9>(visibility, rotate, order);
                case "p1":
                    return CreateInstance<Dot_1>(visibility, rotate, order);
                case "p2":
                    return CreateInstance<Dot_2>(visibility, rotate, order);
                case "p3":
                    return CreateInstance<Dot_3>(visibility, rotate, order);
                case "p4":
                    return CreateInstance<Dot_4>(visibility, rotate, order);
                case "p5":
                    return CreateInstance<Dot_5>(visibility, rotate, order);
                case "p5r":
                    return CreateRedInstance<Dot_5>(visibility, rotate, order);
                case "p6":
                    return CreateInstance<Dot_6>(visibility, rotate, order);
                case "p7":
                    return CreateInstance<Dot_7>(visibility, rotate, order);
                case "p8":
                    return CreateInstance<Dot_8>(visibility, rotate, order);
                case "p9":
                    return CreateInstance<Dot_9>(visibility, rotate, order);
                case "東":
                    return CreateInstance<East>(visibility, rotate, order);
                case "南":
                    return CreateInstance<South>(visibility, rotate, order);
                case "西":
                    return CreateInstance<West>(visibility, rotate, order);
                case "北":
                    return CreateInstance<North>(visibility, rotate, order);
                case "白":
                    return CreateInstance<White>(visibility, rotate, order);
                case "發":
                    return CreateInstance<Green>(visibility, rotate, order);
                case "中":
                    return CreateInstance<Red>(visibility, rotate, order);
                case "":
                    return CreateInstance<Dummy>(visibility, rotate, order);
                default:
                    throw new NotSupportedException("No corresponding Tile");
            }
        }

        public static Tile CreateInstance<T>() where T : Tile, new()
        {
            var tile = new T();
            tile.CallFrom = EOpponent.Default;
            return tile;
        }

        public static Tile CreateInstance<T>(Visibility visibility, RotateTransform rotate, int order = 0) where T : Tile, new()
        {
            var t = new T();
            t.Visibility.Value = visibility;
            t.Rotate = rotate;
            t.CallFrom = EOpponent.Default;
            t.Order = order;
            return t;
        }

        public static Tile CreateRedInstance<T>() where T : Tile, IRedSuitedTile, new()
        {
            var tile = new T();
            tile.IsRedSuited = true;
            tile.Lock();
            tile.CallFrom = EOpponent.Default;
            return tile;
        }

        public static Tile CreateRedInstance<T>(Visibility visibility, RotateTransform rotate, int order = 0) where T : Tile, IRedSuitedTile, new()
        {
            var tile = new T();
            tile.IsRedSuited = true;
            tile.Lock();
            tile.Visibility.Value = visibility;
            tile.Rotate = rotate;
            tile.CallFrom = EOpponent.Default;
            tile.Order = order;
            return tile;
        }

        public static Tile CreateRedInstance(int hashCode, Visibility visibility, RotateTransform rotate, int order = 0)
        {
            switch (hashCode)
            {
                case 4:
                    return CreateRedInstance<Character_5>(visibility, rotate, order);
                case 13:
                    return CreateRedInstance<Dot_5>(visibility, rotate, order);
                case 22:
                    return CreateRedInstance<Bamboo_5>(visibility, rotate, order);
                default:
                    throw new NotSupportedException("No corresponding Tile");
            }
        }

        public static Tile CreateInstance(Tile clone)
        {
            return CreateInstance(clone.Code, clone.Visibility.Value, clone.Rotate);
        }

        public int CompareTo(Tile other)
        {
            if (this is IRedSuitedTile t && other is IRedSuitedTile o)
            {
                if (this.Code > other.Code)
                {
                    return 1;
                }
                else if (this.Code < other.Code)
                {
                    return -1;
                }
                else
                {
                    if (this.Order.CompareTo(other.Order) != 0)
                        return this.Order.CompareTo(other.Order);
                    else if (t.IsRedSuited && o.IsRedSuited)
                    {
                        if (this.Rotate?.Value == null && other.Rotate?.Value == null)
                            return this.Visibility.Value.CompareTo(other.Visibility.Value);
                        else if (this.Rotate?.Value != null && other.Rotate?.Value == null)
                            return 1;
                        else if (this.Rotate?.Value == null && other.Rotate?.Value != null)
                            return -1;
                        else
                            return this.Visibility.Value.CompareTo(other.Visibility.Value);
                    }
                    else if (t.IsRedSuited)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            if (Code > other.Code) return 1;
            else if (Code < other.Code) return -1;
            else
            {
                if (this.Order.CompareTo(other.Order) != 0)
                    return this.Order.CompareTo(other.Order);
                else if (this.Rotate?.Value == null && other.Rotate?.Value == null)
                    return this.Visibility.Value.CompareTo(other.Visibility.Value);
                else if (this.Rotate?.Value != null && other.Rotate?.Value == null)
                    return 1;
                else if (this.Rotate?.Value == null && other.Rotate?.Value != null)
                    return -1;
                else
                    return this.Visibility.Value.CompareTo(other.Visibility.Value);
                //return _Guid.CompareTo(other._Guid);
            }
        }

        public bool EqualsRedSuitedTileIncluding(object obj)
        {
            if (!(obj is Tile)) return false;
            return Code == (obj as Tile).Code;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Tile t)) return false;
            var a = Code.Equals(t.Code);
            var b = Order.Equals(t.Order);
            var c = ((Rotate == null && t.Rotate == null) || (Rotate != null && t.Rotate != null && Rotate.Angle.Equals(t.Rotate.Angle)));
            var d = ((this is IRedSuitedTile rthis && obj is IRedSuitedTile r && rthis.IsRedSuited == r.IsRedSuited) || !(this is IRedSuitedTile && obj is IRedSuitedTile));
            return a && b && c && d;
        }

        public object Clone()
        {
            if (this is IRedSuitedTile r && r.IsRedSuited)
            {
                var ret = CreateRedInstance(Code, Visibility.Value, Rotate, Order);
                ret.CallFrom = CallFrom;
                return ret;
            }
            else
            {
                var ret = CreateInstance(Code, Visibility.Value, Rotate, Order);
                ret.CallFrom = CallFrom;
                return ret;
            }
        }
    }
}
