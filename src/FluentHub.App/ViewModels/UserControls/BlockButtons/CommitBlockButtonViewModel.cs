namespace FluentHub.App.ViewModels.UserControls.BlockButtons
{
	public class CommitBlockButtonViewModel : ObservableObject
	{
		public CommitBlockButtonViewModel()
		{
		}

		private Commit _commitItem;
		public Commit CommitItem { get => _commitItem; set => SetProperty(ref _commitItem, value); }

		private PullRequest _pullRequest;
		public PullRequest PullRequest { get => _pullRequest; set => SetProperty(ref _pullRequest, value); }
	}
}
