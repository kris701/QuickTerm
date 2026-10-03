using CommunityToolkit.Mvvm.ComponentModel;
using QuickTerm.Models;
using QuickTerm.Models.Github;
using QuickTerm.Services;
using SerializableHttps;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Wpf.Ui.Appearance;

namespace QuickTerm.Windows
{
	[ObservableObject]
	public partial class TrayHostWindow : Window
	{
		[ObservableProperty]
		private ConfigModel _config;

		[ObservableProperty]
		private bool _updateAvailable = false;
		private string _assetUrl = "";

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

		private async void Window_Loaded(object sender, RoutedEventArgs e)
		{
			if (Config.Nodes.Count == 0)
				_settingsWindow.Show();

			if (!trayIcon.IsRegistered)
				trayIcon.Register();

			if (Config.CheckForUpdates)
			{
				var http = new SerializableHttpsClient();
				http.AddHeader("X-GitHub-Api-Version", "2026-03-10");
				http.AddHeader("User-Agent", "QuickTerm-Client");
				http.AddHeader("Accept", "application/vnd.github+json");

				var url = $"https://api.github.com/repos/kris701/QuickTerm/releases/latest";
				var version = await http.GetAsync<GithubReleaseModel>(url);

				var thisVersion = Assembly.GetEntryAssembly()?.GetName().Version!;
				var thisVersionStr = $"v{thisVersion.Major}.{thisVersion.Minor}.{thisVersion.Build}";

				if (version != null && version.TagName != thisVersionStr)
				{
					var asset = version.Assets.FirstOrDefault(x => x.Name == "QuickTerm.exe");
					if (asset != null)
					{
						UpdateAvailable = true;
						_assetUrl = asset.DownloadURL;
					}
				}
			}
		}

		private void ExitButton_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

		private void SettingsButton_Click(object sender, RoutedEventArgs e) {
			if(!_settingsWindow.IsVisible)
				_settingsWindow.ShowDialog();
		}

		private async void ExecuteCommand_Click(object sender, RoutedEventArgs e)
		{
			if (sender is MenuItem item && item.Tag is TerminalCommand node)
				await _executorService.ExecuteCommand(node);
		}

		private async void UpdateButton_Click(object sender, RoutedEventArgs e)
		{
			HttpClient webClient = new HttpClient();
			webClient.DefaultRequestHeaders.Add("X-GitHub-Api-Version", "2026-03-10");
			webClient.DefaultRequestHeaders.Add("User-Agent", "QuickTerm-Client");
			webClient.DefaultRequestHeaders.Add("Accept", "application/octet-stream");
			var stream = await webClient.GetStreamAsync(_assetUrl);
			var ms = new MemoryStream();
			await stream.CopyToAsync(ms);

			if (Directory.Exists("tmp"))
				Directory.Delete("tmp", true);
			Directory.CreateDirectory("tmp");
			var path = Path.Combine("tmp", "QuickTerm.exe");
			await File.WriteAllBytesAsync(path, ms.ToArray());

			Process p = new Process();
			p.StartInfo.FileName = "powershell.exe";
			p.StartInfo.Arguments = "Start-Sleep -Seconds 2 ; Remove-Item ./QuickTerm.exe ; Move-Item -Path ./tmp/QuickTerm.exe -Destination ./QuickTerm.exe ; ./QuickTerm.exe";
			p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
			p.Start();
			Application.Current.Shutdown();
		}
	}
}