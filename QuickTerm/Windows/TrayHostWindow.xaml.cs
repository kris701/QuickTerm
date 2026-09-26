using CommunityToolkit.Mvvm.ComponentModel;
using QuickTerm.Models;
using System.Windows;
using Wpf.Ui.Appearance;

namespace QuickTerm.Windows
{
	[ObservableObject]
	public partial class TrayHostWindow : Window
	{
		[ObservableProperty]
		private ConfigModel _config;

		private readonly SettingsWindow _settingsWindow;
		
		public TrayHostWindow(ConfigModel config, SettingsWindow settingsWindow)
		{
			DataContext = this;

			Config = config;
			_settingsWindow = settingsWindow;

			SystemThemeWatcher.Watch(this);

			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (Config.Nodes.Count == 0)
				_settingsWindow.ShowDialog();
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

		private void SettingsButton_Click(object sender, RoutedEventArgs e) => _settingsWindow.ShowDialog();
	}
}