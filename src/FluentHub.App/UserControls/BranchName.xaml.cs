using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls
{
	public sealed partial class BranchName : UserControl
	{
		#region propdp
		public static readonly DependencyProperty BranchNameProperty =
			DependencyProperty.Register(
				nameof(Branch),
				typeof(string),
				typeof(BranchName),
				new PropertyMetadata(null));

		public string Branch
		{
			get => (string)GetValue(BranchNameProperty);
			set
			{
				SetValue(BranchNameProperty, value);
				DataContext = Branch;
			}
		}
		#endregion

		public BranchName()
			=> InitializeComponent();
	}
}
