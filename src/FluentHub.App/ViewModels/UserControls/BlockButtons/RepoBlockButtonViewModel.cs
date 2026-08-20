// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Models;
using FluentHub.Octokit.Mutations;
using System.Windows.Input;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.App.ViewModels.UserControls.BlockButtons
{
	public class RepoBlockButtonViewModel : ObservableObject
	{
		private readonly IFluentHubGitHubClient _gitHub;

		private Repository _item = default!;
		public Repository Repository { get => _item; set => SetProperty(ref _item, value); }

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

			var navBar = _navigation.TabView.SelectedItem.NavigationBar;
			navBar.Context = new()
			{
				PrimaryText = Repository.Owner.Login,
				SecondaryText = Repository.Name,
			};

			if (App.AppSettings.UseDetailsView)
				_navigation.Navigate<Views.Repositories.Code.DetailsLayoutView>();
			else
				_navigation.Navigate<Views.Repositories.Code.TreeLayoutView>();
		}

		private async Task AddStarToRepositoryAsync()
		{
			try
			{
				if (Repository.ViewerHasStarred)
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

				Repository.ViewerHasStarred = !Repository.ViewerHasStarred;
			}
			catch (Exception ex)
			{
				var messenger = Ioc.Default.GetRequiredService<IMessenger>();
				messenger.Send(new UserNotificationMessage("Something went wrong", ex.Message, UserNotificationType.Error));
			}
		}
	}
}
