using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using FluentHub.Core.Application.Models;

namespace FluentHub.ViewModels.Controls.Overview
{
	public class AppSettingsOverviewViewModel : ObservableObject
	{
		#region Fields and Properties
		private User _user = default!;
		public User User { get => _user; set => SetProperty(ref _user, value); }

		public static User StoredUser = default!;

		private string _selectedTag = default!;
		public string SelectedTag { get => _selectedTag; set => SetProperty(ref _selectedTag, value); }
		#endregion
	}
}
