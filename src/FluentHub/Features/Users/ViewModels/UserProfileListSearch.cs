namespace FluentHub.Features.Users.ViewModels
{
	internal static class UserProfileListSearch
	{
		public static bool Matches(User user, string? searchText)
			=> string.IsNullOrWhiteSpace(searchText)
			|| Contains(user.Login, searchText)
			|| Contains(user.Name, searchText)
			|| Contains(user.Bio, searchText);

		public static bool Matches(Organization organization, string? searchText)
			=> string.IsNullOrWhiteSpace(searchText)
			|| Contains(organization.Login, searchText)
			|| Contains(organization.Name, searchText)
			|| Contains(organization.Description, searchText);

		private static bool Contains(string? value, string searchText)
			=> value?.Contains(searchText.Trim(), StringComparison.OrdinalIgnoreCase) == true;
	}
}
