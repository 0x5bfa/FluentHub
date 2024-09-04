namespace FluentHub.App.ViewModels.UserControls.FeedBlocks
{
	public class SingleCommitBlockViewModel : ObservableObject
	{
		private PushEventPayload _pushEventPayload;
		public PushEventPayload PushEventPayload { get => _pushEventPayload; set => SetProperty(ref _pushEventPayload, value); }
	}
}
