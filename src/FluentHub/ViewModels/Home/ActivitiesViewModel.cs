using Humanizer;
using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.Blocks;
using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Home
{
    public class ActivitiesViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<ActivityBlockViewModel> EventItems { get; set; } = new();

        public async Task GetAllActivityForCurrent(string login)
        {
            IsActive = true;

            ActivityQueries queries = new();
            var response = await queries.GetAll(login);

            foreach (var res in response)
            {
                ActivityBlockViewModel viewModel = new();
                viewModel.Payload = res;
                viewModel.UpdatedAtHumanized = res.CreatedAt.Humanize();

                EventItems.Add(viewModel);
            }

            IsActive = false;
        }

        private bool isActive;
        public bool IsActive { get => isActive; set => SetProperty(ref isActive, value); }

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
