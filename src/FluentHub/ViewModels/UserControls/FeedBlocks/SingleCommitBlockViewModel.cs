using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Utils;

namespace FluentHub.ViewModels.UserControls.FeedBlocks
{
	public class SingleCommitBlockViewModel : ObservableObject
	{
		private PushEventPayload _pushEventPayload = default!;
		public PushEventPayload PushEventPayload { get => _pushEventPayload; set => SetProperty(ref _pushEventPayload, value); }
	}
}
