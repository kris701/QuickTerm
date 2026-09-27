using System.Windows;

namespace QuickTerm.Converters
{
	public sealed class ListNotEmptyConverter : CountConverter<Visibility>
	{
		public ListNotEmptyConverter() :
			base(Visibility.Collapsed, Visibility.Visible)
		{
			Count = 0;
			Comparison = ComparisonTypes.Greater;
		}
	}
}
