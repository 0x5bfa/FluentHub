using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace FluentHub.Uwp.Converters
{
    public class SizeToCornerRadiusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool isSquare = false;
            CornerRadius cr;

            if (parameter is string param && string.Compare(param, "true", true) == 0)
            {
                isSquare = true;
            }

            if (isSquare)
            {
                var crNum = (double)value / 8;

                // Set valid value
                if (crNum <= 6)
                    crNum = 6;
                else if (crNum >= 12)
                    crNum = 12;

                cr = new(crNum);
            }
            else
            {
                cr = new((double)value / 2);
            }

            return cr;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => throw new NotImplementedException();
    }
}
