using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace DoAn.Converters
{
    public class SelectedBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isSelected && isSelected)
                return Color.FromHex("#D3D3D3"); // Màu nền khi được chọn
            return Color.FromHex("#FFFFFF"); // Màu nền mặc định
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}