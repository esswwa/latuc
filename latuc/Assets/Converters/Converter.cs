
namespace latuc.Assets.Converters
{
    class BooleanConverter<T> : IValueConverter
    {
        public T OnTrue { get; set; }
        public T OnFalse { get; set; }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) => (bool)value ? OnTrue : OnFalse;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => object.Equals(value, OnTrue);
    }

    class BooleanToStringConverter : BooleanConverter<string> { }
}
