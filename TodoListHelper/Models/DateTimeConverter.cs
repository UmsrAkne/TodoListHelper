using System;
using System.Globalization;
using System.Windows.Data;

namespace TodoListHelper.Models
{
public class DateTimeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return ((DateTimeOffset)value).ToString("yy/MM/dd HH:mm");
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
}