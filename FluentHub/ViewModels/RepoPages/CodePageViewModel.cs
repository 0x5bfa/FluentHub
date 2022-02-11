using Humanizer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace FluentHub.ViewModels.RepoPages
{
    public class DetailsLayoutModel
    {
        public string ObjectTypeIconGlyph { get; set; }

        public string ObjectName { get; set; }

        public string ObjectLatestCommitMessage { get; set; }

        public string ObjectUpdatedAtHumanized { get; set; }
    }

    public class CodePageViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<DetailsLayoutModel> _items = new ObservableCollection<DetailsLayoutModel>();
        public ObservableCollection<DetailsLayoutModel> Items
        {
            get => _items;
            private set
            {
                _items = value;
                NotifyPropertyChanged(nameof(Items));
            }
        }

        public async Task EnumRepositoryContents(long repoId)
        {
            var contents = await App.Client.Repository.Content.GetAllContents(repoId);

            foreach(var item in contents)
            {
                DetailsLayoutModel listItem = new DetailsLayoutModel();

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

                var commit = await App.Client.Repository.Commit.GetAll(repoId, request);

                listItem.ObjectLatestCommitMessage = commit[0].Commit.Message.Split("\n")[0];

                listItem.ObjectUpdatedAtHumanized = commit[0].Commit.Author.Date.Humanize();

                Items.Add(listItem);
            }


            Items = new ObservableCollection<DetailsLayoutModel>(Items.OrderByDescending(x => x.ObjectTypeIconGlyph));

        }

        public async Task EnumRepositoryBranches(long repoId)
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

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
