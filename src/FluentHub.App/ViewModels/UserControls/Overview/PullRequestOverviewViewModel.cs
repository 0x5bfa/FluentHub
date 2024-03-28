// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.App.ViewModels.UserControls.Overview
{
	public class PullRequestOverviewViewModel : ObservableObject
	{
		private PullRequest _pullRequest;
		public PullRequest PullRequest { get => _pullRequest; set => SetProperty(ref _pullRequest, value); }

		private string _selectedTag;
		public string SelectedTag { get => _selectedTag; set => SetProperty(ref _selectedTag, value); }

		private bool _Loaded;
		public bool Loaded { get => _Loaded; set => SetProperty(ref _Loaded, value); }
	}
}
