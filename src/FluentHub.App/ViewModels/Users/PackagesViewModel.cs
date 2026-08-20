// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.Queries.Users;
using FluentHub.App.Models;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.Octokit.Models.v4;

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

		public PackagesViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
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
			var queries = _gitHub.Users.Packages;

			var result = await queries.GetPageAsync(login, PageRequest.Forward(20));

			_lastPageInfo = result.PageInfo;
			var items = result.Items;

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
				var queries = _gitHub.Users.Packages;

				var result = await queries.GetPageAsync(Login, PageRequest.Forward(20, _lastPageInfo.EndCursor));

				_lastPageInfo = result.PageInfo;
				var items = result.Items;

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
