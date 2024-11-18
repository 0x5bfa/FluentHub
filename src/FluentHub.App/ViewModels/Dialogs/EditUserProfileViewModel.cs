using FluentHub.App.Utils;
using FluentHub.Octokit.Queries.Users;

namespace FluentHub.App.ViewModels.Dialogs
{
	public class EditUserProfileViewModel : ObservableObject
	{
		public EditUserProfileViewModel(IMessenger messenger = null, ILogger logger = null)
		{
			_logger = logger;
			_messenger = messenger;
		}

		#region Fields and Properties
		private readonly ILogger _logger;
		private readonly IMessenger _messenger;

		private string _login;
		public string Login { get => _login; set => SetProperty(ref _login, value); }

		private User _userInfo;
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
				UserQueries queries = new();
				var response = await queries.GetAsync(login);

				if (response == null) return;

				UserInfo = response;
			}
			catch (Exception ex)
			{
				// TODO: Log the exception
			}
		}

		public async Task UpdateUserAsync(string login)
		{
			if (!DataIsUpdated)
				return;
		}
	}
}
