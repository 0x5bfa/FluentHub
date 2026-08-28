using FluentHub.Core.Infrastructure.GitHub.Queries.Repositories;
using FluentHub.Extensions;
using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using FluentHub.Shared.Controls.ViewModels.Overview;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Core.Application.Models;

namespace FluentHub.Features.Repositories.ViewModels.Releases
{
	public class ReleaseViewModel : BaseViewModel
	{
		private string _tagName = default!;
		public string TagName{ get => _tagName; set => SetProperty(ref _tagName, value); }

		private Release _singleRelease = default!;
		public Release SingleRelease { get => _singleRelease; set => SetProperty(ref _singleRelease, value); }

		public IAsyncRelayCommand LoadRepositoryReleasePageCommand { get; }

		public ReleaseViewModel(IFluentHubGitHubClient gitHub, ScreenViewModelDependencies dependencies) : base(gitHub, dependencies)
		{
			TagName = CurrentRoute is RepositoryReleaseRoute release
				? release.Tag
				: string.Empty;

			LoadRepositoryReleasePageCommand = new AsyncRelayCommand(LoadRepositoryReleasePageAsync);
		}

		private async Task LoadRepositoryReleasePageAsync()
		{
			SetTabInformation("Release", "Release", "Repositories");
			SetLoadingProgress(true);
			InitializeNodePagingInfo();

			_currentTaskingMethodName = nameof(LoadRepositoryReleasePageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositorySingleReleaseAsync);
				await LoadRepositorySingleReleaseAsync(Login, Name, TagName);

				SetTabInformation("Release", "Release");
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

		private async Task LoadRepositorySingleReleaseAsync(string login, string name, string tagName)
		{
			 var queries = _gitHub.Repositories.Releases;
			 var response = await queries.GetAsync(login, name, tagName);

			 SingleRelease = response;
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			var queries = _gitHub.Repositories.Repositories;
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
