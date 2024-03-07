﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace ProceduralWorldGenerator.Common.Converters
{
    public class BooleanToVisibilityConverter : MarkupExtension, IValueConverter
    {
        public object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value?.ToString();
            if (bool.TryParse(stringValue, out var b))
                return (Negate ? !b : b) ? Visibility.Visible : FalseVisibility;
            if (double.TryParse(stringValue, out var d))
                return (Negate ? !(d > 0) : d > 0) ? Visibility.Visible : FalseVisibility;

            var result = value != null;
            return (Negate ? !result : result) ? Visibility.Visible : FalseVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is Visibility v && v == Visibility.Visible;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        public bool Negate { get; set; }
        public Visibility FalseVisibility { get; set; } = Visibility.Collapsed;
    }
}