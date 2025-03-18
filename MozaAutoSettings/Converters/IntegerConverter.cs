using Microsoft.UI.Xaml.Data;
using System;

namespace MozaAutoSettings.Converters
{
    public class IntegerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (int.TryParse(value.ToString(), out int result))
            {
                return result;
            }
            return 0; // or any default value
        }
    }
}
