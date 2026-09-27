using System.Globalization;
using System.Windows.Data;

namespace QuickTerm.Converters
{
	public class NullConverter<T> : IValueConverter
		where T : notnull
	{
		public T True { get; set; }
		public T False { get; set; }

		public NullConverter(T trueValue, T falseValue)
		{
			True = trueValue;
			False = falseValue;
		}

		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value == null ? True : False;
		}

		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
