using System.IO;
using GraphQL;
using FluentHub.Core.Clients;

namespace FluentHub.Core.Queries.Discussions
{
	internal sealed class DiscussionSearchQueries
	{
		private const string SearchQuery = """
			query($query: String!, $first: Int!, $after: String) {
			  search(query: $query, type: DISCUSSION, first: $first, after: $after) {
			    nodes {
			      ... on Discussion {
			        answerChosenAt
			        author { avatarUrl(size: 500) login }
			        closed
			        comments { totalCount }
			        createdAt
			        id
			        labels(first: 10) { nodes { color description name } }
			        locked
			        number
			        title
			        updatedAt
			        upvoteCount
			        url
			        viewerCanDelete
			        viewerDidAuthor
			        viewerHasUpvoted
			        category { emoji id name }
			        repository { name owner { avatarUrl(size: 500) id login } }
			      }
			    }
			    pageInfo { endCursor hasNextPage hasPreviousPage startCursor }
			  }
			}
			""";

		private const string LabelSearchQuery = """
			query($query: String!) {
			  search(query: $query, type: DISCUSSION, first: 100) {
			    nodes {
			      ... on Discussion { labels(first: 20) { nodes { name } } }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;

		public DiscussionSearchQueries(IGitHubApiClient gitHub)
			=> _gitHub = gitHub;

		public Task<PageResult<Discussion>> GetRepositoryPageAsync(
			string owner,
			string name,
			PageRequest page,
			DiscussionListFilters filters,
			CancellationToken cancellationToken)
			=> GetPageAsync(
				DiscussionSearchQueryBuilder.BuildForRepository(owner, name, filters),
				page,
				cancellationToken);

		public Task<PageResult<Discussion>> GetAuthorPageAsync(
			string login,
			PageRequest page,
			DiscussionListFilters filters,
			CancellationToken cancellationToken)
			=> GetPageAsync(
				DiscussionSearchQueryBuilder.BuildForAuthor(login, filters),
				page,
				cancellationToken);

		public Task<IReadOnlyList<string>> GetRepositoryLabelNamesAsync(
			string owner,
			string name,
			CancellationToken cancellationToken)
			=> GetLabelNamesAsync(
				DiscussionSearchQueryBuilder.BuildForRepository(
					owner,
					name,
					new DiscussionListFilters { State = DiscussionStateFilter.All }),
				cancellationToken);

		public Task<IReadOnlyList<string>> GetAuthorLabelNamesAsync(
			string login,
			CancellationToken cancellationToken)
			=> GetLabelNamesAsync(
				DiscussionSearchQueryBuilder.BuildForAuthor(
					login,
					new DiscussionListFilters { State = DiscussionStateFilter.All }),
				cancellationToken);

		private async Task<PageResult<Discussion>> GetPageAsync(
			string searchText,
			PageRequest page,
			CancellationToken cancellationToken)
		{
			ArgumentNullException.ThrowIfNull(page);
			if (page.First is not int count)
				throw new NotSupportedException("Discussion search only supports forward pagination.");

			var request = new GraphQLRequest
			{
				Query = SearchQuery,
				Variables = new
				{
					query = searchText,
					first = Math.Min(count, 100),
					after = page.After,
				},
			};
			var response = await _gitHub.SendGraphQLAsync<SearchResponse<DiscussionNode>>(request, cancellationToken);
			ThrowIfErrors(response.Errors);
			var connection = response.Data?.Search
				?? throw new InvalidDataException("GitHub returned an incomplete discussion search response.");

			return new PageResult<Discussion>(
				connection.Nodes.Where(node => node is not null).Select(node => MapDiscussion(node!)).ToList(),
				connection.PageInfo);
		}

		private async Task<IReadOnlyList<string>> GetLabelNamesAsync(
			string searchText,
			CancellationToken cancellationToken)
		{
			var request = new GraphQLRequest
			{
				Query = LabelSearchQuery,
				Variables = new { query = searchText },
			};
			var response = await _gitHub.SendGraphQLAsync<SearchResponse<DiscussionLabelNode>>(request, cancellationToken);
			ThrowIfErrors(response.Errors);

			return response.Data?.Search?.Nodes
				.Where(node => node is not null)
				.SelectMany(node => node!.Labels.Nodes)
				.Where(label => !string.IsNullOrWhiteSpace(label?.Name))
				.Select(label => label!.Name)
				.Distinct(StringComparer.OrdinalIgnoreCase)
				.OrderBy(label => label, StringComparer.OrdinalIgnoreCase)
				.ToList() ?? [];
		}

		private static void ThrowIfErrors(GraphQLError[]? errors)
		{
			if (errors is { Length: > 0 })
				throw new InvalidOperationException(string.Join("; ", errors.Select(error => error.Message)));
		}

		private static Discussion MapDiscussion(DiscussionNode node)
			=> new()
			{
				AnswerChosenAt = node.AnswerChosenAt,
				Author = node.Author is null ? null : new Actor
				{
					AvatarUrl = node.Author.AvatarUrl ?? string.Empty,
					Login = node.Author.Login,
				},
				Category = new DiscussionCategory
				{
					Emoji = node.Category.Emoji,
					Id = new ID(node.Category.Id),
					Name = node.Category.Name,
				},
				Closed = node.Closed,
				Comments = new DiscussionCommentConnection { TotalCount = node.Comments.TotalCount },
				CreatedAt = node.CreatedAt,
				CreatedAtHumanized = node.CreatedAt.ToRelativeTime(),
				Id = new ID(node.Id),
				Labels = new LabelConnection
				{
					Nodes = node.Labels.Nodes
						.Where(label => label is not null)
						.Select(label => (Label?)new Label
						{
							Color = label!.Color,
							Description = label.Description,
							Name = label.Name,
						})
						.ToList(),
				},
				Locked = node.Locked,
				Number = node.Number,
				Repository = new Repository
				{
					Name = node.Repository.Name,
					Owner = new RepositoryOwner
					{
						AvatarUrl = node.Repository.Owner.AvatarUrl,
						Id = new ID(node.Repository.Owner.Id),
						Login = node.Repository.Owner.Login,
					},
				},
				Title = node.Title,
				UpdatedAt = node.UpdatedAt,
				UpdatedAtHumanized = node.UpdatedAt.ToRelativeTime(),
				UpvoteCount = node.UpvoteCount,
				Url = node.Url,
				ViewerCanDelete = node.ViewerCanDelete,
				ViewerDidAuthor = node.ViewerDidAuthor,
				ViewerHasUpvoted = node.ViewerHasUpvoted,
			};

		private sealed class SearchResponse<TNode>
		{
			public SearchConnection<TNode>? Search { get; set; }
		}

		private sealed class SearchConnection<TNode>
		{
			public List<TNode?> Nodes { get; set; } = [];

			public PageInfo PageInfo { get; set; } = new();
		}

		private class DiscussionLabelNode
		{
			public LabelConnectionNode Labels { get; set; } = new();
		}

		private sealed class DiscussionNode : DiscussionLabelNode
		{
			public DateTimeOffset? AnswerChosenAt { get; set; }

			public ActorNode? Author { get; set; }

			public DiscussionCategoryNode Category { get; set; } = new();

			public bool Closed { get; set; }

			public CountNode Comments { get; set; } = new();

			public DateTimeOffset CreatedAt { get; set; }

			public string Id { get; set; } = string.Empty;

			public bool Locked { get; set; }

			public int Number { get; set; }

			public RepositoryNode Repository { get; set; } = new();

			public string Title { get; set; } = string.Empty;

			public DateTimeOffset UpdatedAt { get; set; }

			public int UpvoteCount { get; set; }

			public string Url { get; set; } = string.Empty;

			public bool ViewerCanDelete { get; set; }

			public bool ViewerDidAuthor { get; set; }

			public bool ViewerHasUpvoted { get; set; }
		}

		private sealed class ActorNode
		{
			public string? AvatarUrl { get; set; }

			public string Login { get; set; } = string.Empty;
		}

		private sealed class CountNode
		{
			public int TotalCount { get; set; }
		}

		private sealed class DiscussionCategoryNode
		{
			public string Emoji { get; set; } = string.Empty;

			public string Id { get; set; } = string.Empty;

			public string Name { get; set; } = string.Empty;
		}

		private sealed class LabelConnectionNode
		{
			public List<LabelNode?> Nodes { get; set; } = [];
		}

		private sealed class LabelNode
		{
			public string Color { get; set; } = string.Empty;

			public string? Description { get; set; }

			public string Name { get; set; } = string.Empty;
		}

		private sealed class RepositoryNode
		{
			public string Name { get; set; } = string.Empty;

			public RepositoryOwnerNode Owner { get; set; } = new();
		}

		private sealed class RepositoryOwnerNode
		{
			public string AvatarUrl { get; set; } = string.Empty;

			public string Id { get; set; } = string.Empty;

			public string Login { get; set; } = string.Empty;
		}
	}
}
