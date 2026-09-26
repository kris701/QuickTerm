using Microsoft.Extensions.DependencyInjection;
using QuickTerm.Models;
using QuickTerm.Windows;
using System.IO;
using System.Text.Json;
using System.Windows;
using Wpf.Ui.Appearance;

namespace QuickTerm
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private IServiceProvider _serviceProvider;

		protected override void OnStartup(StartupEventArgs e)
		{
			ApplicationThemeManager.ApplySystemTheme();

			base.OnStartup(e);
			var serviceCollection = new ServiceCollection();
			ConfigureServices(serviceCollection);
			_serviceProvider = serviceCollection.BuildServiceProvider();
			var window = _serviceProvider.GetRequiredService<TrayHostWindow>();
			window.Show();
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

			// Setup windows
			services.AddSingleton<SettingsWindow>();
			services.AddSingleton<TrayHostWindow>();
		}
	}

}
