using FluentHub.Models.Items;
using FluentHub.OctokitEx.Queries.Repository;
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
        private ObservableCollection<DetailsLayoutListViewItem> items = new ObservableCollection<DetailsLayoutListViewItem>();
        public ObservableCollection<DetailsLayoutListViewItem> Items
        {
            get => items;
            private set => SetProperty(ref items, value);
        }

        private CommonRepoViewModel commonRepoViewModel;
        public CommonRepoViewModel CommonRepoViewModel { get => commonRepoViewModel; set => SetProperty(ref commonRepoViewModel, value); }

        public async Task<int> EnumRepositoryContents()
        {
            #region outdated
            /*
            var contents = await App.Client.Repository.Content.GetAllContents(CommonRepoViewModel.RepositoryId, CommonRepoViewModel.Path);
            LatestCommitQueries queries = new();

            CommonRepoViewModel.IsDir = true;

            if (CommonRepoViewModel.Path == "/")
            {
                CommonRepoViewModel.IsRootDir = true;
            }

            foreach (var item in contents)
            {
                DetailsLayoutListViewItem listItem = new();

                if (item.Type == Octokit.ContentType.Dir)
                {
                    listItem.ObjectTypeIconGlyph = "\uE9A0";
                    listItem.ObjectTag = "dir/" + item.Name;
                }
                else
                {
                    listItem.ObjectTypeIconGlyph = "\uE996";
                    listItem.ObjectTag = "file/" + item.Name;
                }

                listItem.ObjectName = item.Name;

                Items.Add(listItem);
            }

            for (int i = 0; i < Items.Count(); i++)
            {
                // using await inside for is deprecated.
                var commitOverview
                    = await queries.Get(
                    CommonRepoViewModel.Name,
                    CommonRepoViewModel.Owner,
                    CommonRepoViewModel.BranchName,
                    contents[i].Path);

                Items[i].ObjectLatestCommitMessage = commitOverview.CommitMessage.Split("\n")[0];

                Items[i].ObjectUpdatedAtHumanized = commitOverview.CommittedDate.Humanize();

            }
            */
            #endregion

            LatestCommitQueries queries = new();
            var fileOverviews = await queries.EnumFilesAndItsLatestCommit(CommonRepoViewModel.Name,
                    CommonRepoViewModel.Owner,
                    CommonRepoViewModel.BranchName,
                    CommonRepoViewModel.Path);

            CommonRepoViewModel.IsDir = true;

            if (CommonRepoViewModel.Path == "/")
            {
                CommonRepoViewModel.IsRootDir = true;
            }

            foreach (var overview in fileOverviews)
            {
                DetailsLayoutListViewItem listItem = new();

                // object's icon
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

                // object's name
                listItem.ObjectName = overview.FileName;

                // object's commit message headline
                listItem.ObjectLatestCommitMessage = overview.CommitMessage.Split("\n")[0];

                // object's committed date
                listItem.ObjectUpdatedAtHumanized = overview.CommittedDate.Humanize();

                Items.Add(listItem);
            }

            Items = new ObservableCollection<DetailsLayoutListViewItem>(Items.OrderByDescending(x => x.ObjectTypeIconGlyph));

            return Items.Count();
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
