using Humanizer;
using FluentHub.OctokitEx.Queries.Repository;
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
    public class IssuesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<IssueButtonBlockViewModel> IssueItems { get; private set; } = new();

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        public async Task GetRepoIssues(string name, string owner)
        {
            IsActive = true;

            EnumIssueOverviewsQueries queries = new();
            var items = await queries.Get(name, owner);

            foreach (var item in items)
            {
                IssueButtonBlockViewModel viewModel = new();
                viewModel.IssueItem = item;
                viewModel.NameWithOwner = item.Owner + " / " + item.Name + " #" + item.Number;
                viewModel.UpdatedAtHumanized = item.UpdatedAt.Humanize();

                IssueItems.Add(viewModel);
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
