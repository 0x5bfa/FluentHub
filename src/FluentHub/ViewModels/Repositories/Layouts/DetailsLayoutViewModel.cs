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
        public ObservableCollection<DetailsLayoutListViewItem> Items { get; private set; } = new();

        private CommonRepoViewModel commonRepoViewModel;
        public CommonRepoViewModel CommonRepoViewModel { get => commonRepoViewModel; set => SetProperty(ref commonRepoViewModel, value); }

        public async Task EnumRepositoryContents()
        {
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
