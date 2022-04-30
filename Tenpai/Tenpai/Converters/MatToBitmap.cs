using OpenCvSharp;
using OpenCvSharp.WpfExtensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Tenpai.Converters
{
    public class MatToBitmap : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is null)
                return DependencyProperty.UnsetValue;
            var mat = value as Mat;
            if (mat.Cols == 0 || mat.Rows == 0)
                return DependencyProperty.UnsetValue;
            return mat.ToWriteableBitmap();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
