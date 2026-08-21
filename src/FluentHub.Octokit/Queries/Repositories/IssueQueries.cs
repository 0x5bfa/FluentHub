using FluentHub.Octokit.Clients;

namespace FluentHub.Octokit.Queries.Repositories
{
	public class IssueQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public IssueQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<PageResult<Issue>> GetPageAsync(
			string owner,
			string name,
			PageRequest page,
			OctokitGraphQLModel.IssueFilters? filterBy = null,
			IEnumerable<string>? labels = null,
			OctokitGraphQLModel.IssueOrder? orderBy = null,
			IEnumerable<OctokitGraphQLModel.IssueState>? states = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);

			orderBy ??= new()
			{
				Direction = OctokitGraphQLModel.OrderDirection.Desc,
				Field = OctokitGraphQLModel.IssueOrderField.CreatedAt
			};

			var query = new Query()
				.Repository(name, owner)
				.Issues(
					page.First,
					page.After,
					page.Last,
					page.Before,
					filterBy,
					labels is not null ? new OctokitGraphQLCore.Arg<IEnumerable<string>>(labels) : null!,
					orderBy,
					states is not null ? new OctokitGraphQLCore.Arg<IEnumerable<OctokitGraphQLModel.IssueState>>(states) : null!)
				.Select(connection => new IssueConnection
				{
					Edges = connection.Edges.Select(edge => (IssueEdge?)new IssueEdge
					{
						Node = edge.Node.Select(x => new Issue
						{
							Closed = x.Closed,
							Number = x.Number,
							Title = x.Title,
							UpdatedAt = x.UpdatedAt,
							UpdatedAtHumanized = x.UpdatedAt.ToRelativeTime(),

							Repository = x.Repository.Select(repo => new Repository
							{
								Name = repo.Name,

								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Id = owner.Id,
									Login = owner.Login,
								}).SingleOrDefault(),
							}).SingleOrDefault(),

							Comments = x.Comments(null, null, null, null, null).Select(comments => new IssueCommentConnection
							{
								TotalCount = comments.TotalCount,
							}).SingleOrDefault(),

							Labels = x.Labels(10, null, null, null, null).Select(labels => new LabelConnection
							{
								Nodes = labels.Nodes.Select(y => (Label?)new Label
								{
									Color = y.Color,
									Description = y.Description,
									Name = y.Name,
								}).ToList(),
							}).SingleOrDefault(),
						}).Single(),
					}).ToList(),

