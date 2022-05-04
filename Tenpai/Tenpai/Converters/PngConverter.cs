using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using Tenpai.Models.Tiles;

namespace Tenpai.Converters
{
    public class PngConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == string.Empty || value is null)
                return DependencyProperty.UnsetValue;
            var tile = value as Tile;
            if (tile is Dummy)
                return DependencyProperty.UnsetValue;
            if (tile is null)
                tile = Tile.CreateInstance(value as string);
            return new Uri(new Uri(Directory.GetCurrentDirectory() + "/net5.0-windows"), $"Assets/{tile.Display}.png");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
