namespace FluentHub.Octokit.Queries.Repositories
{
	public class RepositoryQueries
	{
		public async Task<Repository> GetAsync(string owner, string name)
		{
			OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.IssueState>> issueState =
				new(new OctokitGraphQLModel.IssueState[] {
					OctokitGraphQLModel.IssueState.Open
				});
			OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.PullRequestState>> pullRequestState =
				new(new OctokitGraphQLModel.PullRequestState[] {
					OctokitGraphQLModel.PullRequestState.Open
				});

			var query = new Query()
				.Repository(name, owner)
				.Select(x => new Repository
				{
					Name = x.Name,
					Description = x.Description,
					StargazerCount = x.StargazerCount,
					ForkCount = x.ForkCount,
					IsFork = x.IsFork,
					IsInOrganization = x.IsInOrganization,
					ViewerHasStarred = x.ViewerHasStarred,
					UpdatedAt = x.UpdatedAt,

					LicenseInfo = x.LicenseInfo.Select(licenseInfo => new License
					{
						Name = licenseInfo.Name,
					})
					.SingleOrDefault(),

					Issues = x.Issues(null, null, null, null, null, null, null, issueState).Select(issues => new IssueConnection
					{
						TotalCount = issues.TotalCount
					})
					.Single(),

					PullRequests = x.PullRequests(null, null, null, null, null, null, null, null, pullRequestState).Select(issues => new PullRequestConnection
					{
						TotalCount = issues.TotalCount
					})
					.Single(),

					Owner = x.Owner.Select(owner => new RepositoryOwner
					{
						AvatarUrl = owner.AvatarUrl(500),
						Id = owner.Id,
						Login = owner.Login,
					})
					.Single(),

					PrimaryLanguage = x.PrimaryLanguage.Select(y => new Language
					{
						Name = y.Name,
						Color = y.Color,
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await App.Connection.Run(query);

			return response;
		}

		public async Task<Repository> GetDetailsAsync(string owner, string name)
		{
			OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.IssueState>> issueState =
				new(new OctokitGraphQLModel.IssueState[] {
					OctokitGraphQLModel.IssueState.Open
				});
			OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.PullRequestState>> pullRequestState =
				new(new OctokitGraphQLModel.PullRequestState[] {
					OctokitGraphQLModel.PullRequestState.Open
				});

			var query = new Query()
				.Repository(owner: owner, name: name)
				.Select(x => new Repository
				{
					HomepageUrl = x.HomepageUrl,
					ForkingAllowed = x.ForkingAllowed,
					HasIssuesEnabled = x.HasIssuesEnabled,
					HasProjectsEnabled = x.HasProjectsEnabled,
					IsArchived = x.IsArchived,
					IsEmpty = x.IsEmpty,
					IsPrivate = x.IsPrivate,
					IsTemplate = x.IsTemplate,
					ViewerSubscription = (SubscriptionState)x.ViewerSubscription,
					Name = x.Name,
					Description = x.Description,
					StargazerCount = x.StargazerCount,
					ForkCount = x.ForkCount,
					IsFork = x.IsFork,
					IsInOrganization = x.IsInOrganization,
					ViewerHasStarred = x.ViewerHasStarred,
					UpdatedAt = x.UpdatedAt,

					LicenseInfo = x.LicenseInfo.Select(licenseInfo => new License
					{
						Name = licenseInfo.Name,
					})
					.SingleOrDefault(),

					DefaultBranchRef = x.DefaultBranchRef.Select(defaultbranchref => new Ref
					{
						Name = defaultbranchref.Name,
					})
					.SingleOrDefault(),

					Watchers = x.Watchers(null, null, null, null).Select(watchers => new UserConnection
					{
						TotalCount = watchers.TotalCount,
					})
					.Single(),

					Releases = x.Releases(null, null, null, null, null).Select(releases => new ReleaseConnection
					{
						TotalCount = releases.TotalCount,
					})
					.Single(),

					Issues = x.Issues(null, null, null, null, null, null, null, issueState).Select(issues => new IssueConnection
					{
						TotalCount = issues.TotalCount
					})
					.Single(),

					PullRequests = x.PullRequests(null, null, null, null, null, null, null, null, pullRequestState).Select(issues => new PullRequestConnection
					{
						TotalCount = issues.TotalCount
					})
					.Single(),

					Owner = x.Owner.Select(owner => new RepositoryOwner
					{
						AvatarUrl = owner.AvatarUrl(500),
						Id = owner.Id,
						Login = owner.Login,
					})
					.Single(),

					LatestRelease = x.Releases(null, null, 1, null, null).Nodes.Select(release => new Release
					{
						DescriptionHTML = release.DescriptionHTML,
						IsDraft = release.IsDraft,
						IsLatest = release.IsLatest,
						IsPrerelease = release.IsPrerelease,
						Name = release.Name,
						PublishedAt = release.PublishedAt,

						Author = release.Author.Select(author => new User
						{
							Login = author.Login,
							AvatarUrl = author.AvatarUrl(500),
						})
						.Single(),
					})
					.ToList().FirstOrDefault(),

					Languages = x.Languages(10, null, null, null, null).Select(langConection => new LanguageConnection
					{
						Nodes = langConection.Nodes.Select(lang => new Language
						{
							Color = lang.Color,
							Name = lang.Name,
						})
						.ToList(),
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await App.Connection.Run(query);

			return response;
		}

		public async Task<CustomRepositoryResponseForCodePage> GetCustomDetailsAsync(string owner, string name)
		{
			OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.IssueState>> issueState =
				new(new OctokitGraphQLModel.IssueState[] {
					OctokitGraphQLModel.IssueState.Open
				});
			OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.PullRequestState>> pullRequestState =
				new(new OctokitGraphQLModel.PullRequestState[] {
					OctokitGraphQLModel.PullRequestState.Open
				});

			var query = new Query()
				.Select(root => new
				{
					first = root.Repository(name, owner, null).Select(x => new Repository
					{
						HomepageUrl = x.HomepageUrl,
						ForkingAllowed = x.ForkingAllowed,
						HasIssuesEnabled = x.HasIssuesEnabled,
						HasProjectsEnabled = x.HasProjectsEnabled,
						IsArchived = x.IsArchived,
						IsEmpty = x.IsEmpty,
						IsPrivate = x.IsPrivate,
						IsTemplate = x.IsTemplate,
						ViewerSubscription = (SubscriptionState)x.ViewerSubscription,
						Name = x.Name,
						Description = x.Description,
						StargazerCount = x.StargazerCount,
						ForkCount = x.ForkCount,
						IsFork = x.IsFork,
						IsInOrganization = x.IsInOrganization,
						ViewerHasStarred = x.ViewerHasStarred,
						UpdatedAt = x.UpdatedAt,

						LicenseInfo = x.LicenseInfo.Select(licenseInfo => new License
						{
							Name = licenseInfo.Name,
						})
						.SingleOrDefault(),

						DefaultBranchRef = x.DefaultBranchRef.Select(defaultbranchref => new Ref
						{
							Name = defaultbranchref.Name,
						})
						.SingleOrDefault(),

						Watchers = x.Watchers(null, null, null, null).Select(watchers => new UserConnection
						{
							TotalCount = watchers.TotalCount,
						})
						.Single(),

						Releases = x.Releases(null, null, null, null, null).Select(releases => new ReleaseConnection
						{
							TotalCount = releases.TotalCount,
						})
						.Single(),

						Issues = x.Issues(null, null, null, null, null, null, null, issueState).Select(issues => new IssueConnection
						{
							TotalCount = issues.TotalCount
						})
						.Single(),

						PullRequests = x.PullRequests(null, null, null, null, null, null, null, null, pullRequestState).Select(issues => new PullRequestConnection
						{
							TotalCount = issues.TotalCount
						})
						.Single(),

						Owner = x.Owner.Select(owner => new RepositoryOwner
						{
							AvatarUrl = owner.AvatarUrl(500),
							Id = owner.Id,
							Login = owner.Login,
						})
						.Single(),

						LatestRelease = x.Releases(1, null, null, null, null).Nodes.Select(release => new Release
						{
							DescriptionHTML = release.DescriptionHTML,
							IsDraft = release.IsDraft,
							IsLatest = release.IsLatest,
							IsPrerelease = release.IsPrerelease,
							Name = release.Name,
							PublishedAt = release.PublishedAt,
							PublishedAtHumanized = release.PublishedAt.Humanize(null, null),

							Author = release.Author.Select(author => new User
							{
								Login = author.Login,
								AvatarUrl = author.AvatarUrl(500),
							})
							.Single(),
						})
						.ToList().FirstOrDefault(),

						Languages = x.Languages(10, null, null, null, null).Select(langConection => new LanguageConnection
						{
							Nodes = langConection.Nodes.Select(lang => new Language
							{
								Color = lang.Color,
								Name = lang.Name,
							})
							.ToList(),
						})
					.SingleOrDefault(),
					})
					.SingleOrDefault(),

					second = root.Repository(name, owner, null).Select(y => new
					{
						Heads = y.Refs("refs/heads/", null, null, null, null, null, null, null).Select(ref1 => new RefConnection
						{
							TotalCount = ref1.TotalCount,
						})
						.SingleOrDefault(),

						Tags = y.Refs("refs/tags/", null, null, null, null, null, null, null).Select(ref2 => new RefConnection
						{
							TotalCount = ref2.TotalCount,
						})
						.SingleOrDefault(),
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await App.Connection.Run(query);

			return new CustomRepositoryResponseForCodePage()
			{
				Repository = response.first,
				BranchesTotalCount = response.second.Heads.TotalCount,
				TagsTotalCount = response.second.Tags.TotalCount,
			};
		}

		public async Task<(int, int)> GetBranchAndTagCountAsync(string owner, string name)
		{
			var query = new Query()
				.Repository(owner: owner, name: name)
				.Select(x => new
				{
					HeadRefsCount = x.Refs("refs/heads/", null, null, null, null, null, null, null).TotalCount,
					TagCount = x.Refs("refs/tags/", null, null, null, null, null, null, null).TotalCount,
				})
				.Compile();

			var response = await App.Connection.Run(query);

			return (response.HeadRefsCount, response.TagCount);
		}

		public async Task<List<string>> GetBranchNameAllAsync(string owner, string name)
		{
			#region query
			var query = new Query()
				.Repository(name, owner)
				.Refs(refPrefix: "refs/", first: 30, query: "heads/")
				.Select(x => new
				{
					BranchNames = x.Nodes.Select(y => new
					{
						y.Name,
					})
					.ToList()
				})
				.Compile();
			#endregion

			var response = await App.Connection.Run(query);

			List<string> branchNames = new();
			foreach (var branch in response.BranchNames)
			{
				// Delete "heads/"
				branchNames.Add(branch.Name.Remove(0, 6));
			}

			return branchNames;
		}

		public async Task<string> GetReadmeHtml(string owner, string name, string branch, string theme, string index)
		{
			string bodyHtml;

			try
			{
				bodyHtml = await App.Client.Repository.Content.GetReadmeHtml(owner, name);

				string missedPath = "https://raw.githubusercontent.com/" + owner + "/" + name + "/" + branch + "/";

				MarkdownQueries markdown = new();
				var html = markdown.GetHtml(index, bodyHtml, missedPath, theme, true);

				return html;
			}
			catch (global::Octokit.NotFoundException)
			{
				return null;
			}
		}
	}
}
