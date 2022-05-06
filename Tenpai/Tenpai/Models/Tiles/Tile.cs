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

        public override string ToString()
        {
            return Display;
        }

        public abstract bool IsSameType(Tile other);

        public static Tile CreateInstance(int hashCode)
        {
            switch (hashCode)
            {
                case 0:
                    return CreateInstance<Character_1>();
                case 1:
                    return CreateInstance<Character_2>();
                case 2:
                    return CreateInstance<Character_3>();
                case 3:
                    return CreateInstance<Character_4>();
                case 4:
                    return CreateInstance<Character_5>();
                case 5:
                    return CreateInstance<Character_6>();
                case 6:
                    return CreateInstance<Character_7>();
                case 7:
                    return CreateInstance<Character_8>();
                case 8:
                    return CreateInstance<Character_9>();
                case 9:
                    return CreateInstance<Dot_1>();
                case 10:
                    return CreateInstance<Dot_2>();
                case 11:
                    return CreateInstance<Dot_3>();
                case 12:
                    return CreateInstance<Dot_4>();
                case 13:
                    return CreateInstance<Dot_5>();
                case 14:
                    return CreateInstance<Dot_6>();
                case 15:
                    return CreateInstance<Dot_7>();
                case 16:
                    return CreateInstance<Dot_8>();
                case 17:
                    return CreateInstance<Dot_9>();
                case 18:
                    return CreateInstance<Bamboo_1>();
                case 19:
                    return CreateInstance<Bamboo_2>();
                case 20:
                    return CreateInstance<Bamboo_3>();
                case 21:
                    return CreateInstance<Bamboo_4>();
                case 22:
                    return CreateInstance<Bamboo_5>();
                case 23:
                    return CreateInstance<Bamboo_6>();
                case 24:
                    return CreateInstance<Bamboo_7>();
                case 25:
                    return CreateInstance<Bamboo_8>();
                case 26:
                    return CreateInstance<Bamboo_9>();
                case 27:
                    return CreateInstance<East>();
                case 28:
                    return CreateInstance<South>();
                case 29:
                    return CreateInstance<West>();
                case 30:
                    return CreateInstance<North>();
                case 31:
                    return CreateInstance<White>();
                case 32:
                    return CreateInstance<Green>();
                case 33:
                    return CreateInstance<Red>();
                case int.MaxValue:
                    return CreateInstance<Dummy>();
                default:
                    throw new NotSupportedException("No corresponding Tile");
            }
        }

        public static Tile CreateInstance(string code)
        {
            switch (code)
            {
                case "s1":
                    return CreateInstance<Bamboo_1>();
                case "s2":
                    return CreateInstance<Bamboo_2>();
                case "s3":
                    return CreateInstance<Bamboo_3>();
                case "s4":
                    return CreateInstance<Bamboo_4>();
                case "s5":
                    return CreateInstance<Bamboo_5>();
                case "s5r":
                    return CreateRedInstance<Bamboo_5>();
                case "s6":
                    return CreateInstance<Bamboo_6>();
                case "s7":
                    return CreateInstance<Bamboo_7>();
                case "s8":
                    return CreateInstance<Bamboo_8>();
                case "s9":
                    return CreateInstance<Bamboo_9>();
                case "m1":
                    return CreateInstance<Character_1>();
                case "m2":
                    return CreateInstance<Character_2>();
                case "m3":
                    return CreateInstance<Character_3>();
                case "m4":
                    return CreateInstance<Character_4>();
                case "m5":
                    return CreateInstance<Character_5>();
                case "m5r":
                    return CreateRedInstance<Character_5>();
                case "m6":
                    return CreateInstance<Character_6>();
                case "m7":
                    return CreateInstance<Character_7>();
                case "m8":
                    return CreateInstance<Character_8>();
                case "m9":
                    return CreateInstance<Character_9>();
                case "p1":
                    return CreateInstance<Dot_1>();
                case "p2":
                    return CreateInstance<Dot_2>();
                case "p3":
                    return CreateInstance<Dot_3>();
                case "p4":
                    return CreateInstance<Dot_4>();
                case "p5":
                    return CreateInstance<Dot_5>();
                case "p5r":
                    return CreateRedInstance<Dot_5>();
                case "p6":
                    return CreateInstance<Dot_6>();
                case "p7":
                    return CreateInstance<Dot_7>();
                case "p8":
                    return CreateInstance<Dot_8>();
                case "p9":
                    return CreateInstance<Dot_9>();
                case "東":
                    return CreateInstance<East>();
                case "南":
                    return CreateInstance<South>();
                case "西":
                    return CreateInstance<West>();
                case "北":
                    return CreateInstance<North>();
                case "白":
                    return CreateInstance<White>();
                case "發":
                    return CreateInstance<Green>();
                case "中":
                    return CreateInstance<Red>();
                case "":
                    return CreateInstance<Dummy>();
                default:
                    throw new NotSupportedException("No corresponding Tile");
            }
        }

        public static Tile CreateInstance<T>() where T : Tile, new()
        {
            return new T();
        }

        public static Tile CreateRedInstance<T>() where T : Tile, IRedSuitedTile, new()
        {
            var tile = new T();
            tile.IsRedSuited = true;
            tile.Lock();
            return tile;
        }

        public static Tile CreateRedInstance(int hashCode)
        {
            switch (hashCode)
            {
                case 4:
                    return CreateRedInstance<Character_5>();
                case 13:
                    return CreateRedInstance<Dot_5>();
                case 22:
                    return CreateRedInstance<Bamboo_5>();
                default:
                    throw new NotSupportedException("No corresponding Tile");
            }
        }

        public static Tile CreateInstance(Tile clone)
        {
            return CreateInstance(clone.Code);
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
                    if (t.IsRedSuited && o.IsRedSuited)
                    {
                        return 0;
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
                return _Guid.CompareTo(other._Guid);
            }
        }

        public bool EqualsRedSuitedTileIncluding(object obj)
        {
            if (!(obj is Tile)) return false;
            return Code == (obj as Tile).Code;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Tile)) return false;
            return Code == (obj as Tile).Code && ((this is IRedSuitedTile rthis && obj is IRedSuitedTile r && rthis.IsRedSuited == r.IsRedSuited) || !(this is IRedSuitedTile && obj is IRedSuitedTile));
        }

        public object Clone()
        {
            if (this is IRedSuitedTile r && r.IsRedSuited)
                return CreateRedInstance(Code);
            else
                return CreateInstance(Code);
        }
    }
}
