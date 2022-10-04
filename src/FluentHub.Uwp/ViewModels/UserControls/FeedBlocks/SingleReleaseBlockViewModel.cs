using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.UserControls.FeedBlocks
{
    public class SingleReleaseBlockViewModel :ObservableObject
    {
        private ReleaseEventPayload _releaseEventPayload;
        public ReleaseEventPayload ReleaseEventPayload { get => _releaseEventPayload; set => SetProperty(ref _releaseEventPayload, value); }
    }
}
