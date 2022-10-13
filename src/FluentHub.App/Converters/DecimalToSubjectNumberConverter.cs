using Microsoft.UI.Xaml.Data;

namespace FluentHub.App.Converters
{
    internal class DecimalToSubjectNumberConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var val = System.Convert.ToInt32(value.ToString());

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
