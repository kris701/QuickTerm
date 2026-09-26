using QuickTerm.Models;
using System.Windows;

namespace QuickTerm.Windows
{
	public partial class TrayHostWindow : Window
	{
		private readonly ConfigModel _config;
		private readonly SettingsWindow _settingsWindow;
		public TrayHostWindow(ConfigModel config, SettingsWindow settingsWindow)
		{
			_config = config;
			_settingsWindow = settingsWindow;

			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (_config.Nodes.Count == 0)
				_settingsWindow.ShowDialog();
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e)
		{
			_config.Save();
			Application.Current.Shutdown();
		}

		private void SettingsButton_Click(object sender, RoutedEventArgs e)
		{
			_settingsWindow.ShowDialog();
		}
	}
}