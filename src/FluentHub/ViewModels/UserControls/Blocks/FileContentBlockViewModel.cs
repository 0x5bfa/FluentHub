using FluentHub.OctokitEx.Queries.Repository;
using FluentHub.ViewModels.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class FileContentBlockViewModel : INotifyPropertyChanged
    {
        private CommonRepoViewModel commonRepoViewModel;
        public CommonRepoViewModel CommonRepoViewModel
        {
            get => commonRepoViewModel;
            set => SetProperty(ref commonRepoViewModel, value);
        }

        private string blobContent;
        public string BlobContent { get => blobContent; set => SetProperty(ref blobContent, value); }

        public async Task GetFileContent()
        {
            BlobQueries queries = new();
            var content = await queries.GetBlob(
                CommonRepoViewModel.Name,
                CommonRepoViewModel.Owner,
                CommonRepoViewModel.BranchName,
                CommonRepoViewModel.Path);

            BlobContent = content;
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
