using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Extensions;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.App.ViewModels.Repositories.Releases
{
	public class ReleaseViewModel : BaseViewModel
	{
		private Repository _repository = default!;
		public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

		private string _tagName = default!;
		public string TagName{ get => _tagName; set => SetProperty(ref _tagName, value); }

		private Release _singleRelease = default!;
		public Release SingleRelease { get => _singleRelease; set => SetProperty(ref _singleRelease, value); }

		public IAsyncRelayCommand LoadRepositoryReleasePageCommand { get; }

		public ReleaseViewModel(IFluentHubGitHubClient gitHub) : base(gitHub)
		{
			var parameter = _navigation.TabView.SelectedItem.NavigationBar.Context;
			Login = parameter.PrimaryText;
			Name = parameter.SecondaryText;
			TagName = parameter.Parameters as string;

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
