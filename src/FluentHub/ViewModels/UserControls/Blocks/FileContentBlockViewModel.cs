using ColorCode;
using FluentHub.Core;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.Repositories;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class FileContentBlockViewModel : ObservableObject
    {
        #region constructor
        public FileContentBlockViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;
        }
        #endregion

        #region fields
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private string _blobContent;
        public string BlobContent { get => _blobContent; set => SetProperty(ref _blobContent, value); }

        private string _formattedFileDetails;
        public string FormattedFileDetails { get => _formattedFileDetails; set => SetProperty(ref _formattedFileDetails, value); }

        private string _formattedFileSize;
        public string FormattedFileSize { get => _formattedFileSize; set => SetProperty(ref _formattedFileSize, value); }

        private string _lineText;
        public string LineText { get => _lineText; set => SetProperty(ref _lineText, value); }

        private RepoContextViewModel contextViewModel;
        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }
        #endregion

        #region methods
        public async Task LoadContentsAsync(RichTextBlock textBlock)
        {
            try
            {
                BlobQueries queries = new();
                var content = await queries.GetAsync(
                    ContextViewModel.Name,
                    ContextViewModel.Owner,
                    ContextViewModel.BranchName,
                    ContextViewModel.Path);

                BlobContent = "";
                FormattedFileDetails = "";
                FormattedFileSize = "";
                LineText = "";

                BlobContent = content.Item1;

                var lines = BlobDetailsHelpers.GetBlobActualLines(ref content.Item1);
                var sloc = BlobDetailsHelpers.GetBlobSloc(ref content.Item1);

                FormattedFileDetails = $"{lines} lines ({sloc} sloc)";
                FormattedFileSize = BlobDetailsHelpers.FormatSize(content.Item2);

                for (int i = 1; i <= lines + 1; i++)
                    LineText += $"{i}\n";

                LineText = LineText.TrimEnd('\n');

                textBlock.Blocks.Clear();
                var formatter = new RichTextBlockFormatter(ThemeHelper.ActualTheme);

                string extension = Path.GetExtension(ContextViewModel.Path.Remove(0, 1));
                if (!string.IsNullOrEmpty(extension))
                {
                    extension = extension.TrimStart('.');
                }

                var fileType = FileTypeHelper.GetFileTypeStringId(extension);

                if (!string.IsNullOrEmpty(fileType) && !string.IsNullOrEmpty(extension))
                {
                    ILanguage lang = Languages.FindById(fileType);
                    formatter.FormatRichTextBlock(BlobContent, lang, textBlock);
                }
                else
                {
                    Paragraph paragraph = new();
                    Run run = new() { Text = BlobContent };
                    paragraph.Inlines.Add(run);
                    textBlock.Blocks.Add(paragraph);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error("LoadBlobContentBlockAsync", ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
        #endregion
    }
}
