using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.UserControls.Blocks
{
    public class SingleCommitBlockViewModel : ObservableObject
    {
        private PushEventPayload _pushEventPayload;
        public PushEventPayload PushEventPayload { get => _pushEventPayload; set => SetProperty(ref _pushEventPayload, value); }
    }
}
