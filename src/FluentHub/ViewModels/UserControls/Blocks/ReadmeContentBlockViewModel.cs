using FluentHub.Helpers;
using FluentHub.Octokit.Queries;
using FluentHub.Octokit.Queries.Repositories;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class ReadmeContentBlockViewModel : INotifyPropertyChanged
    {
        private string fileName;
        public string FileName { get => fileName; set => SetProperty(ref fileName, value); }

        //private string filePath = "";
        //public string FilePath { get => filePath; set => SetProperty(ref filePath, value); }

        private string owner;
        public string Owner { get => owner; set => SetProperty(ref owner, value); }

        private string repoName;
        public string RepoName { get => repoName; set => SetProperty(ref repoName, value); }

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        //private string markdownText;
        //public string MarkdownText { get => markdownText; set => SetProperty(ref markdownText, value); }

        private string htmlText;
        public string HtmlText { get => htmlText; set => SetProperty(ref htmlText, value); }

        public void GetMarkdownContent(ref WebView webView)
        {
            // Do not wait
            _ = GetMarkdownContentAsync(webView);
        }

        public async Task GetMarkdownContentAsync(WebView webView)
        {
            try
            {
                var repo = await App.Client.Repository.Get(Owner, RepoName);

                MarkdownQueries markdown = new();

                global::Octokit.Readme readme = await App.Client.Repository.Content.GetReadme(repo.Id);
                FileName = readme.Name;

                string missedPath = "https://raw.githubusercontent.com/" + repo.Owner.Login + "/" + repo.Name + "/" + repo.DefaultBranch + "/";
                HtmlText = await markdown.GetHtmlAsync(readme.Content, missedPath, ThemeHelper.ActualTheme.ToString().ToLower());
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
