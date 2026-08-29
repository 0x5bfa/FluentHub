using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.Controls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Application.Models;

namespace FluentHub.Controls.BlockButtons
{
	public sealed partial class ProjectBlockButton : UserControl
	{
		#region dprops
		public static readonly DependencyProperty ViewModelProperty
			= DependencyProperty.Register(
				  nameof(ViewModel),
				  typeof(ProjectBlockButtonViewModel),
				  typeof(ProjectBlockButton),
				  new PropertyMetadata(null)
				);

		public ProjectBlockButtonViewModel ViewModel
		{
			get => (ProjectBlockButtonViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				DataContext = ViewModel;
			}
		}
		#endregion

		public ProjectBlockButton()
			=> InitializeComponent();

		private async void OnButtonClick(object sender, RoutedEventArgs e)
		{
			if (Uri.TryCreate(ViewModel.Item.Url, UriKind.Absolute, out var uri))
				await Windows.System.Launcher.LaunchUriAsync(uri);
		}
	}
}
