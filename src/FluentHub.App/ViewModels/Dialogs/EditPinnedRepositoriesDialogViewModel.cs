using FluentHub.App.Utils;
using FluentHub.App.Models;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.App.ViewModels.Dialogs
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

		private readonly ObservableCollection<PinnableRepository> _pinnableItems;
		public ReadOnlyObservableCollection<PinnableRepository> PinnableItems { get; }
		#endregion

		public async Task LoadPinnableAndPinnedRepositoriesAsync(CancellationToken cancellationToken = default)
		{
			if (Login == null)
			{
				throw new ArgumentNullException();
			}

			var queries = _gitHub.Users.PinnedItems;
			(List<Repository> pinnables, List<Repository> pinneds) = await queries.GetAllPinnableAndPinnedItemsAsync(Login, cancellationToken);

			foreach (var item in pinnables)
			{
				var pinnableRepo = new PinnableRepository()
				{
					PinnableItem = item,
				};

				var result = pinneds.Find(x => x.NameWithOwner == item.NameWithOwner);

				if (result != default(Repository))
				{
					pinnableRepo.IsPinned = true;
				}

				_pinnableItems.Add(pinnableRepo);
			}
		}
	}
}
