// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Queries.Users;

namespace FluentHub.App.ViewModels.Users
{
	public class PackagesViewModel : BaseViewModel
	{
		private bool _AsViewer;
		public bool AsViewer { get => _AsViewer; set => SetProperty(ref _AsViewer, value); }

		private readonly ObservableCollection<PackageBlockButtonViewModel> _packages;
		public ReadOnlyObservableCollection<PackageBlockButtonViewModel> Packages { get; }

		public IAsyncRelayCommand LoadUserPackagesPageCommand { get; }
		public IAsyncRelayCommand LoadUserPackagesFurtherCommand { get; }

		public PackagesViewModel() : base()
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			if (parameter.AsViewer)
			{
				var currentTabItem = _navigation.TabView.SelectedItem;
				currentTabItem.NavigationBar.PageKind = NavigationPageKind.None;

				AsViewer = true;
			}

			_packages = new();
			Packages = new(_packages);

			LoadUserPackagesPageCommand = new AsyncRelayCommand(LoadUserPackagesPageAsync);
			LoadUserPackagesFurtherCommand = new AsyncRelayCommand(LoadUserPackagesFurtherAsync);
		}

		private async Task LoadUserPackagesPageAsync()
		{
			SetTabInformation("Packages", "Packages", "Packages");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadUserPackagesPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadUserAsync);
				await LoadUserAsync(Login);

				_currentTaskingMethodName = nameof(LoadUserPackagesAsync);
				await LoadUserPackagesAsync(Login);

				SetTabInformation("Packages", "Packages");

				if (Packages.Count == 0)
					IsEmpty = true;
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

		private async Task LoadUserPackagesAsync(string login)
		{
			PackageQueries queries = new();

			var result = await queries.GetAllAsync(login, 20);
			if (result.Response is null || result.PageInfo is null)
				return;

			_lastPageInfo = result.PageInfo;
			var items = (List<Package>)result.Response;

			_packages.Clear();
			foreach (var item in items)
			{
				PackageBlockButtonViewModel viewModel = new()
				{
					Item = item,
				};

				_packages.Add(viewModel);
			}
		}

		private async Task LoadUserPackagesFurtherAsync()
		{
			if (!_lastPageInfo.HasNextPage)
				return;

			SetLoadingProgress(true);

			try
			{
				PackageQueries queries = new();

				var result = await queries.GetAllAsync(Login, 20, _lastPageInfo.EndCursor);
				if (result.Response is null || result.PageInfo is null)
					return;

				_lastPageInfo = result.PageInfo;
				var items = (List<Package>)result.Response;

				foreach (var item in items)
				{
					PackageBlockButtonViewModel viewModel = new()
					{
						Item = item,
					};

					_packages.Add(viewModel);
				}
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
	}
}
