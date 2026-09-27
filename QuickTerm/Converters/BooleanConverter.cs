using System.Globalization;
using System.Windows.Data;

namespace QuickTerm.Converters
{
	public class BooleanConverter<T> : IValueConverter
		where T : notnull
	{
		public T True { get; set; }
		public T False { get; set; }

		public BooleanConverter(T trueValue, T falseValue)
		{
			True = trueValue;
			False = falseValue;
		}

		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool v)
				return v ? True : False;
			return False;
		}

		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value is T && EqualityComparer<T>.Default.Equals((T)value, True);
		}
	}
}
