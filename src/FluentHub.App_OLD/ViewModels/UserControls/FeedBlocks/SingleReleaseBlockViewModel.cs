namespace FluentHub.App.ViewModels.UserControls.FeedBlocks
{
	public class SingleReleaseBlockViewModel : ObservableObject
	{
		private ReleaseEventPayload _releaseEventPayload;
		public ReleaseEventPayload ReleaseEventPayload { get => _releaseEventPayload; set => SetProperty(ref _releaseEventPayload, value); }
	}
}
