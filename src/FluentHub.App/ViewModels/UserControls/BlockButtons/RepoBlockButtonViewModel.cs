// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Octokit.Mutations;
using System.Windows.Input;

namespace FluentHub.App.ViewModels.UserControls.BlockButtons
{
	public class RepoBlockButtonViewModel : ObservableObject
	{
		private Repository _item;
		public Repository Repository { get => _item; set => SetProperty(ref _item, value); }

		private bool _displayDetails;
		public bool DisplayDetails { get => _displayDetails; set => SetProperty(ref _displayDetails, value); }

		private bool _displayStarButton;
		public bool DisplayStarButton { get => _displayStarButton; set => SetProperty(ref _displayStarButton, value); }

		public ICommand GoRepositoryCommand { get; private set; }
		public ICommand AddStarToRepositoryCommand { get; private set; }

		public RepoBlockButtonViewModel()
		{
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
					var removeStarMutation = new RemoveStarMutation();

					await removeStarMutation.Execute(Repository.Id);
				}
				else
				{
					// Add star
					var addStarMutation = new AddStarMutation();

					await addStarMutation.Execute(Repository.Id);
				}

				Repository.ViewerHasStarred = !Repository.ViewerHasStarred;
			}
			catch (Exception ex)
			{

			}
		}
	}
}
