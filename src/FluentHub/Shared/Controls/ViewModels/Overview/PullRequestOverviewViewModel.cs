// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application.Models;

namespace FluentHub.Shared.Controls.ViewModels.Overview
{
	public class PullRequestOverviewViewModel : ObservableObject
	{
		private PullRequest _pullRequest = default!;
		public PullRequest PullRequest { get => _pullRequest; set => SetProperty(ref _pullRequest, value); }

		private string _selectedTag = default!;
		public string SelectedTag { get => _selectedTag; set => SetProperty(ref _selectedTag, value); }

		private bool _Loaded;
		public bool Loaded { get => _Loaded; set => SetProperty(ref _Loaded, value); }
	}
}
