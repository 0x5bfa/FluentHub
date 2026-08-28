using FluentHub.Helpers;
using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Utils;
using FluentHub.Core.Application.Models;

namespace FluentHub.Shared.Controls.ViewModels.Overview
{
	public class OrganizationProfileOverviewViewModel : ObservableObject
	{
		#region Fields and Properties
		private Organization _organization = default!;
		public Organization Organization { get => _organization; set => SetProperty(ref _organization, value); }

		public static Organization StoredOrganization = default!;

		private Uri _builtWebsiteUrl = default!;
		public Uri BuiltWebsiteUrl { get => _builtWebsiteUrl; set => SetProperty(ref _builtWebsiteUrl, value); }

		private string _selectedTag = default!;
		public string SelectedTag { get => _selectedTag; set => SetProperty(ref _selectedTag, value); }
		#endregion
	}
}
