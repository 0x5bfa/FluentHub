// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Users;

namespace FluentHub.App.ViewModels.Users
{
	public class IssuesViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<IssueBlockButtonViewModel> _issueItems;
		public ReadOnlyObservableCollection<IssueBlockButtonViewModel> IssueItems { get; }

		public IAsyncRelayCommand LoadUserIssuesPageCommand { get; }
		public IAsyncRelayCommand LoadUserIssuesFurtherCommand { get; }

		public IssuesViewModel() : base()
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
			IssueQueries queries = new();

			var result = await queries.GetAllAsync(login, 20);
			if (result.Response is null || result.PageInfo is null)
				return;

			_lastPageInfo = result.PageInfo;
			var items = (List<Issue>)result.Response;

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
				IssueQueries queries = new();

				var result = await queries.GetAllAsync(Login, 20, _lastPageInfo.EndCursor);
				if (result.Response is null || result.PageInfo is null)
					return;

				_lastPageInfo = result.PageInfo;
				var items = (List<Issue>)result.Response;

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
