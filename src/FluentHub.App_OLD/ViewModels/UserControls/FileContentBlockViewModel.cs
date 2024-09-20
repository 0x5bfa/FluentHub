using ColorCode;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.Repositories;
using FluentHub.Octokit.Queries.Repositories;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Documents;
using System.IO;

namespace FluentHub.App.ViewModels.UserControls
{
	public class FileContentBlockViewModel : ObservableObject
	{
		public FileContentBlockViewModel(IMessenger messenger = null, ILogger logger = null)
		{
			_messenger = messenger;
			_logger = logger;
		}

		#region Fields and Properties
		private readonly ILogger _logger;
		private readonly IMessenger _messenger;

		private RepoContextViewModel contextViewModel;
		public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

		private Blob _blob;
		public Blob Blob { get => _blob; set => SetProperty(ref _blob, value); }

		private string _formattedFileDetails;
		public string FormattedFileDetails { get => _formattedFileDetails; set => SetProperty(ref _formattedFileDetails, value); }

		private string _formattedFileSize;
		public string FormattedFileSize { get => _formattedFileSize; set => SetProperty(ref _formattedFileSize, value); }

		private string _lineText;
		public string LineText { get => _lineText; set => SetProperty(ref _lineText, value); }
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

				Blob = content;

				FormattedFileSize = content.ByteSize.Bytes().ToString("#.##");

				if (string.IsNullOrEmpty(content.Text))
					return;

				var text = content.Text;
				int lines = text.Length - text.Replace("\n", "").Length;
				int notsloc = text.Length - text.Replace("\n\n", "").Length;
				var sloc = lines - notsloc;

				FormattedFileDetails = $"{lines} lines ({sloc} sloc)";
				LineText = "";

				for (int i = 1; i <= lines + 1; i++)
					LineText += $"{i}\n";

				LineText = LineText.TrimEnd('\n');

				textBlock.Blocks.Clear();
				var formatter = new RichTextBlockFormatter(ThemeHelpers.RootTheme);

				string extension = Path.GetExtension(ContextViewModel.Path.Remove(0, 1));
				if (string.IsNullOrEmpty(extension) is false)
				{
					extension = extension.TrimStart('.');
				}

				var fileType = FileTypeHelpers.GetFileTypeStringId(extension);

				if (string.IsNullOrEmpty(fileType) is false && string.IsNullOrEmpty(extension) is false)
				{
					ILanguage lang = Languages.FindById(fileType);
					formatter.FormatRichTextBlock(text, lang, textBlock);
				}
				else
				{
					Paragraph paragraph = new();
					paragraph.Inlines.Add(new Run() { Text = text });
					textBlock.Blocks.Add(paragraph);
				}
			}
			catch (Exception ex)
			{
				_logger?.Error(nameof(LoadRepositoryOneContentAsync), ex);
				throw;
			}
		}
	}
}
