using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;

namespace Tenpai.Converters
{
    public class EnumToStringConverter<T> : IValueConverter where T : Enum
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var enumValue = (T)value;
            return (typeof(T).GetField(enumValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), true).ElementAt(0) as DescriptionAttribute).Description;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var description = value as string;
            foreach (var wValue in Enum.GetValues(typeof(T)))
            {
                FieldInfo fi = typeof(T).GetField(wValue.ToString());
                if (null != fi)
                {
                    BrowsableAttribute[] wBrowsableAttributes = fi.GetCustomAttributes(typeof(BrowsableAttribute), true) as BrowsableAttribute[];
                    if (wBrowsableAttributes.Length > 0)
                    {
                        //  If the Browsable attribute is false
                        if (wBrowsableAttributes[0].Browsable == false)
                        {
                            // Do not add the enumeration to the list.
                            continue;
                        }
                    }

                    DescriptionAttribute[] wDescriptions = fi.GetCustomAttributes(typeof(DescriptionAttribute), true) as DescriptionAttribute[];
                    if (wDescriptions.Length > 0 && wDescriptions[0].Description.Equals(description))
                    {
                        return wValue;
                    }
                }
            }
            return DependencyProperty.UnsetValue;
        }
    }
}
