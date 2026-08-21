// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.Queries.Users;
using FluentHub.Models;
using FluentHub.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Contracts;

namespace FluentHub.ViewModels.Users
{
	public class IssuesViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<IssueBlockButtonViewModel> _issueItems;
		public ReadOnlyObservableCollection<IssueBlockButtonViewModel> IssueItems { get; }

		public IAsyncRelayCommand LoadUserIssuesPageCommand { get; }
		public IAsyncRelayCommand LoadUserIssuesFurtherCommand { get; }

		public IssuesViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			if (parameter.AsViewer)
			{
				var currentTabItem = _navigation.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;

				AsViewer = true;
			}

			_issueItems = new();
			IssueItems = new(_issueItems);

			LoadUserIssuesPageCommand = new AsyncRelayCommand(LoadUserIssuesPageAsync);
			LoadUserIssuesFurtherCommand = new AsyncRelayCommand(LoadUserIssuesFurtherAsync);
		}

		private async Task LoadUserIssuesPageAsync()
		{
			SetTabInformation("Issues", "Issues", "Issues");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadUserIssuesPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserIssuesAsync);
				await LoadUserIssuesAsync(Login);

				SetTabInformation("Issues", "Issues");

				if (IssueItems.Count == 0)
					IsEmpty = true;
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}

		private async Task LoadUserIssuesAsync(string login)
		{
			var queries = _gitHub.Users.Issues;

			var result = await queries.GetPageAsync(login, PageRequest.Forward(20));

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

			_issueItems.Clear();
			foreach (var item in items)
			{
				IssueBlockButtonViewModel viewModel = new()
				{
					IssueItem = item,
				};

				_issueItems.Add(viewModel);
			}
		}

		private async Task LoadUserIssuesFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				var queries = _gitHub.Users.Issues;

				var result = await queries.GetPageAsync(Login, PageRequest.Forward(20, _lastPageInfo.EndCursor));

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

				foreach (var item in items)
				{
					IssueBlockButtonViewModel viewmodel = new()
					{
						IssueItem = item,
					};

					_issueItems.Add(viewmodel);
				}
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}
	}
}
