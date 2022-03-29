using FluentHub.Models.Items;
using FluentHub.ViewModels.UserControls.Blocks;
using FluentHub.Octokit.Queries.Repositories;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Repositories.Layouts
{
    public class DetailsLayoutViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DetailsLayoutListViewItem> Items { get; private set; } = new();

        private RepoContextViewModel contextViewModel;
        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

        private ReadmeContentBlockViewModel readmeBlockViewModel;
        public ReadmeContentBlockViewModel ReadmeBlockViewModel { get => readmeBlockViewModel; set => SetProperty(ref readmeBlockViewModel, value); }

        public async Task EnumRepositoryContents()
        {
            CommitQueries queries = new();
            var fileOverviews = await queries.GetOverviewAllFilesAndLatestCommit(
                ContextViewModel.Name,
                ContextViewModel.Owner,
                ContextViewModel.BranchName,
                ContextViewModel.Path);

            ContextViewModel.IsDir = true;

            if (ContextViewModel.Path == "/")
            {
                ContextViewModel.IsRootDir = true;
            }

            foreach (var overview in fileOverviews)
            {
                DetailsLayoutListViewItem listItem = new();

                if (overview.ObjectType == "tree")
                {
                    listItem.ObjectTypeIconGlyph = "\uE9A0";
                    listItem.ObjectTag = "tree/" + overview.FileName;
                }
                else
                {
                    listItem.ObjectTypeIconGlyph = "\uE996";
                    listItem.ObjectTag = "blob/" + overview.FileName;
                }

                listItem.ObjectName = overview.FileName;
                listItem.ObjectLatestCommitMessage = overview.CommitMessage.Split("\n")[0];
                listItem.ObjectUpdatedAtHumanized = overview.CommittedDate.Humanize();

                Items.Add(listItem);
            }

            var orderedByItemType = new ObservableCollection<DetailsLayoutListViewItem>(Items.OrderByDescending(x => x.ObjectTypeIconGlyph));
            Items.Clear();
            foreach (var orderedItem in orderedByItemType) Items.Add(orderedItem);

            ReadmeContentBlockViewModel readmeViewModel = new();
            readmeViewModel.Owner = ContextViewModel.Owner;
            readmeViewModel.RepoName = ContextViewModel.Name;
            ReadmeBlockViewModel = readmeViewModel;
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
