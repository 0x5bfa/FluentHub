using Octokit.GraphQL.Core;

using FluentHub.Core.Clients;

namespace FluentHub.Core.Queries.Users
{
	public class PullRequestQueries
	{
		private readonly IGitHubApiClient _gitHub;

		public PullRequestQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;
		public async Task<PageResult<PullRequest>> GetPageAsync(
			string login,
			PageRequest page,
			string? baseRefName = null,
			string? headRefName = null,
			IEnumerable<string>? labels = null,
			OctokitGraphQLModel.IssueOrder? orderBy = null,
			IEnumerable<OctokitGraphQLModel.PullRequestState>? states = null,
			CancellationToken cancellationToken = default)
		{
			ArgumentNullException.ThrowIfNull(page);

			orderBy ??= new()
			{
				Direction = OctokitGraphQLModel.OrderDirection.Desc,
				Field = OctokitGraphQLModel.IssueOrderField.CreatedAt
			};
			states ??= [OctokitGraphQLModel.PullRequestState.Open];

			var query = new Query()
				.User(login)
				.PullRequests(
					page.First,
					page.After,
					page.Last,
					page.Before,
					baseRefName,
					headRefName,
					labels is null ? null! : new Arg<IEnumerable<string>>(labels),
					orderBy,
					new Arg<IEnumerable<OctokitGraphQLModel.PullRequestState>>(states))
				.Select(connection => new PullRequestConnection
				{
					Edges = connection.Edges.Select(edge => (PullRequestEdge?)new PullRequestEdge
					{
						Node = edge.Node.Select(x => new PullRequest
						{
							BaseRefName = x.BaseRefName,
							Closed = x.Closed,
							HeadRefName = x.HeadRefName,
							IsDraft = x.IsDraft,
							Merged = x.Merged,
							Number = x.Number,
							Title = x.Title,
							UpdatedAt = x.UpdatedAt,

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

							HeadRepository = x.HeadRepository.Select(repo => new Repository
							{
								Name = repo.Name,

								Owner = repo.Owner.Select(owner => new RepositoryOwner
								{
									AvatarUrl = owner.AvatarUrl(500),
									Login = owner.Login,
								})
								.SingleOrDefault(),
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
									Name = y.Name,
								})
								.ToList(),
							})
							.SingleOrDefault(),

							Reviews = x.Reviews(null, null, 1, null, null, null).Select(reviews => new PullRequestReviewConnection
							{
								Nodes = reviews.Nodes.Select(y => (PullRequestReview?)new PullRequestReview
								{
									State = (PullRequestReviewState)y.State,
								})
								.ToList().DefaultIfEmpty().ToList(),
							})
							.SingleOrDefault(),

							Commits = x.Commits(null, null, 1, null).Select(commits => new PullRequestCommitConnection
							{
								Nodes = commits.Nodes.Select(y => (PullRequestCommit?)new PullRequestCommit
								{
									Commit = y.Commit.Select(commit => new Commit
									{
										StatusCheckRollup = commit.StatusCheckRollup.Select(rollup => new StatusCheckRollup
										{
											State = (StatusState)rollup.State,
										})
										.SingleOrDefault(),
									})
									.SingleOrDefault(),
								})
								.ToList().DefaultIfEmpty().ToList(),
							})
							.SingleOrDefault(),
						}).Single()
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

			return new PageResult<PullRequest>(
				response.Edges?
					.Where(x => x?.Node is not null)
					.Select(x => x!.Node!)
					.ToList() ?? [],
				response.PageInfo);
		}
	}
}
