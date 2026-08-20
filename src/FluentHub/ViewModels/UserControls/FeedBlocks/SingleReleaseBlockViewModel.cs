using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;

namespace FluentHub.ViewModels.UserControls.FeedBlocks
{
	public class SingleReleaseBlockViewModel :ObservableObject
	{
		private ReleaseEventPayload _releaseEventPayload = default!;
		public ReleaseEventPayload ReleaseEventPayload { get => _releaseEventPayload; set => SetProperty(ref _releaseEventPayload, value); }
	}
}
