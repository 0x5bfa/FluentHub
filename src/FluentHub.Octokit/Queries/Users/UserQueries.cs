namespace FluentHub.Octokit.Queries.Users
{
	public class UserQueries
	{
		public async Task<User> GetAsync(string login)
		{
			var query = new Query()
				.User(login)
				.Select(x => new User
				{
					AvatarUrl = x.AvatarUrl(500),
					Bio = x.Bio,
					Company = x.Company,
					Email = x.Email,
					IsCampusExpert = x.IsCampusExpert,
					IsBountyHunter = x.IsBountyHunter,
					IsDeveloperProgramMember = x.IsDeveloperProgramMember,
					IsEmployee = x.IsEmployee,
					IsGitHubStar = x.IsGitHubStar,
					IsViewer = x.IsViewer,
					Location = x.Location,
					Login = x.Login,
					Name = x.Name,
					TwitterUsername = x.TwitterUsername,
					ViewerIsFollowing = x.ViewerIsFollowing,
					WebsiteUrl = x.WebsiteUrl,

					Followers = x.Followers(null, null, null, null).Select(followers => new FollowerConnection
					{
						TotalCount = followers.TotalCount,
					})
					.SingleOrDefault(),

					Following = x.Following(null, null, null, null).Select(following => new FollowingConnection
					{
						TotalCount = following.TotalCount,
					})
					.SingleOrDefault(),

					Status = x.Status.Select(status => new UserStatus
					{
						Emoji = status.Emoji,
						Message = status.Message,
						IndicatesLimitedAvailability = status.IndicatesLimitedAvailability,
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await App.Connection.Run(query);

			return response;
		}

		public async Task<string> GetViewerLogin()
		{
			var query = new Query()
				.Viewer
				.Select(x => new
				{
					x.Login,
				})
				.Compile();

			var response = await App.Connection.Run(query);

			return response.Login;
		}
	}
}
