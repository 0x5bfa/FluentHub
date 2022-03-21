using Humanizer;
using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Users
{
    public class DiscussionsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<DiscussionButtonBlockViewModel> DiscussionItems { get; private set; } = new();

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        public async Task GetUserDiscussions(string login)
        {
            IsActive = true;

            DiscussionQueries queries = new();
            var items = await queries.GetOverviewAll(login);

            foreach (var item in items)
            {
                DiscussionButtonBlockViewModel viewModel = new();
                viewModel.NameWithOwner = item.Owner + " / " + item.Name + " #" + item.Number;
                viewModel.UpdatedAtHumanized = item.UpdatedAt.Humanize();
                viewModel.DiscussionItem = item;

                DiscussionItems.Add(viewModel);
            }

            IsActive = false;
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
