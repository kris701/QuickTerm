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

		private void AddNode_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Button but1 && but1.Tag is TerminalNode node)
			{
				var newConfig = new ConfigModel(Config);
				node.Nodes.Add(new TerminalNode()
				{
					Name = "New Node"
				});
				Config = newConfig;
			}
		}

		private void RemoveNodeButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Button but1 && but1.Tag is TerminalNode node)
			{
				var newConfig = new ConfigModel(Config);
				foreach (var subNode in newConfig.Nodes)
					RemoveNodeRec(subNode, node);
				newConfig.Nodes.Remove(node);
				Config = newConfig;
			}
		}

		private void RemoveNodeRec(TerminalNode node, TerminalNode target)
		{
			foreach (var subNode in node.Nodes)
				RemoveNodeRec(subNode, target);
			node.Nodes.Remove(target);
		}

		private void AddCommandButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Button but1 && but1.Tag is TerminalNode node)
			{
				var newConfig = new ConfigModel(Config);
				node.Nodes = new List<TerminalNode>();
				node.Command = new TerminalCommand() { Command = "", ShowTerminal = true, TerminalType = TerminalTypes.PowerShell };
				Config = newConfig;
			}
		}

		private void RemoveCommandButton_Click(object sender, RoutedEventArgs e)
		{
			if (sender is Button but1 && but1.Tag is TerminalNode node)
			{
				var newConfig = new ConfigModel(Config);
				node.Nodes = new List<TerminalNode>();
				node.Command = null;
				Config = newConfig;
			}
		}
	}
}
