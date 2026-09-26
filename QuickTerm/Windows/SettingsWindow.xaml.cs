using QuickTerm.Models;
using System.Windows;

namespace QuickTerm.Windows
{
	public partial class SettingsWindow : Window
	{
		private readonly ConfigModel _config;
		public SettingsWindow(ConfigModel config)
		{
			_config = config;
			InitializeComponent();
		}
	}
}
