using CommunityToolkit.Mvvm.ComponentModel;
using QuickTerm.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuickTerm.UserControls
{
	[ObservableObject]
	public partial class TerminalNodeControl : UserControl
    {
		[ObservableProperty]
		private TerminalNode _data = new TerminalNode();

		public static readonly DependencyProperty DataProperty =
			DependencyProperty.Register(
				"Data",
				typeof(TerminalNode),
				typeof(TerminalNodeControl),
				new PropertyMetadata(new TerminalNode(), (s, e) =>
				{
					if (s is TerminalNodeControl con)
						con.Data = (TerminalNode)e.NewValue;
				}));

		public TerminalNodeControl()
        {
			DataContext = this;

			InitializeComponent();
        }
    }
}
