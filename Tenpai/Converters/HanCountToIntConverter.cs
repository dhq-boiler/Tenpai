﻿using Reactive.Bindings;
using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Tenpai.Models.Yaku;
using Tenpai.Models.Yaku.Meld;

namespace Tenpai.Converters
{
    public class HanCountToIntConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(values[0] is Yaku))
                return DependencyProperty.UnsetValue;
            var yaku = values[0] as Yaku;
            var sarashiHai = values[1] as ReactiveCollection<Meld>;
            return yaku.HanCount(sarashiHai.Where(x => (x is Quad q && q.Type != KongType.ConcealedKong) || (x is not Quad)).Count() > 0);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
