// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using FluentHub.Core.Application;
using FluentHub.Core.Application.Abstractions.Authentication;
using FluentHub.Core.Infrastructure.GitHub.Authorization;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace FluentHub.Core.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddFluentHubCore(this IServiceCollection services)
	{
		ArgumentNullException.ThrowIfNull(services);

		return services
			.AddSingleton<AccountService>()
			.AddSingleton<GitHubSessionManager>()
			.AddSingleton<IUserSession>(provider => provider.GetRequiredService<GitHubSessionManager>())
			.AddSingleton<IGitHubApiClient, GitHubApiClient>()
			.AddSingleton<IFluentHubGitHubClient, FluentHubGitHubClient>()
			.AddSingleton<AuthorizationService>();
	}
}
