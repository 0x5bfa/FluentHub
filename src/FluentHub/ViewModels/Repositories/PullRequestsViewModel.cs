using Humanizer;
using FluentHub.Octokit.Queries.Repository;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Repositories
{
    public class PullRequestsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<PullButtonBlockViewModel> PullItems { get; private set; } = new();

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        public async Task GetRepoPRs(string name, string owner)
        {
            IsActive = true;

            PullRequestQueries queries = new();
            var items = await queries.GetOverviewAll(name, owner);

            foreach (var item in items)
            {
                PullButtonBlockViewModel viewModel = new();
                viewModel.PullItem = item;
                viewModel.NameWithOwner = item.Owner + " / " + item.Name + " #" + item.Number;
                viewModel.UpdatedAtHumanized = item.UpdatedAt.Humanize();

                PullItems.Add(viewModel);
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
