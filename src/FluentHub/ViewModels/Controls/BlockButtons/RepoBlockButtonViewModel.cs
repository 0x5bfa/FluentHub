// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Models;
using FluentHub.Core.Infrastructure.GitHub.Mutations;
using System.Windows.Input;
using FluentHub.Core.Application.Models;
using FluentHub.Utils;

namespace FluentHub.ViewModels.Controls.BlockButtons
{
	public class RepoBlockButtonViewModel : ObservableObject
	{
		private readonly IFluentHubGitHubClient _gitHub;

		private Repository _item = default!;
		public Repository Repository
		{
			get => _item;
			set
			{
				if (SetProperty(ref _item, value))
					NotifyStarStateChanged();
			}
		}

		public bool ViewerHasStarred
			=> Repository?.ViewerHasStarred ?? false;

		public int StargazerCount
			=> Repository?.StargazerCount ?? 0;

		private bool _displayDetails;
		public bool DisplayDetails { get => _displayDetails; set => SetProperty(ref _displayDetails, value); }

		private bool _displayStarButton;
		public bool DisplayStarButton { get => _displayStarButton; set => SetProperty(ref _displayStarButton, value); }

		public ICommand GoRepositoryCommand { get; private set; }
		public ICommand AddStarToRepositoryCommand { get; private set; }

		public RepoBlockButtonViewModel(IFluentHubGitHubClient gitHub)
		{
			_gitHub = gitHub;
			GoRepositoryCommand = new RelayCommand(GoRepository);
			AddStarToRepositoryCommand = new AsyncRelayCommand(AddStarToRepositoryAsync);
		}

		private void GoRepository()
		{
			var _navigation = Ioc.Default.GetRequiredService<INavigationService>();

			var layout = App.AppSettings.UseDetailsView
				? RepositoryCodeLayout.Details
				: RepositoryCodeLayout.Tree;
			_ = _navigation.NavigateAsync(
				new RepositoryCodeRoute(
					new RepositorySlug(Repository.Owner.Login, Repository.Name),
					Layout: layout));
		}

		private async Task AddStarToRepositoryAsync()
		{
			try
			{
				var wasStarred = Repository.ViewerHasStarred;
				if (wasStarred)
				{
					// Remove star
					var removeStarMutation = _gitHub.Mutations.RemoveStar;

					await removeStarMutation.ExecuteAsync(Repository.Id);
				}
				else
				{
					// Add star
					var addStarMutation = _gitHub.Mutations.AddStar;

					await addStarMutation.ExecuteAsync(Repository.Id);
				}

				Repository.ViewerHasStarred = !wasStarred;
				Repository.StargazerCount = Math.Max(0, Repository.StargazerCount + (wasStarred ? -1 : 1));
				NotifyStarStateChanged();
				await InvalidateRepositoryCacheAsync();
			}
			catch (Exception ex)
			{
				var messenger = Ioc.Default.GetRequiredService<IMessenger>();
				messenger.Send(new UserNotificationMessage("Something went wrong", ex.Message, UserNotificationType.Error));
			}
		}

		private async Task InvalidateRepositoryCacheAsync()
		{
			try
			{
				await _gitHub.Repositories.Repositories.InvalidateAsync(
					Repository.Owner.Login,
					Repository.Name);
			}
			catch (Exception ex)
			{
				Ioc.Default.GetService<ILogger>()?.Warn(
					"Failed to invalidate repository cache: {0}",
					ex.Message);
			}
		}

		private void NotifyStarStateChanged()
		{
			OnPropertyChanged(nameof(ViewerHasStarred));
			OnPropertyChanged(nameof(StargazerCount));
		}
	}
}
