using Microsoft.UI.Xaml.Data;

namespace FluentHub.App.Converters
{
    public class DecimalToMetricConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = System.Convert.ToInt32(value.ToString());

            return val.ToMetric(false, true, 1);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
