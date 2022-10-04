using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.UserControls.FeedBlocks
{
    public class SingleCommitBlockViewModel : ObservableObject
    {
        private PushEventPayload _pushEventPayload;
        public PushEventPayload PushEventPayload { get => _pushEventPayload; set => SetProperty(ref _pushEventPayload, value); }
    }
}
