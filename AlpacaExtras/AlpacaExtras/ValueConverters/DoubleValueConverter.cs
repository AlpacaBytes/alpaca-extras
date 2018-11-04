
using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace AlpacaExtras.ValueConverters
{
    [Preserve(AllMembers = true)]
    public class DoubleValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value ?? string.Empty).ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double.TryParse(value.ToString(), out double i);
            return i;
        }
    }
}
