using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using FluentHub.Octokit.Models.v4;

namespace FluentHub.ViewModels.UserControls.Overview
{
	public class UserProfileOverviewViewModel : ObservableObject
	{
		#region Fields and Properties
		private User _user = default!;
		public User User { get => _user; set => SetProperty(ref _user, value); }

		public static User StoredUser = default!;

		private Uri _builtWebsiteUrl = default!;
		public Uri BuiltWebsiteUrl { get => _builtWebsiteUrl; set => SetProperty(ref _builtWebsiteUrl, value); }

		private string _selectedTag = default!;
		public string SelectedTag { get => _selectedTag; set => SetProperty(ref _selectedTag, value); }
		#endregion
	}
}
