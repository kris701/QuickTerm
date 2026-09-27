using System.Collections;
using System.Globalization;
using System.Windows.Data;

namespace QuickTerm.Converters
{
	public class CountConverter<T> : IValueConverter
		where T : notnull
	{
		public enum ComparisonTypes { Equals, Greater, GreaterOrEqual, Less, LessOrEqual }

		public int Count { get; set; } = 0;
		public ComparisonTypes Comparison { get; set; } = ComparisonTypes.Greater;

		public T True { get; set; }
		public T False { get; set; }

		public CountConverter(T trueValue, T falseValue)
		{
			True = trueValue;
			False = falseValue;
		}

		public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is IList lst)
			{
				switch (Comparison)
				{
					case ComparisonTypes.Equals:
						return lst.Count == Count ? True : False;
					case ComparisonTypes.Greater:
						return lst.Count > Count ? True : False;
					case ComparisonTypes.GreaterOrEqual:
						return lst.Count >= Count ? True : False;
					case ComparisonTypes.Less:
						return lst.Count < Count ? True : False;
					case ComparisonTypes.LessOrEqual:
						return lst.Count <= Count ? True : False;
				}
			}
			return False;
		}

		public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
