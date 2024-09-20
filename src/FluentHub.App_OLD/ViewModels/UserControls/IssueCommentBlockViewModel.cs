using FluentHub.App.Utils;

namespace FluentHub.App.ViewModels.UserControls
{
	public class IssueCommentBlockViewModel : ObservableObject
	{
		private readonly ILogger _logger;

		private readonly IMessenger _messenger;

		private IssueComment _issueComment;
		public IssueComment IssueComment { get => _issueComment; set => SetProperty(ref _issueComment, value); }

		private int _thumbsUpCount;
		public int ThumbsUpCount { get => _thumbsUpCount; set => SetProperty(ref _thumbsUpCount, value); }

		private int _thumbsDownCount;
		public int ThumbsDownCount { get => _thumbsDownCount; set => SetProperty(ref _thumbsDownCount, value); }

		private int _laughCount;
		public int LaughCount { get => _laughCount; set => SetProperty(ref _laughCount, value); }

		private int _hoorayCount;
		public int HoorayCount { get => _hoorayCount; set => SetProperty(ref _hoorayCount, value); }

		private int _confusedCount;
		public int ConfusedCount { get => _confusedCount; set => SetProperty(ref _confusedCount, value); }

		private int _heartCount;
		public int HeartCount { get => _heartCount; set => SetProperty(ref _heartCount, value); }

		private int _rocketCount;
		public int RocketCount { get => _rocketCount; set => SetProperty(ref _rocketCount, value); }

		private int _eyesCount;
		public int EyesCount { get => _eyesCount; set => SetProperty(ref _eyesCount, value); }

		public IssueCommentBlockViewModel(IMessenger messenger = null, ILogger logger = null)
		{
			_messenger = messenger;
			_logger = logger;
		}
	}
}
