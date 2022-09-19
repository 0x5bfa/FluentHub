using Humanizer;
using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace FluentHub.Uwp.Converters
{
    public class StringToQuantityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = value.ToString();
            var param = System.Convert.ToInt32(parameter);

            return val.ToQuantity(param);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
