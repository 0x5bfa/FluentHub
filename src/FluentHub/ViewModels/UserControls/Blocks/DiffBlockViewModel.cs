using FluentHub.Octokit.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class DiffBlockViewModel : INotifyPropertyChanged
    {
        private ChangedFile _changedFile;
        public ChangedFile ChangedFile { get => _changedFile; set => SetProperty(ref _changedFile, value); }

        public void ParseDiffPatchString()
        {
            // TODO: FluentHub original perser here
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
