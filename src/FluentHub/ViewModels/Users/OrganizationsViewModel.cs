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
    public class OrganizationsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<OrgButtonBlockViewModel> OrgItems { get; private set; } = new();

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

        public async Task GetUserOrganizations(string login)
        {
            IsActive = true;

            OrganizationQueries queries = new();
            var items = await queries.GetOverviewAll(login);

            foreach (var item in items)
            {
                OrgButtonBlockViewModel viewModel = new();
                viewModel.OrgItem = item;

                OrgItems.Add(viewModel);
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
