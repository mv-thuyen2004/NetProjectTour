using System;
using System.Globalization;
using Microsoft.Maui.Controls;

namespace DoAn.Converters
{
    public class StatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int status)
            {
                return status == 0 ? "Chưa bắt đầu" : "Đã hoàn thành";
            }
            return "Không xác định";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}