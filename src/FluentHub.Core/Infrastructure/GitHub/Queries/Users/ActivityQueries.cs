// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using System.IO;
using FluentHub.Core.Application.Abstractions.Caching;
using FluentHub.Core.Infrastructure.Caching;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Core.Infrastructure.GitHub.Serialization;

namespace FluentHub.Core.Infrastructure.GitHub.Queries.Users
{
	public class ActivityQueries
	{
		private const string ContributionCalendarQuery = """
			query ContributionCalendar($login: String!) {
			  result: user(login: $login) {
			    contributionsCollection {
			      contributionCalendar {
			        colors totalContributions
			        months { firstDay name totalWeeks year }
			        weeks {
			          firstDay
			          contributionDays { color contributionCount contributionLevel date weekday }
			        }
			      }
			    }
			  }
			}
			""";

		private readonly IGitHubApiClient _gitHub;
		private readonly ICacheService? _cache;

		public ActivityQueries(IGitHubApiClient gitHub, ICacheService? cache = null)
		{
			_gitHub = gitHub;
			_cache = cache;
		}

		public async Task<List<Activity>> GetAllAsync(string login, CancellationToken cancellationToken = default)
		{
			var response = await _gitHub.RunRestAsync(
				(client, token) => client.Activity.GetReceivedEventsAsync(login, 60, 1, token),
				cancellationToken);
			return new Wrappers.ActivityWrapper().Wrap(response);
		}

		public Task<ContributionCalendar> GetContributionCalendarAsync(
			string login,
			CancellationToken cancellationToken = default)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(login);
			if (_cache is null)
				return GetContributionCalendarUncachedAsync(login, cancellationToken);

			var key = CacheKey.ForAccount(_gitHub.CachePartition, "contribution-calendars-v2", login.Trim().ToLowerInvariant());
			return _cache.GetOrCreateAsync(
				key,
				CachePolicies.User,
				GitHubCacheSerializers.ContributionCalendar,
				token => GetContributionCalendarUncachedAsync(login, token),
				cancellationToken);
		}

		private async Task<ContributionCalendar> GetContributionCalendarUncachedAsync(
			string login,
			CancellationToken cancellationToken)
		{
			var response = await _gitHub.RunGraphQLAsync(
				ContributionCalendarQuery,
				GitHubGraphQLJsonContext.Default.GraphQLResultUser,
				writer => writer.WriteString("login", login),
				cancellationToken);
			return response.Result?.ContributionsCollection?.ContributionCalendar
				?? throw new InvalidDataException("GitHub returned an incomplete contribution calendar response.");
		}
	}
}
