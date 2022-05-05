using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Repositories
{
    public class RepoContextViewModel : INotifyPropertyChanged
    {
        public RepoContextViewModel()
        {
            BranchName = "main";
            Path = "/";
        }

        public Octokit.Models.Repository Repository { get; set; }

        private string _name;
        public string Name { get => _name; set => SetProperty(ref _name, value); }

        private string _owner;
        public string Owner { get => _owner; set => SetProperty(ref _owner, value); }

        private string _branchName;
        public string BranchName { get => _branchName; set => SetProperty(ref _branchName, value); }

        private string _path;
        public string Path { get => _path; set => SetProperty(ref _path, value); }

        private bool isRootDir;
        public bool IsRootDir
        {
            get => isRootDir;
            set
            {
                if (value) IsFile = false;
                SetProperty(ref isRootDir, value);
            }
        }

        private bool isFile;
        public bool IsFile
        {
            get => isFile;
            set
            {
                if (value)
                {
                    IsRootDir = false;
                    IsDir = false;
                    IsSubDir = false;
                }
                SetProperty(ref isFile, value);
            }
        }

        private bool isDir;
        public bool IsDir
        {
            get => isDir;
            set
            {
                if (value) IsFile = false;
                SetProperty(ref isDir, value);
            }
        }

        private bool isSubDir;
        public bool IsSubDir
        {
            get => isSubDir;
            set
            {
                if (value)
                {
                    IsFile = false;
                    IsRootDir = false;
                }
                SetProperty(ref isSubDir, value);
            }
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
