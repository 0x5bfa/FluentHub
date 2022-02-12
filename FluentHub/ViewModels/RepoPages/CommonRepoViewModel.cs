using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.RepoPages
{
    public class CommonRepoViewModel : INotifyPropertyChanged
    {
        public long RepositoryId { get; set; }

        public string BranchName { get; set; } = "main";

        public string Path { get; set; } = "/";

        private bool isRootDir;
        public bool IsRootDir
        {
            get => isRootDir;
            set
            {
                if (value == true) IsFile = false;
                SetProperty(ref isRootDir, value);
            }
        }

        private bool isFile;
        public bool IsFile
        {
            get => isFile;
            set
            {
                if (value == true) IsRootDir = false;
                SetProperty(ref isFile, value);
            }
        }

        private bool isDir;
        public bool IsDir
        {
            get => isDir;
            set
            {
                if (value == true) IsFile = false;
                SetProperty(ref isDir, value);
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
