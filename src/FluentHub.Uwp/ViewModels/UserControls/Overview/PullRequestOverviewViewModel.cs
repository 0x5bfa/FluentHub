using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.Utils;
using Microsoft.Extensions.DependencyInjection;
using muxc = Microsoft.UI.Xaml.Controls;

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
