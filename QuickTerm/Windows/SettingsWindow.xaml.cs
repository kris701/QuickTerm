using CommunityToolkit.Mvvm.ComponentModel;
using QuickTerm.Models;
using System.Windows;
using Wpf.Ui.Controls;

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

		private void AddRootNode_Click(object sender, RoutedEventArgs e)
		{
			var newConfig = new ConfigModel(Config);
			newConfig.Nodes.Add(new TerminalNode()
			{
				Name = "New Node"
			});
			Config = newConfig;
		}

		private void RemoveNodeButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Button but1 && but1.Tag is TerminalNode node)
			{
				var newConfig = new ConfigModel(Config);
				newConfig.Nodes.Remove(node);
				Config = newConfig;
			}
		}
	}
}
