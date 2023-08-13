namespace FluentHub.Octokit.Queries.Organizations
{
	public class OrganizationQueries
	{
		public async Task<Organization> GetAsync(string org)
		{
			var query = new Query()
				.Organization(org)
				.Select(x => new Organization
				{
					AvatarUrl = x.AvatarUrl(500),
					Description = x.Description,
					Email = x.Email,
					Id = x.Id,
					IsVerified = x.IsVerified,
					Location = x.Location,
					Login = x.Login,
					Name = x.Name,
					TwitterUsername = x.TwitterUsername,
					Url = x.Url,
					ViewerCanChangePinnedItems = x.ViewerCanChangePinnedItems,
					ViewerCanSponsor = x.ViewerCanSponsor,
					ViewerIsAMember = x.ViewerIsAMember,
					ViewerIsFollowing = x.ViewerIsFollowing,
					ViewerIsSponsoring = x.ViewerIsSponsoring,
					WebsiteUrl = x.WebsiteUrl,
				})
				.Compile();

			var response = await App.Connection.Run(query);

			return response;
		}
	}
}
