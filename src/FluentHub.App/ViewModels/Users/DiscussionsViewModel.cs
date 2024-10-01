// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Users;

namespace FluentHub.App.ViewModels.Users
{
	public class DiscussionsViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<DiscussionBlockButtonViewModel> _discussions;
		public ReadOnlyObservableCollection<DiscussionBlockButtonViewModel> DiscussionItems { get; }

		public IAsyncRelayCommand LoadUserDiscussionsPageCommand { get; }
		public IAsyncRelayCommand LoadUserDiscussionsFurtherCommand { get; }

		public DiscussionsViewModel() : base()
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			if (parameter.AsViewer)
			{
				var currentTabItem = _navigation.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;

				AsViewer = true;
			}

			_discussions = new();
			DiscussionItems = new(_discussions);

			LoadUserDiscussionsPageCommand = new AsyncRelayCommand(LoadUserDiscussionsPageAsync);
			LoadUserDiscussionsFurtherCommand = new AsyncRelayCommand(LoadUserDiscussionsFurtherAsync);
		}

		private async Task LoadUserDiscussionsPageAsync()
		{
			SetTabInformation("Discussions", "Discussions", "Discussions");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadUserDiscussionsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserDiscussionsAsync);
				await LoadUserDiscussionsAsync(Login);

				SetTabInformation("Discussions", "Discussions");

				if (DiscussionItems.Count == 0)
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

		private async Task LoadUserDiscussionsAsync(string login)
		{
			DiscussionQueries queries = new();

			var result = await queries.GetAllAsync(login, 20);
			if (result.Response is null || result.PageInfo is null)
				return;

			_lastPageInfo = result.PageInfo;
			var items = (List<Discussion>)result.Response;

			_discussions.Clear();
			foreach (var item in items)
			{
				DiscussionBlockButtonViewModel viewModel = new()
				{
					Item = item,
				};

				_discussions.Add(viewModel);
			}
		}

		private async Task LoadUserDiscussionsFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				DiscussionQueries queries = new();

				var result = await queries.GetAllAsync(Login, 20, _lastPageInfo.EndCursor);
				if (result.Response is null || result.PageInfo is null)
					return;

				_lastPageInfo = result.PageInfo;
				var items = (List<Discussion>)result.Response;

				foreach (var item in items)
				{
					DiscussionBlockButtonViewModel viewmodel = new()
					{
						Item = item,
					};

					_discussions.Add(viewmodel);
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
