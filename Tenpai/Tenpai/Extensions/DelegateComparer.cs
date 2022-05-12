using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tenpai.Extensions
{
    /// <summary>
    /// https://threeshark3.com/linq-distinct/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class DelegateComparer<T, TKey> : IEqualityComparer<T>
    {
        private readonly Func<T, TKey> selector;
        public DelegateComparer(Func<T, TKey> keySelector)
        {
            // キーを指定する関数を受け取って…
            selector = keySelector;
        }

        // 比較の際に関数を通してからEqualsやGetHashCodeをする
        public bool Equals(T x, T y)
            => selector(x).Equals(selector(y));
        public int GetHashCode(T obj)
            => selector(obj).GetHashCode();
    }
}
