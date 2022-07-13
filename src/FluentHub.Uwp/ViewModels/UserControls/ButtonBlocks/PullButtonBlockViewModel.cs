using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.Labels;

namespace FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks
{
    public class PullButtonBlockViewModel : ObservableObject
    {
        public PullButtonBlockViewModel()
        {
            _labelViewModels = new();
            LabelViewModels = new(_labelViewModels);

            CommentCountLabel = new()
            {
                Color = "#36000000",
            };

            ReviewStateLabel = new()
            {
                Name = "Reviews",
                Color = "#36000000",
            };

            StatusStateLabel = new()
            {
                Name = "Status",
                Color = "#36000000",
            };
        }

        #region Fields and Properties
        private PullRequest _pullItem;
        public PullRequest PullItem { get => _pullItem; set => SetProperty(ref _pullItem, value); }

        private readonly ObservableCollection<LabelControlViewModel> _labelViewModels;
        public ReadOnlyObservableCollection<LabelControlViewModel> LabelViewModels { get; }

        private LabelControlViewModel _commentCountLabel;
        public LabelControlViewModel CommentCountLabel { get => _commentCountLabel; set => SetProperty(ref _commentCountLabel, value); }

        private LabelControlViewModel _reviewStateLabel;
        public LabelControlViewModel ReviewStateLabel { get => _reviewStateLabel; set => SetProperty(ref _reviewStateLabel, value); }

        private LabelControlViewModel _statusStateLabel;
        public LabelControlViewModel StatusStateLabel { get => _statusStateLabel; set => SetProperty(ref _statusStateLabel, value); }
        #endregion

        public void LoadContents()
        {
            CommentCountLabel.Name = PullItem.CommentCount.ToString();

            _labelViewModels.Clear();
            foreach (var label in _pullItem.Labels)
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
