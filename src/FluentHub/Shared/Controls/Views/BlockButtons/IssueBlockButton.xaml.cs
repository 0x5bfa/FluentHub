using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Shared.Controls.ViewModels.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Shared.Controls.Views.BlockButtons
{
	public sealed partial class IssueBlockButton : UserControl
	{
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(IssueBlockButtonViewModel),
				typeof(IssueBlockButtonViewModel),
				typeof(IssueBlockButton),
				new PropertyMetadata(null));

		public IssueBlockButtonViewModel ViewModel
		{
			get => (IssueBlockButtonViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				ViewModel?.LoadContents();
			}
		}
	   
		public IssueBlockButton()
		{
			InitializeComponent();
		}

		private async void OnClick(object sender, RoutedEventArgs e)
		{
			var service = Ioc.Default.GetRequiredService<INavigationService>();
			var repository = new RepositorySlug(
				ViewModel.IssueItem.Repository.Owner.Login,
				ViewModel.IssueItem.Repository.Name);
			await service.NavigateAsync(new RepositoryIssueRoute(repository, ViewModel.IssueItem.Number));
		}
	}
}
