using FluentHub.Models.Items;
using FluentHub.Octokit.Queries.Repository;
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
        private CommonRepoViewModel commonRepoViewModel;
        public CommonRepoViewModel CommonRepoViewModel
        {
            get => commonRepoViewModel;
            set => SetProperty(ref commonRepoViewModel, value);
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
