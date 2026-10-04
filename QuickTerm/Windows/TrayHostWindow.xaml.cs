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
		private ConfigModel _config = App.Config;

		[ObservableProperty]
		private bool _updateAvailable = false;

		private readonly SettingsWindow _settingsWindow;
		private readonly TerminalExecutorService _executorService;
		private readonly GithubUpdaterService _githubUpdaterService;

		public TrayHostWindow(SettingsWindow settingsWindow, TerminalExecutorService executorService, GithubUpdaterService githubUpdaterService)
		{
			DataContext = this;

			_settingsWindow = settingsWindow;
			_executorService = executorService;
			_githubUpdaterService = githubUpdaterService;

			SystemThemeWatcher.Watch(this);

			InitializeComponent();

			App.OnConfigUpdated += () => { Config = App.Config; };
		}

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			_settingsWindow.Show();

			if (Config.Nodes.Count > 0)
				_settingsWindow.Hide();

			if (!trayIcon.IsRegistered)
				trayIcon.Register();

			if (Config.CheckForUpdates)
				UpdateAvailable = await _githubUpdaterService.GetNewestVersion();
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

		private void SettingsButton_Click(object sender, RoutedEventArgs e)
		{
			if (!_settingsWindow.IsVisible)
				_settingsWindow.Show();
		}

		private async void ExecuteCommand_Click(object sender, RoutedEventArgs e)
		{
			if (sender is MenuItem item && item.Tag is TerminalCommand node)
				await _executorService.ExecuteCommand(node);
		}

		private async void UpdateButton_Click(object sender, RoutedEventArgs e) => await _githubUpdaterService.UpdateToNewestVersion();
	}
}