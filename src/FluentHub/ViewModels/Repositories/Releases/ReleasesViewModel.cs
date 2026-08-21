using FluentHub.Core.Queries.Repositories;
using FluentHub.Extensions;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using FluentHub.Views.Repositories.Releases;
using FluentHub.ViewModels.UserControls.Overview;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Windows.Input;
using FluentHub.Core.Contracts;

namespace FluentHub.ViewModels.Repositories.Releases
{
	public class ReleasesViewModel : BaseViewModel
	{
		private readonly ObservableCollection<Release> _items;
		public ReadOnlyObservableCollection<Release> Items { get; }

		private Release _latestRelease = default!;
		public Release LatestRelease { get => _latestRelease; set => SetProperty(ref _latestRelease, value); }

		public ICommand GoToReleasePageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryReleasesPageCommand { get; }
		public IAsyncRelayCommand LoadRepositoryReleasesFurtherCommand { get; }

		public ReleasesViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText ?? string.Empty;
			Name = parameter.SecondaryText ?? string.Empty;

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
			var queries = _gitHub.Repositories.Releases;

			var result = await queries.GetPageAsync(login, name, PageRequest.Forward(20));

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

			if (items.Any())
			{
				LatestRelease = items[0];
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
				var queries = _gitHub.Repositories.Releases;

				var result = await queries.GetPageAsync(
					Login,
					Name,
					PageRequest.Forward(20, _lastPageInfo.EndCursor));

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

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
			var queries = _gitHub.Repositories.Repositories;
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
