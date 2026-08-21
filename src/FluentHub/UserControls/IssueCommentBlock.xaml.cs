using FluentHub.Extensions;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.UserControls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using FluentHub.Octokit.Contracts;

namespace FluentHub.UserControls
{
	public sealed partial class IssueCommentBlock : UserControl
	{
		public static readonly DependencyProperty ViewModelProperty =
			DependencyProperty.Register(
				nameof(ViewModel),
				typeof(IssueCommentBlockViewModel),
				typeof(IssueCommentBlock),
				new PropertyMetadata(null));

		public IssueCommentBlockViewModel ViewModel
		{
			get => (IssueCommentBlockViewModel)GetValue(ViewModelProperty);
			set => SetValue(ViewModelProperty, value);
		}

		public IssueCommentBlock()
			=> InitializeComponent();

		private async void OnEditCommentClicked(object sender, RoutedEventArgs e)
		{
			var bodyBox = new TextBox
			{
				AcceptsReturn = true,
				MinHeight = 160,
				Text = ViewModel.IssueComment.Body,
				TextWrapping = TextWrapping.Wrap,
			};
			var dialog = new ContentDialog
			{
				Title = "Edit comment",
				Content = bodyBox,
				PrimaryButtonText = "Save",
				CloseButtonText = "Cancel",
				DefaultButton = ContentDialogButton.Primary,
				XamlRoot = XamlRoot,
			};

			if (await dialog.ShowAsync() == ContentDialogResult.Primary)
				await ViewModel.UpdateCommentAsync(bodyBox.Text);
		}

		private async void OnDeleteCommentClicked(object sender, RoutedEventArgs e)
		{
			var dialog = new ContentDialog
			{
				Title = "Delete comment?",
				Content = "This action cannot be undone.",
				PrimaryButtonText = "Delete",
				CloseButtonText = "Cancel",
				DefaultButton = ContentDialogButton.Close,
				XamlRoot = XamlRoot,
			};

			if (await dialog.ShowAsync() == ContentDialogResult.Primary)
				await ViewModel.DeleteCommentAsync();
		}

		private async void OnReactionClicked(object sender, RoutedEventArgs e)
		{
			if (sender is ToggleButton button
				&& Enum.TryParse<ReactionContent>(button.Tag as string, out var content))
			{
				await ViewModel.ToggleReactionAsync(content);
			}
		}
	}
}
