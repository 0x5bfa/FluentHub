using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using Windows.UI.Xaml.Controls;

namespace FluentHub.Uwp.ViewModels.UserControls.BlockButtons
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
