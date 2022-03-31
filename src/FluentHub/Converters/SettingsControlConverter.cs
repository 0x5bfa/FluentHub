using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

#nullable enable

namespace FluentHub.Converters
{
    public abstract class ValueConverter<TSource, TTarget> : IValueConverter
    {
        public TTarget? Convert(TSource? value)
        {
            return Convert(value, null, null);
        }

        public TSource? ConvertBack(TTarget? value)
        {
            return ConvertBack(value, null, null);
        }

        public object? Convert(object? value, Type? targetType, object? parameter, string? language)
        {
            // CastExceptions will occur when invalid value, or target type provided.
            return Convert((TSource?)value, parameter, language);
        }

        public object? ConvertBack(object? value, Type? targetType, object? parameter, string? language)
        {
            // CastExceptions will occur when invalid value, or target type provided.
            return ConvertBack((TTarget?)value, parameter, language);
        }

        protected virtual TTarget? Convert(TSource? value, object? parameter, string? language)
        {
            throw new NotSupportedException();
        }

        protected virtual TSource? ConvertBack(TTarget? value, object? parameter, string? language)
        {
            throw new NotSupportedException();
        }
    }

    public abstract class ToObjectConverter<T> : ValueConverter<T?, object?>
    {
        protected override object? Convert(T? value, object? parameter, string? language)
        {
            return value;
        }

        protected override T? ConvertBack(object? value, object? parameter, string? language)
        {
            return (T?)value;
        }
    }

    public class InverseBooleanConverter : ValueConverter<bool, bool>
    {
        protected override bool Convert(bool value, object? parameter, string? language)
        {
            return !value;
        }

        protected override bool ConvertBack(bool value, object? parameter, string? language)
        {
            return !value;
        }
    }

    public class NullToTrueConverter : ValueConverter<object?, bool>
    {
        public bool Inverse { get; set; }

        protected override bool Convert(object? value, object? parameter, string? language)
        {
            return Inverse ? value != null : value == null;
        }

        protected override object? ConvertBack(bool value, object? parameter, string? language)
        {
            return null;
        }
    }

    public class StringNullOrWhiteSpaceToTrueConverter : ValueConverter<string, bool>
    {
        public bool Inverse { get; set; }

        protected override bool Convert(string? value, object? parameter, string? language)
        {
            return Inverse ? !string.IsNullOrWhiteSpace(value) : string.IsNullOrWhiteSpace(value);
        }

        protected override string ConvertBack(bool value, object? parameter, string? language)
        {
            return string.Empty;
        }
    }
}