					PageInfo = new()
					{
						EndCursor = connection.PageInfo.EndCursor,
						HasNextPage = connection.PageInfo.HasNextPage,
						HasPreviousPage = connection.PageInfo.HasPreviousPage,
						StartCursor = connection.PageInfo.StartCursor,
					},
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return new PageResult<Issue>(
				response.Edges?
					.Where(x => x?.Node is not null)
					.Select(x => x!.Node!)
					.ToList() ?? [],
				response.PageInfo);
		}

		public async Task<Issue> GetAsync(string owner, string name, int number, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.Repository(name, owner)
				.Issue(number)
				.Select(x => new Issue
				{
					Body = x.Body,
					Closed = x.Closed,
					Id = x.Id,
					Number = x.Number,
					State = (IssueState)x.State,
					StateReason = x.StateReason == null ? null : (IssueStateReason?)x.StateReason.Value,
					Title = x.Title,
					UpdatedAt = x.UpdatedAt,
					ViewerCanClose = x.ViewerCanUpdate,
					ViewerCanLabel = x.ViewerCanUpdate,
					ViewerCanReopen = x.ViewerCanUpdate,
					ViewerCanSubscribe = x.ViewerCanSubscribe,
					ViewerCanUpdate = x.ViewerCanUpdate,
					ViewerSubscription = x.ViewerSubscription == null
						? null
						: (SubscriptionState?)x.ViewerSubscription.Value,

					Assignees = x.Assignees(6, null, null, null).Select(assignees => new UserConnection
					{
						Nodes = assignees.Nodes.Select(y => (User?)new User
						{
							AvatarUrl = y.AvatarUrl(500),
							Id = y.Id,
							Login = y.Login,
						})
						.ToList(),
					})
					.SingleOrDefault(),

					Comments = x.Comments(null, null, null, null, null).Select(comments => new IssueCommentConnection
					{
						TotalCount = comments.TotalCount,
					})
					.SingleOrDefault(),

					Labels = x.Labels(10, null, null, null, null).Select(labels => new LabelConnection
					{
						Nodes = labels.Nodes.Select(y => (Label?)new Label
						{
							Color = y.Color,
							Description = y.Description,
							Id = y.Id,
							Name = y.Name,
						})
						.ToList(),
					})
					.SingleOrDefault(),

					Milestone = x.Milestone.Select(y => new Milestone
					{
						Id = y.Id,
						Title = y.Title,
						ProgressPercentage = y.ProgressPercentage,
					})
					.SingleOrDefault(),

					Participants = x.Participants(6, null, null, null).Select(participants => new UserConnection
					{
						Nodes = participants.Nodes.Select(y => (User?)new User
						{
							AvatarUrl = y.AvatarUrl(500),
							Login = y.Login,
						})
						.ToList(),
					})
					.SingleOrDefault(),

					ProjectCards = x.ProjectCards(6, null, null, null, null).Select(projects => new ProjectCardConnection
					{
						Nodes = projects.Nodes.Select(y => (ProjectCard?)new ProjectCard
						{
							Note = y.Note,
						})
						.ToList(),
					})
					.SingleOrDefault(),

					Repository = x.Repository.Select(repo => new Repository
					{
						Name = repo.Name,

						Owner = repo.Owner.Select(owner => new RepositoryOwner
						{
							AvatarUrl = owner.AvatarUrl(500),
							Id = owner.Id,
							Login = owner.Login,
						})
						.SingleOrDefault(),
					})
					.SingleOrDefault(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
		}

		public async Task<IssueComment> GetBodyAsync(string owner, string name, int number, CancellationToken cancellationToken = default)
		{
			var query = new Query()
				.Repository(name, owner)
				.Issue(number)
				.Select(x => new IssueComment
				{
					AuthorAssociation = (CommentAuthorAssociation)x.AuthorAssociation,
					Body = x.Body,
					BodyHTML = x.BodyHTML,
					CreatedAt = x.CreatedAt,
					CreatedAtHumanized = x.CreatedAt.ToRelativeTime(),
					Id = x.Id,
					LastEditedAt = x.LastEditedAt,
					UpdatedAt = x.UpdatedAt,
					UpdatedAtHumanized = x.UpdatedAt.ToRelativeTime(),
					Url = x.Url,
					ViewerCanReact = x.ViewerCanReact,
					ViewerCanUpdate = x.ViewerCanUpdate,
					ViewerDidAuthor = x.ViewerDidAuthor,

					Author = x.Author.Select(author => new Actor
					{
						Login = author.Login,
						AvatarUrl = author.AvatarUrl(500),
					})
					.SingleOrDefault(),

					Reactions = x.Reactions(100, null, null, null, null, null).Select(reactions => new ReactionConnection
					{
						Nodes = reactions.Nodes.Select(reaction => (Reaction?)new Reaction
						{
							Content = (ReactionContent)reaction.Content,

							User = reaction.User.Select(user => new User
							{
								Login = user.Login,
							})
							.SingleOrDefault(),
						})
						.ToList(),
					})
					.SingleOrDefault(),

					ReactionGroups = x.ReactionGroups.Select(group => new ReactionGroup
					{
						Content = (ReactionContent)group.Content,
						ViewerHasReacted = group.ViewerHasReacted,
						Reactors = group.Reactors(null, null, null, null).Select(reactors => new ReactorConnection
						{
							TotalCount = reactors.TotalCount,
						}).SingleOrDefault(),
					}).ToList(),
				})
				.Compile();

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response;
		}

		public async Task<List<Issue>> GetPinnedAllAsync(string owner, string name, CancellationToken cancellationToken = default)
		{
			#region query
			var query = new Query()
				.Repository(name, owner)
				.PinnedIssues(3, null, null, null)
				.Nodes
				.Select(x => new Issue
				{
					Closed = x.Issue.Closed,
					Number = x.Issue.Number,
					Title = x.Issue.Title,
					UpdatedAt = x.Issue.UpdatedAt,

					Repository = x.Repository.Select(repo => new Repository
					{
						Name = repo.Name,

						Owner = repo.Owner.Select(owner => new RepositoryOwner
						{
							AvatarUrl = owner.AvatarUrl(500),
							Id = owner.Id,
							Login = owner.Login,
						})
						.SingleOrDefault(),
					})
					.SingleOrDefault(),

					Comments = x.Issue.Comments(null, null, null, null, null).Select(comments => new IssueCommentConnection
					{
						TotalCount = comments.TotalCount,
					})
					.SingleOrDefault(),

					Labels = x.Issue.Labels(10, null, null, null, null).Select(labels => new LabelConnection
					{
						Nodes = labels.Nodes.Select(y => (Label?)new Label
						{
							Color = y.Color,
							Description = y.Description,
							Name = y.Name,
						})
						.ToList(),
					})
					.SingleOrDefault(),
				})
				.Compile();
			#endregion

			var response = await _gitHub.RunGraphQLAsync(query, cancellationToken);

			return response.ToList();
		}
	}
}
