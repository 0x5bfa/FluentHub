using FluentHub.Models.Items;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.RepoPages.Layouts
{
    public class DetailsLayoutViewModel : INotifyPropertyChanged
    {
        private long _repositoryId;
        public long RepositoryId { get => _repositoryId; set => SetProperty(ref _repositoryId, value); }

        private ObservableCollection<DetailsLayoutListViewItem> items = new ObservableCollection<DetailsLayoutListViewItem>();
        public ObservableCollection<DetailsLayoutListViewItem> Items
        {
            get => items;
            private set => SetProperty(ref items, value);
        }

        private CommonRepoViewModel commonRepoViewModel;
        public CommonRepoViewModel CommonRepoViewModel { get => commonRepoViewModel; set => commonRepoViewModel = value; }

        public async Task EnumRepositoryContents()
        {
            var contents = await App.Client.Repository.Content.GetAllContents(CommonRepoViewModel.RepositoryId, CommonRepoViewModel.Path);

            foreach (var item in contents)
            {
                DetailsLayoutListViewItem listItem = new DetailsLayoutListViewItem();

                if (item.Type == Octokit.ContentType.Dir)
                {
                    listItem.ObjectTypeIconGlyph = "\uE9A0";
                }
                else
                {
                    listItem.ObjectTypeIconGlyph = "\uE996";
                }

                listItem.ObjectName = item.Name;

                Octokit.CommitRequest request = new Octokit.CommitRequest();
                request.Path = item.Path;

                var commit = await App.Client.Repository.Commit.GetAll(CommonRepoViewModel.RepositoryId, request);

                listItem.ObjectLatestCommitMessage = commit[0].Commit.Message.Split("\n")[0];

                listItem.ObjectUpdatedAtHumanized = commit[0].Commit.Author.Date.Humanize();

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
