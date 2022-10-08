using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.UserControls.BlockButtons
{
    public class PullBlockButtonViewModel : ObservableObject
    {
        public PullBlockButtonViewModel()
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
        #endregion

        public void LoadContents()
        {
        }
    }
}
