using FluentHub.Octokit.Models.ActivityPayloads;
using System.Collections.ObjectModel;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class SingleReleaseBlockViewModel :ObservableObject
    {
        private ReleaseEventPayload _releaseEventPayload;
        public ReleaseEventPayload ReleaseEventPayload { get => _releaseEventPayload; set => SetProperty(ref _releaseEventPayload, value); }
    }
}
