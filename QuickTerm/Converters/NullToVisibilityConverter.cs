using System.Windows;

namespace QuickTerm.Converters
{
	public sealed class NullToVisibilityConverter : NullConverter<Visibility>
	{
		public NullToVisibilityConverter() :
			base(Visibility.Collapsed, Visibility.Visible)
		{ }
	}
}
