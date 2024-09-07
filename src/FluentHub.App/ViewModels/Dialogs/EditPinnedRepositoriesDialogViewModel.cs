using FluentHub.App.Models;
using FluentHub.App.Utils;

namespace FluentHub.App.ViewModels.Dialogs
{
	public class EditPinnedRepositoriesDialogViewModel : ObservableObject
	{
		public EditPinnedRepositoriesDialogViewModel(IMessenger messenger = null, ILogger logger = null)
		{
			_logger = logger;
			_messenger = messenger;

			_pinnableItems = new();
			PinnableItems = new(_pinnableItems);
		}

		#region Fields and Properties
		private readonly ILogger _logger;
		private readonly IMessenger _messenger;

		private string _login;
		public string Login { get => _login; set => SetProperty(ref _login, value); }

		private readonly ObservableCollection<PinnableRepository> _pinnableItems;
		public ReadOnlyObservableCollection<PinnableRepository> PinnableItems { get; }
		#endregion

		public async Task LoadPinnableAndPinnedRepositories()
		{
			if (Login == null)
			{
				throw new ArgumentNullException();
			}

			var queries = new Octokit.Queries.Users.PinnedItemQueries();
			(List<Repository> pinnables, List<Repository> pinneds) = await queries.GetAllPinnableAndPinnedItems(Login);

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
