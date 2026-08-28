using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Shared.Controls.ViewModels.FeedBlocks;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Application.Models;

namespace FluentHub.Shared.Controls.Views.FeedBlocks
{
	public sealed partial class ActivityBlock : UserControl
	{
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
			   nameof(ViewModel),
			   typeof(ActivityBlockViewModel),
			   typeof(ActivityBlock),
			   new PropertyMetadata(null));

		public ActivityBlockViewModel ViewModel
		{
			get => (ActivityBlockViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				ViewModel?.LoadContentsAsync();
			}
		}

		public ActivityBlock()
		{
			InitializeComponent();
		}

		private async void OnActivityRepositoryButtonClick(object sender, RoutedEventArgs e)
		{
			if (sender is not Button { Tag: Repository repo } || repo.Owner is null)
				return;

			var service = Ioc.Default.GetRequiredService<INavigationService>();

			var layout = App.AppSettings.UseDetailsView
				? RepositoryCodeLayout.Details
				: RepositoryCodeLayout.Tree;
			await service.NavigateAsync(
				new RepositoryCodeRoute(new RepositorySlug(repo.Owner.Login, repo.Name), Layout: layout));
		}
	}
}
