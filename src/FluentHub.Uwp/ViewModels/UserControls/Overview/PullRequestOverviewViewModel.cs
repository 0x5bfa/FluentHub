using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.UserControls.Labels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Uwp;

namespace FluentHub.Uwp.ViewModels.UserControls.Overview
{
    public class PullRequestOverviewViewModel : ObservableObject
    {
        #region Fields and Properties
        private PullRequest _pullRequest;
        public PullRequest PullRequest { get => _pullRequest; set => SetProperty(ref _pullRequest, value); }

        private string _selectedTag;
        public string SelectedTag { get => _selectedTag; set => SetProperty(ref _selectedTag, value); }
        #endregion
    }
}
