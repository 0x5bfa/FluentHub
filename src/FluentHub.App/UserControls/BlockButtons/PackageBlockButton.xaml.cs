using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.BlockButtons
{
	public sealed partial class PackageBlockButton : UserControl
	{
		#region dprops
		public static readonly DependencyProperty ViewModelProperty
			= DependencyProperty.Register(
				  nameof(ViewModel),
				  typeof(PackageBlockButtonViewModel),
				  typeof(PackageBlockButton),
				  new PropertyMetadata(null)
				);

		public PackageBlockButtonViewModel ViewModel
		{
			get => (PackageBlockButtonViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				DataContext = ViewModel;
			}
		}
		#endregion

		public PackageBlockButton()
		 => InitializeComponent();

		private void Button_Click(object sender, RoutedEventArgs e)
		{
		}
	}
}
