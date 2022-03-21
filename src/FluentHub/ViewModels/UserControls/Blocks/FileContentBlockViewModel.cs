using FluentHub.Helpers;
using FluentHub.Octokit.Queries.Repository;
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

        private string formattedFileDetails;
        public string FormattedFileDetails { get => formattedFileDetails; set => SetProperty(ref formattedFileDetails, value); }

        private string formattedFileSize;
        public string FormattedFileSize { get => formattedFileSize; set => SetProperty(ref formattedFileSize, value); }

        private string lineText;
        public string LineText { get => lineText; set => SetProperty(ref lineText, value); }

        public async Task GetFileContent()
        {
            BlobQueries queries = new();
            var content = await queries.Get(
                CommonRepoViewModel.Name,
                CommonRepoViewModel.Owner,
                CommonRepoViewModel.BranchName,
                CommonRepoViewModel.Path);

            BlobContent = content.Item1;

            var lines = BlobDetailsHelpers.GetBlobActualLines(ref content.Item1);
            var sloc = BlobDetailsHelpers.GetBlobSloc(ref content.Item1);

            FormattedFileDetails = $"{lines} lines ({sloc} sloc)";
            FormattedFileSize = BlobDetailsHelpers.FormatSize(content.Item2);

            for (int i = 0; i < lines; i++)
            {
                LineText += $"{i}\n";
            }

            LineText += $"{lines}";
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
