namespace FluentHub.App.ViewModels.UserControls.Overview
{
	public class AppSettingsOverviewViewModel : ObservableObject
	{
		#region Fields and Properties
		private User _user;
		public User User { get => _user; set => SetProperty(ref _user, value); }

		public static User StoredUser;

		private string _selectedTag;
		public string SelectedTag { get => _selectedTag; set => SetProperty(ref _selectedTag, value); }
		#endregion
	}
}
