using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Extensions;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.Views.Repositories.Releases;
using FluentHub.App.ViewModels.UserControls.Overview;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
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

		public ReleasesViewModel() : base()
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText;
			Name = parameter.SecondaryText;

			_items = new();
			Items = new(_items);

			GoToReleasePageCommand = new RelayCommand<string>(ExecuteGoToReleasePageCommand);
			LoadRepositoryReleasesPageCommand = new AsyncRelayCommand(LoadRepositoryReleasesPageAsync);
		}

		private async Task LoadRepositoryReleasesPageAsync()
		{
			SetTabInformation("Releases", "Releases", "Repositories");

			_messenger?.Send(new TaskStateMessaging(TaskStatusType.IsStarted));
			IsTaskFaulted = false;

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

				_logger?.Error(_currentTaskingMethodName, ex);
			}
			finally
			{
				_messenger?.Send(new TaskStateMessaging(IsTaskFaulted ? TaskStatusType.IsFaulted : TaskStatusType.IsCompletedSuccessfully));
			}
		}

		private async Task LoadRepositoryReleasesAsync(string login, string name)
		{
			ReleaseQueries queries = new();
			var items = await queries.GetAllAsync(login, name);

			if (items.Any())
			{
				LatestRelease = items.FirstOrDefault();
			}

			_items.Clear();
			foreach (var item in items) _items.Add(item);
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
