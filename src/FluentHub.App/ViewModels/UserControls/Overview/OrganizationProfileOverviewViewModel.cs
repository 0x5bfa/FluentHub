namespace FluentHub.App.ViewModels.UserControls.Overview
{
	public class OrganizationProfileOverviewViewModel : ObservableObject
	{
		#region Fields and Properties
		private Organization _organization;
		public Organization Organization { get => _organization; set => SetProperty(ref _organization, value); }

		public static Organization StoredOrganization;

		private Uri _builtWebsiteUrl;
		public Uri BuiltWebsiteUrl { get => _builtWebsiteUrl; set => SetProperty(ref _builtWebsiteUrl, value); }

		private string _selectedTag;
		public string SelectedTag { get => _selectedTag; set => SetProperty(ref _selectedTag, value); }
		#endregion
	}
}
