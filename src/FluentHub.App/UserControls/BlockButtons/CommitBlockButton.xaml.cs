using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.BlockButtons
{
	public sealed partial class CommitBlockButton : UserControl
	{
		#region propdp
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(CommitBlockButtonViewModel),
				typeof(CommitBlockButtonViewModel),
				typeof(CommitBlockButton),
				new PropertyMetadata(null));

		public CommitBlockButtonViewModel ViewModel
		{
			get => (CommitBlockButtonViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}
		#endregion

		public CommitBlockButton()
			=> InitializeComponent();

		private void CommitItemButton_Click(object sender, RoutedEventArgs e)
		{
			Type frameType;

			var param = new FrameNavigationParameter()
			{
				PrimaryText = ViewModel.CommitItem.Repository.Owner.Login,
				SecondaryText = ViewModel.CommitItem.Repository.Name,
				Parameters = ViewModel.CommitItem,
			};

			if (ViewModel.PullRequest == null)
			{
				frameType = typeof(Views.Repositories.Commits.CommitPage);
			}
			else
			{
				frameType = typeof(Views.Repositories.PullRequests.CommitPage);
				param.Number = ViewModel.PullRequest.Number;
			}

			var navService = Ioc.Default.GetRequiredService<INavigationService>();
			navService.Navigate(frameType, param);
		}
	}
}
