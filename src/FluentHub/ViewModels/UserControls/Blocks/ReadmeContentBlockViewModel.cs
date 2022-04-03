using FluentHub.Helpers;
using FluentHub.Octokit.Queries;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class ReadmeContentBlockViewModel : INotifyPropertyChanged
    {
        private RepoContextViewModel repoContextViewModel;
        public RepoContextViewModel RepoContextViewModel { get; set; }

        private string fileName;
        public string FileName { get => fileName; set => SetProperty(ref fileName, value); }

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        private string markdownText;
        public string MarkdownText { get => markdownText; set => SetProperty(ref markdownText, value); }

        private string htmlText;
        public string HtmlText { get => htmlText; set => SetProperty(ref htmlText, value); }

        private Visibility hasReadme = Visibility.Collapsed;
        public Visibility HasReadme { get => hasReadme; set => SetProperty(ref hasReadme, value); }

        public void GetMarkdownContent(ref WebView webView)
        {
            _ = GetMarkdownContentAsync(webView);
        }

        public async Task GetMarkdownContentAsync(WebView webView)
        {
            try
            {
                HasReadme = Visibility.Collapsed;
                 var repo = await App.Client.Repository.Get(RepoContextViewModel.Owner, RepoContextViewModel.Name);

                MarkdownQueries markdown = new();

                global::Octokit.Readme readme = await App.Client.Repository.Content.GetReadme(repo.Id);
                FileName = readme.Name;
                HasReadme = Visibility.Visible;
                string bodyHtml = await readme.GetHtmlContent();

                string missedPath = "https://raw.githubusercontent.com/" + repo.Owner.Login + "/" + repo.Name + "/" + repo.DefaultBranch + "/";
                HtmlText = await markdown.GetHtmlAsync(bodyHtml, missedPath, ThemeHelper.ActualTheme.ToString().ToLower(), true);
                webView.NavigateToString(HtmlText);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
