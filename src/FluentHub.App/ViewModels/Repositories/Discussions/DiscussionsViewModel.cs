using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.Utils;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels.Repositories.Discussions
{
	public class DiscussionsViewModel : BaseViewModel
	{
		private readonly ObservableCollection<DiscussionBlockButtonViewModel> _items;
		public ReadOnlyObservableCollection<DiscussionBlockButtonViewModel> Items { get; }

		public IAsyncRelayCommand LoadRepositoryDiscussionsPageCommand { get; }

		public DiscussionsViewModel() : base()
		{
			_items = new();
			Items = new(_items);

			LoadRepositoryDiscussionsPageCommand = new AsyncRelayCommand(LoadRepositoryDiscussionsPageAsync);
		}

		private async Task LoadRepositoryDiscussionsPageAsync()
		{
			SetTabInformation("Discussions", "Discussions", "Discussions");
			SetLoadingProgress(true);

			_currentTaskingMethodName = nameof(LoadRepositoryDiscussionsPageAsync);

			try
			{
				_currentTaskingMethodName = nameof(LoadRepositoryAsync);
				await LoadRepositoryAsync(Login, Name);

				_currentTaskingMethodName = nameof(LoadRepositoryDiscussionsAsync);
				await LoadRepositoryDiscussionsAsync(Login, Name);

				SetTabInformation($"Discussions \u2022 {Login}/{Name}", $"Discussions \u2022 {Login}/{Name}");
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

		private async Task LoadRepositoryDiscussionsAsync(string owner, string name)
		{
			DiscussionQueries queries = new();
			var items = await queries.GetAllAsync(owner, name);

			_items.Clear();
			foreach (var item in items)
			{
				DiscussionBlockButtonViewModel viewmodel = new()
				{
					Item = item,
				};

				_items.Add(viewmodel);
			}
		}

		private async Task LoadRepositoryAsync(string owner, string name)
		{
			RepositoryQueries queries = new();
			Repository = await queries.GetDetailsAsync(owner, name);
		}
	}
}
