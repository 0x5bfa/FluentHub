using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Repositories
{
    public class IssueViewModel : INotifyPropertyChanged
    {
        private string repoOwner;
        public string RepoOwner { get => repoOwner; set => SetProperty(ref repoOwner, value); }

        private string repoOwnerAvatar;
        public string RepoOwnerAvatar { get => repoOwnerAvatar; set => SetProperty(ref repoOwnerAvatar, value); }

        private string repoName;
        public string RepoName { get => repoName; set => SetProperty(ref repoName, value); }

        public void Get()
        {

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
