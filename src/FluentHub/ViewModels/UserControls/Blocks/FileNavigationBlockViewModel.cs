using FluentHub.Models.Items;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.Repositories;
using Humanizer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class FileNavigationBlockViewModel : INotifyPropertyChanged
    {
        private RepoContextViewModel contextViewModel;
        public RepoContextViewModel ContextViewModel
        {
            get => contextViewModel;
            set => SetProperty(ref contextViewModel, value);
        }

        private List<string> branchNames;
        public List<string> BranchNames { get => branchNames; set => SetProperty(ref branchNames, value); }

        public async Task LoadBranchNames()
        {
            // temp workaround
            BranchNames.Add(ContextViewModel.Repository.DefaultBranchName);
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
