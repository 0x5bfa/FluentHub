using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.Controls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Application.Models;

namespace FluentHub.Controls.BlockButtons
{
	public sealed partial class DiscussionBlockButton : UserControl
	{
		#region dprops
		public static readonly DependencyProperty ViewModelProperty
			= DependencyProperty.Register(
				  nameof(Discussion),
				  typeof(DiscussionBlockButtonViewModel),
				  typeof(DiscussionBlockButton),
				  new PropertyMetadata(null)
				);

		public DiscussionBlockButtonViewModel ViewModel
		{
			get => (DiscussionBlockButtonViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				DataContext = ViewModel;
			}
		}
		#endregion

		public DiscussionBlockButton()
			=> InitializeComponent();

		private async void Button_Click(object sender, RoutedEventArgs e)
		{
			var navService = Ioc.Default.GetRequiredService<INavigationService>();
			var repository = new RepositorySlug(
				ViewModel.Item.Repository.Owner.Login,
				ViewModel.Item.Repository.Name);
			await navService.NavigateAsync(new RepositoryDiscussionRoute(repository, ViewModel.Item.Number));
		}
	}
}
