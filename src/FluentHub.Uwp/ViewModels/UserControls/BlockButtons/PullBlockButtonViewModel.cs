using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.UserControls.BlockButtons
{
    public class PullBlockButtonViewModel : ObservableObject
    {
        public PullBlockButtonViewModel()
        {
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
