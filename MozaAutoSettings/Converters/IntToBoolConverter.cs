using Microsoft.UI.Xaml.Data;
using System;

namespace MozaAutoSettings.Converters
{
    public class IntToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return (int)value == 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return (bool)value ? 1 : 0;
        }
    }
}