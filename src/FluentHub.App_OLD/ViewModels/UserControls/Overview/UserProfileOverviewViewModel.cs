namespace FluentHub.App.ViewModels.UserControls.Overview
{
	public class UserProfileOverviewViewModel : ObservableObject
	{
		#region Fields and Properties
		private User _user;
		public User User { get => _user; set => SetProperty(ref _user, value); }

		public static User StoredUser;

		private Uri _builtWebsiteUrl;
		public Uri BuiltWebsiteUrl { get => _builtWebsiteUrl; set => SetProperty(ref _builtWebsiteUrl, value); }

		private string _selectedTag;
		public string SelectedTag { get => _selectedTag; set => SetProperty(ref _selectedTag, value); }
		#endregion

		public string UserLocationUrl => $"https://www.bing.com/maps?q={User.Location}";
	}
}
