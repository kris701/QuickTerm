using CommunityToolkit.Mvvm.ComponentModel;
using QuickTerm.Models;
using System.Windows;

namespace QuickTerm.Windows
{
	[ObservableObject]
	public partial class SettingsWindow : Window
	{
		[ObservableProperty]
		private ConfigModel _config;

		public SettingsWindow(ConfigModel config)
		{
			DataContext = this;

			Config = config;

			InitializeComponent();
		}

		private void SaveSettingsButton_Click(object sender, RoutedEventArgs e)
		{
			Config.Save();
			Close();
		}
	}
}
