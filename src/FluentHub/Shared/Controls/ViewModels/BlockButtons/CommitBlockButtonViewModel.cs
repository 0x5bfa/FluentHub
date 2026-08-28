using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Application.Models;

namespace FluentHub.Shared.Controls.ViewModels.BlockButtons
{
	public class CommitBlockButtonViewModel : ObservableObject
	{
		public CommitBlockButtonViewModel()
		{
		}

		private Commit _commitItem = default!;
		public Commit CommitItem { get => _commitItem; set => SetProperty(ref _commitItem, value); }

		private PullRequest _pullRequest = default!;
		public PullRequest PullRequest { get => _pullRequest; set => SetProperty(ref _pullRequest, value); }
	}
}
