using ColorCode;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Uwp.ViewModels.Repositories;
using System.IO;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Documents;

namespace FluentHub.Uwp.ViewModels.UserControls.Blocks
{
    public class FileContentBlockViewModel : ObservableObject
    {
        public FileContentBlockViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;
        }

        #region Fields and Properties
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

        public async Task LoadRepositoryOneContentAsync(RichTextBlock textBlock)
        {
            try
            {
                BlobQueries queries = new();
                var content = await queries.GetAsync(
                    ContextViewModel.Repository.Name,
                    ContextViewModel.Repository.Owner.Login,
                    ContextViewModel.BranchName,
                    ContextViewModel.Path);

                BlobContent = "";
                FormattedFileDetails = "";
                FormattedFileSize = "";
                LineText = "";

                var text = BlobContent = content.Text;

                var lines = BlobDetailsHelpers.GetBlobActualLines(ref text);
                var sloc = BlobDetailsHelpers.GetBlobSloc(ref text);

                FormattedFileDetails = $"{lines} lines ({sloc} sloc)";
                FormattedFileSize = BlobDetailsHelpers.FormatSize(content.ByteSize);

                for (int i = 1; i <= lines + 1; i++)
                    LineText += $"{i}\n";

                LineText = LineText.TrimEnd('\n');

                textBlock.Blocks.Clear();
                var formatter = new RichTextBlockFormatter(ThemeHelper.ActualTheme);

                string extension = Path.GetExtension(ContextViewModel.Path.Remove(0, 1));
                if (string.IsNullOrEmpty(extension) is false)
                {
                    extension = extension.TrimStart('.');
                }

                var fileType = FileTypeHelper.GetFileTypeStringId(extension);

                if (string.IsNullOrEmpty(fileType) is false && string.IsNullOrEmpty(extension) is false)
                {
                    ILanguage lang = Languages.FindById(fileType);
                    formatter.FormatRichTextBlock(BlobContent, lang, textBlock);
                }
                else
                {
                    Paragraph paragraph = new();
                    Run run = new()
                    {
                        Text = BlobContent
                    };

                    paragraph.Inlines.Add(run);
                    textBlock.Blocks.Add(paragraph);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryOneContentAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
    }
}
