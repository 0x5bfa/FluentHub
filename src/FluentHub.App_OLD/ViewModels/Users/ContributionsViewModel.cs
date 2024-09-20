// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls.BlockButtons;

namespace FluentHub.App.ViewModels.Users
{
	public class ContributionsViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<DiscussionBlockButtonViewModel> _discussions;
		public ReadOnlyObservableCollection<DiscussionBlockButtonViewModel> DiscussionItems { get; }

		public IAsyncRelayCommand LoadUserDiscussionsPageCommand { get; }

		public ContributionsViewModel() : base()
		{
			_discussions = new();
			DiscussionItems = new(_discussions);

			LoadUserDiscussionsPageCommand = new AsyncRelayCommand(LoadUserDiscussionsPageAsync);
		}

		private async Task LoadUserDiscussionsPageAsync()
		{
			SetTabInformation("Contributions", "Contributions", "Contributions");
			SetLoadingProgress(true);

			_currentTaskingMethodName = nameof(LoadUserDiscussionsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserDiscussionsAsync);
				await LoadUserDiscussionsAsync(Login);

				SetTabInformation("Contributions", "Contributions");
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
		}
	}
}
