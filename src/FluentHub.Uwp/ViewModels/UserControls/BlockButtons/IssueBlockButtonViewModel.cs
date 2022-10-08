using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.UserControls.BlockButtons
{
    public class IssueBlockButtonViewModel : ObservableObject
    {
        public IssueBlockButtonViewModel()
        {
            _labelViewModels = new();
            LabelViewModels = new(_labelViewModels);

            CommentCountLabel = new()
            {
                Color = "#36000000",
            };
        }

        #region Fields and Properties
        private Issue _issueItem;
        public Issue IssueItem { get => _issueItem; set => SetProperty(ref _issueItem, value); }

        private bool _compactMode;
        public bool CompactMode { get => _compactMode; set => SetProperty(ref _compactMode, value); }
        #endregion

        public void LoadContents()
        {
        }
    }
}
