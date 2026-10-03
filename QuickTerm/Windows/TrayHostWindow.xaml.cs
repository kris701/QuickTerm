using CommunityToolkit.Mvvm.ComponentModel;
using QuickTerm.Models;
using QuickTerm.Services;
using System.Windows;
using System.Windows.Controls;
using Wpf.Ui.Appearance;

namespace QuickTerm.Windows
{
	[ObservableObject]
	public partial class TrayHostWindow : Window
	{
		[ObservableProperty]
		private ConfigModel _config;

		private readonly SettingsWindow _settingsWindow;
		private readonly TerminalExecutorService _executorService;

		public TrayHostWindow(ConfigModel config, SettingsWindow settingsWindow, TerminalExecutorService executorService)
		{
			DataContext = this;

			Config = config;
			_settingsWindow = settingsWindow;
			_executorService = executorService;

			SystemThemeWatcher.Watch(this);

			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (Config.Nodes.Count == 0)
				_settingsWindow.Show();

			if (!trayIcon.IsRegistered)
				trayIcon.Register();
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

		private void SettingsButton_Click(object sender, RoutedEventArgs e) => _settingsWindow.ShowDialog();

		private async void ExecuteCommand_Click(object sender, RoutedEventArgs e)
		{
			if (sender is MenuItem item && item.Tag is TerminalCommand node)
				await _executorService.ExecuteCommand(node);
		}
	}
}