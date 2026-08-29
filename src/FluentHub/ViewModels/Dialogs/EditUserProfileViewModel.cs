using FluentHub.Utils;
using FluentHub.Models;
using FluentHub.Core.Infrastructure.GitHub.Queries.Users;
using FluentHub.Core.Application.Models;

namespace FluentHub.ViewModels.Dialogs
{
	public class EditUserProfileViewModel : ObservableObject
	{
		private readonly IFluentHubGitHubClient _gitHub;

		public EditUserProfileViewModel(IFluentHubGitHubClient gitHub, ILogger? logger = null)
		{
			_gitHub = gitHub;
			_logger = logger;
		}

		#region Fields and Properties
		private readonly ILogger? _logger;

		private string _login = default!;
		public string Login { get => _login; set => SetProperty(ref _login, value); }

		private User _userInfo = default!;
		public User UserInfo
		{
			get => _userInfo;
			set
			{
				if (_userInfo != null)
				{
					DataIsUpdated = true;
				}

				SetProperty(ref _userInfo, value);
			}
		}

		private bool _dataIsUpdated;
		public bool DataIsUpdated { get => _dataIsUpdated; set => SetProperty(ref _dataIsUpdated, value); }
		#endregion

		public async Task LoadUserAsync(string login)
		{
			try
			{
				var queries = _gitHub.Users.Users;
				var response = await queries.GetAsync(login);

				if (response == null) return;

				UserInfo = response;
			}
			catch (Exception ex)
			{
				_logger?.Error(nameof(LoadUserAsync), ex);
			}
		}

		public async Task UpdateUserAsync(string login)
		{
			if (!DataIsUpdated)
				return;
		}
	}
}
