using FluentHub.App.Views.Repositories.Releases;
using FluentHub.Octokit.Queries.Repositories;
using System.Windows.Input;

namespace FluentHub.App.ViewModels.Repositories.Releases
{
	public class ReleasesViewModel : BaseViewModel
	{
		private Repository _repository;
		public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

		private readonly ObservableCollection<Release> _items;
		public ReadOnlyObservableCollection<Release> Items { get; }

		private Release _latestRelease;
		public Release LatestRelease { get => _latestRelease; set => SetProperty(ref _latestRelease, value); }

		public ICommand GoToReleasePageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryReleasesPageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryReleasesFurtherCommand { get; }

		public ReleasesViewModel() : base()
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText;
			Name = parameter.SecondaryText;

			_items = new();
			Items = new(_items);

			GoToReleasePageCommand = new RelayCommand<string>(ExecuteGoToReleasePageCommand);
			LoadRepositoryReleasesPageCommand = new AsyncRelayCommand(LoadRepositoryReleasesPageAsync);
			LoadRepositoryReleasesFurtherCommand = new AsyncRelayCommand(LoadRepositoryReleasesFurtherAsync);
		}

		private async Task LoadRepositoryReleasesPageAsync()
		{
			SetTabInformation("Releases", "Releases", "Repositories");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryReleasesPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryReleasesAsync);
				await LoadRepositoryReleasesAsync(Login, Name);

				SetTabInformation("Releases", "Releases");
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}

		private async Task LoadRepositoryReleasesAsync(string login, string name)
		{
			ReleaseQueries queries = new();

			var result = await queries.GetAllAsync(
				owner: login,
				name: name,
				first: 20);
			if (result.Response is null || result.PageInfo is null)
				return;

			_lastPageInfo = result.PageInfo;
			var items = (List<Release>)result.Response;

			if (items.Count != 0)
			{
				LatestRelease = items.FirstOrDefault();
			}

			_items.Clear();
			foreach (var item in items)
				_items.Add(item);
		}

		private async Task LoadRepositoryReleasesFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				ReleaseQueries queries = new();

				var result = await queries.GetAllAsync(
					owner: Login,
					name: Name,
					first: 20,
					after: _lastPageInfo.EndCursor);
				if (result.Response is null || result.PageInfo is null)
					return;

				_lastPageInfo = result.PageInfo;
				var items = (List<Release>)result.Response;

				foreach (var item in items)
					_items.Add(item);
			}
			catch (Exception ex)
			{
				TaskException = ex;
				IsTaskFaulted = true;
			}
			finally
			{
				SetLoadingProgress(false);
			}
		}

		public async Task LoadRepositoryAsync(string owner, string name)
		{
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}

		private void ExecuteGoToReleasePageCommand(string? tag)
		{
			SelectedTabViewItem.NavigationBar.Context = new()
			{
				PrimaryText = Login,
				SecondaryText = Name,
				Parameters = tag ?? string.Empty
			};

			_navigation.Navigate<ReleasePage>();
		}
	}
}
