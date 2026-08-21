// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Data.Parameters;
using FluentHub.Services;
using FluentHub.ViewModels.Repositories.Issues;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using FluentHub.Octokit.Contracts;

namespace FluentHub.Views.Repositories.Issues
{
	public sealed partial class IssuePage : LocatablePage
	{
		public IssueViewModel ViewModel;

		public IssuePage()
			: base(NavigationPageKind.Repository, NavigationPageKey.Issues)
		{
			InitializeComponent();

			ViewModel = Ioc.Default.GetRequiredService<IssueViewModel>();
			_pageLoadCommand = ViewModel.LoadRepositoryIssuePageCommand;
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			var command = ViewModel.LoadRepositoryIssuePageCommand;
			if (command.CanExecute(null))
				command.Execute(null);
		}

		private async void OnEditIssueClicked(object sender, RoutedEventArgs e)
		{
			var titleBox = new TextBox
			{
				Header = "Title",
				Text = ViewModel.IssueItem.Title,
			};
			var bodyBox = new TextBox
			{
				Header = "Description",
				AcceptsReturn = true,
				MinHeight = 200,
				Text = ViewModel.IssueItem.Body,
				TextWrapping = TextWrapping.Wrap,
			};
			var content = new StackPanel { Spacing = 12 };
			content.Children.Add(titleBox);
			content.Children.Add(bodyBox);

			var dialog = new ContentDialog
			{
				Title = "Edit issue",
				Content = content,
				PrimaryButtonText = "Save",
				CloseButtonText = "Cancel",
				DefaultButton = ContentDialogButton.Primary,
				XamlRoot = XamlRoot,
			};

			if (await dialog.ShowAsync() == ContentDialogResult.Primary)
				await ViewModel.UpdateIssueAsync(titleBox.Text, bodyBox.Text);
		}

		private async void OnEditMetadataClicked(object sender, RoutedEventArgs e)
		{
			Repository options;
			try
			{
				options = await ViewModel.GetIssueOptionsAsync();
			}
			catch (Exception ex)
			{
				await new ContentDialog
				{
					Title = "Unable to load issue metadata",
					Content = ex.Message,
					CloseButtonText = "Close",
					XamlRoot = XamlRoot,
				}.ShowAsync();
				return;
			}

			var assignees = new ListView
			{
				Header = "Assignees",
				DisplayMemberPath = nameof(User.Login),
				ItemsSource = options.AssignableUsers?.Nodes?.OfType<User>().ToList(),
				MaxHeight = 140,
				SelectionMode = ListViewSelectionMode.Multiple,
			};
			SelectExistingItems(assignees, ViewModel.IssueItem.Assignees?.Nodes?.OfType<User>().Select(x => x.Id.ToString()));

			var labels = new ListView
			{
				Header = "Labels",
				DisplayMemberPath = nameof(Label.Name),
				ItemsSource = options.Labels?.Nodes?.OfType<Label>().ToList(),
				MaxHeight = 140,
				SelectionMode = ListViewSelectionMode.Multiple,
			};
			SelectExistingItems(labels, ViewModel.IssueItem.Labels?.Nodes?.OfType<Label>().Select(x => x.Id.ToString()));

			var milestone = new ComboBox
			{
				Header = "Milestone",
				DisplayMemberPath = nameof(Milestone.Title),
				HorizontalAlignment = HorizontalAlignment.Stretch,
			};
			milestone.Items.Add(new Milestone { Title = "No milestone" });
			foreach (var item in options.Milestones?.Nodes?.OfType<Milestone>() ?? [])
				milestone.Items.Add(item);
			milestone.SelectedIndex = 0;
			if (ViewModel.IssueItem.Milestone is not null)
			{
				for (var i = 1; i < milestone.Items.Count; i++)
				{
					if (milestone.Items[i] is Milestone item
						&& item.Id.ToString() == ViewModel.IssueItem.Milestone.Id.ToString())
					{
						milestone.SelectedIndex = i;
						break;
					}
				}
			}

			var content = new StackPanel { Spacing = 12 };
			content.Children.Add(assignees);
			content.Children.Add(labels);
			content.Children.Add(milestone);

			var dialog = new ContentDialog
			{
				Title = "Edit issue metadata",
				Content = content,
				PrimaryButtonText = "Save",
				CloseButtonText = "Cancel",
				DefaultButton = ContentDialogButton.Primary,
				XamlRoot = XamlRoot,
			};

			if (await dialog.ShowAsync() == ContentDialogResult.Primary)
			{
				await ViewModel.UpdateMetadataAsync(
					assignees.SelectedItems.OfType<User>().ToList(),
					labels.SelectedItems.OfType<Label>().ToList(),
					milestone.SelectedIndex == 0 ? null : milestone.SelectedItem as Milestone);
			}
		}

		private async void OnCloseIssueClicked(object sender, RoutedEventArgs e)
		{
			if (sender is not MenuFlyoutItem item)
				return;

			var reason = item.Tag as string == "NotPlanned"
				? IssueClosedStateReason.NotPlanned
				: IssueClosedStateReason.Completed;
			await ViewModel.CloseIssueAsync(reason);
		}

		private static void SelectExistingItems(ListView listView, IEnumerable<string>? selectedIds)
		{
			var ids = selectedIds?.ToHashSet() ?? [];
			foreach (var item in listView.Items.OfType<object>())
			{
				var id = item switch
				{
					User user => user.Id.ToString(),
					Label label => label.Id.ToString(),
					_ => string.Empty,
				};
				if (ids.Contains(id))
					listView.SelectedItems.Add(item);
			}
		}
	}
}
