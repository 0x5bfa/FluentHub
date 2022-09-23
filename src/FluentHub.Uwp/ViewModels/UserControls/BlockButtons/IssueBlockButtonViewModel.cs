using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.Labels;

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

        private readonly ObservableCollection<LabelControlViewModel> _labelViewModels;
        public ReadOnlyObservableCollection<LabelControlViewModel> LabelViewModels { get; }

        private LabelControlViewModel _commentCountLabel;
        public LabelControlViewModel CommentCountLabel { get => _commentCountLabel; set => SetProperty(ref _commentCountLabel, value); }
        #endregion

        public void LoadContents()
        {
            CommentCountLabel.Name = IssueItem.Comments.TotalCount.ToString();

            _labelViewModels.Clear();
            foreach (var label in IssueItem.Labels.Nodes)
            {
                LabelControlViewModel viewModel = new()
                {
                    Name = label.Name,
                    Color = label.Color,
                };

                _labelViewModels.Add(viewModel);
            }
        }
    }
}
