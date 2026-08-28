using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Shared.Controls.ViewModels.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Application.Models;

namespace FluentHub.Shared.Controls.Views.BlockButtons
{
	public sealed partial class PullBlockButton : UserControl
	{
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(PullRequest),
				typeof(PullBlockButtonViewModel),
				typeof(PullBlockButton),
				new PropertyMetadata(null));

		public PullBlockButtonViewModel ViewModel
		{
			get => (PullBlockButtonViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				ViewModel?.LoadContents();
			}
		}

		public PullBlockButton()
		{
			InitializeComponent();
		}

		private async void OnClick(object sender, RoutedEventArgs e)
		{
			var service = Ioc.Default.GetRequiredService<INavigationService>();
			var repository = new RepositorySlug(
				ViewModel.PullItem.Repository.Owner.Login,
				ViewModel.PullItem.Repository.Name);
			await service.NavigateAsync(new RepositoryPullRequestRoute(repository, ViewModel.PullItem.Number));
		}
	}
}
