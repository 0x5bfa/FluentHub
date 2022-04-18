using FluentHub.Octokit.Queries.Organizations;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Organizations
{
    public class OverviewViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<RepoButtonBlockViewModel> OrgPinnedItems { get; private set; } = new();

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        public async Task GetPinnedRepos(string org)
        {
            IsActive = true;

            PinnedItemQueries queries = new();
            var items = await queries.GetAllAsync(org);

            if (items == null)
            {
                IsActive = false;
                return;
            }

            foreach (var item in items)
            {
                RepoButtonBlockViewModel viewModel = new();
                viewModel.Item = item;
                viewModel.DisplayDetails = false;
                viewModel.DisplayStarButton = false;

                OrgPinnedItems.Add(viewModel);
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
