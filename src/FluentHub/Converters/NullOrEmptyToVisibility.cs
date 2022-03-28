using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

#nullable enable

namespace FluentHub.Converters
{
    public class StringNullOrEmptyToTrueConverter : ValueConverter<string, Visibility>
    {
        public bool Inverse { get; set; }

        protected override Visibility Convert(string? value, object? parameter, string? language)
        {
            bool boolean = (Inverse ? !string.IsNullOrEmpty(value) : string.IsNullOrEmpty(value));

            return boolean ? Visibility.Collapsed : Visibility.Visible;
        }

        protected override string ConvertBack(Visibility value, object? parameter, string? language)
        {
            return string.Empty;
        }
    }
}
