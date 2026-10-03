using Microsoft.Extensions.DependencyInjection;
using QuickTerm.Assets;
using QuickTerm.Models;
using QuickTerm.Services;
using QuickTerm.Services.Executors;
using QuickTerm.Windows;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Windows;
using System.Windows.Media;
using Wpf.Ui.Appearance;

[assembly: DisableDpiAwareness]
namespace QuickTerm
{
	public partial class App : Application
	{
		private readonly IServiceProvider _serviceProvider;

		public App()
		{
			var assembly = Assembly.GetEntryAssembly()?.GetName();
			var thisVersion = assembly!.Version!;
			var thisVersionStr = $"{thisVersion.Major}.{thisVersion.Minor}.{thisVersion.Build}";
			MetaData.AppVersion = thisVersionStr;
			MetaData.FullName = assembly!.FullName;

			var serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);
			_serviceProvider = serviceCollection.BuildServiceProvider();
		}

		protected override void OnStartup(StartupEventArgs e)
		{
			ApplicationThemeManager.ApplySystemTheme();

			var window = _serviceProvider.GetRequiredService<TrayHostWindow>();
			window.Show();

			base.OnStartup(e);
		}

		private void ConfigureServices(IServiceCollection services)
		{
			// Load config
			if (File.Exists("config.json"))
			{
				var model = JsonSerializer.Deserialize<ConfigModel>(File.ReadAllText("config.json"));
				if (model != null)
					services.AddSingleton(model);
				else
					services.AddSingleton(new ConfigModel());
			}
			else
				services.AddSingleton(new ConfigModel());

			// Load services
			services.AddSingleton<CMDExecutorService>();
			services.AddSingleton<PowershellExecutorService>();
			services.AddSingleton<TerminalExecutorService>();

			// Setup windows
			services.AddSingleton<SettingsWindow>();
			services.AddSingleton<TrayHostWindow>();
		}
	}

}
