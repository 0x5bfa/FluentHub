using FluentHub.Utils;
using FluentHub.Core.Application;
using FluentHub.Core.Application.Models;
using FluentHub.Core.Application.Models;

namespace FluentHub.Shared.Dialogs.ViewModels
{
	public class EditPinnedRepositoriesDialogViewModel : ObservableObject
	{
		private readonly IFluentHubGitHubClient _gitHub;

		public EditPinnedRepositoriesDialogViewModel(
			IFluentHubGitHubClient gitHub,
			IMessenger? messenger = null,
			ILogger? logger = null)
		{
			_gitHub = gitHub;
			_logger = logger;
			_messenger = messenger;

			_pinnableItems = new();
			PinnableItems = new(_pinnableItems);
		}

		#region Fields and Properties
		private readonly ILogger? _logger;
		private readonly IMessenger? _messenger;

		private string _login = default!;
		public string Login { get => _login; set => SetProperty(ref _login, value); }

		private readonly ObservableCollection<PinnableRepositoryItem> _pinnableItems;
		public ReadOnlyObservableCollection<PinnableRepositoryItem> PinnableItems { get; }
		#endregion

		public async Task LoadPinnableAndPinnedRepositoriesAsync(CancellationToken cancellationToken = default)
		{
			if (Login == null)
			{
				throw new ArgumentNullException();
			}

			var queries = _gitHub.Users.PinnedItems;
			(List<Repository> pinnables, List<Repository> pinneds) = await queries.GetAllPinnableAndPinnedItemsAsync(Login, cancellationToken);

			_pinnableItems.Clear();
			foreach (var item in PinnedRepositoryService.CreateItems(pinnables, pinneds))
				_pinnableItems.Add(item);
		}
	}
}
