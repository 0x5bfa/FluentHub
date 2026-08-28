// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

using System.Windows.Input;
using FluentHub.Core.Application.Models;

namespace FluentHub.ViewModels.Controls.Overview
{
	public class RepositoryOverviewViewModel : ObservableObject
	{
		private string _repositoryOwnerLogin = default!;
		public string RepositoryOwnerLogin { get => _repositoryOwnerLogin; set => SetProperty(ref _repositoryOwnerLogin, value); }

		private string _repositoryName = default!;
		public string RepositoryName { get => _repositoryName; set => SetProperty(ref _repositoryName, value); }

		private string _selectedTag = default!;
		public string SelectedTag { get => _selectedTag; set => SetProperty(ref _selectedTag, value); }

		private Repository _repository = default!;
		public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

		public static Repository StoredRepository = default!;

		private string _viewerSubscriptionState = default!;
		public string ViewerSubscriptionState { get => _viewerSubscriptionState; set => SetProperty(ref _viewerSubscriptionState, value); }

		public ICommand GoOwnerProfileCommand { get; private set; }

		public RepositoryOverviewViewModel()
		{
			GoOwnerProfileCommand = new RelayCommand(GoOwnerProfile);
		}

		private void GoOwnerProfile()
		{
			var service = Ioc.Default.GetRequiredService<INavigationService>();

			if (Repository.IsInOrganization)
			{
				_ = service.NavigateAsync(new OrganizationRoute(Repository.Owner.Login));
			}
			else
			{
				_ = service.NavigateAsync(new UserRoute(Repository.Owner.Login));
			}
		}
	}
}
