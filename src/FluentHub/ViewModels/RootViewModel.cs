// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.Mvvm.ComponentModel;
using FluentHub.Core;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentHub.ViewModels;

public sealed class RootViewModel : ObservableObject
{
	private readonly IFluentHubGitHubClient _gitHub;
	private readonly GitHubSessionManager _session;
	private readonly JsonSettingsStore _settings;
	private readonly ObservableCollection<RepositoryNavigationItem> _repositories = new();
	private CancellationTokenSource? _repositoryLoadCancellation;
	private bool _repositoriesLoaded;
	private bool _isLoadingRepositories;

	public RootViewModel(
		IFluentHubGitHubClient gitHub,
		GitHubSessionManager session,
		JsonSettingsStore settings)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		_session = session ?? throw new ArgumentNullException(nameof(session));
		_settings = settings ?? throw new ArgumentNullException(nameof(settings));
		Repositories = new ReadOnlyObservableCollection<RepositoryNavigationItem>(_repositories);
	}

	public ReadOnlyObservableCollection<RepositoryNavigationItem> Repositories { get; }

	public bool IsLoadingRepositories
	{
		get => _isLoadingRepositories;
		private set => SetProperty(ref _isLoadingRepositories, value);
	}

	public async Task LoadRepositoriesAsync()
	{
		if (_repositoriesLoaded || IsLoadingRepositories)
			return;

		var login = _settings.SignedInUserName;
		if (string.IsNullOrWhiteSpace(login) || !_session.IsAuthenticated)
			return;

		using var cancellation = new CancellationTokenSource();
		_repositoryLoadCancellation = cancellation;
		IsLoadingRepositories = true;

		try
		{
			var repositoryResult = await _gitHub.Users.Repositories.GetPageAsync(
				login,
				PageRequest.Forward(20),
				cancellationToken: cancellation.Token);

			var repositories = new List<RepositoryNavigationItem>();
			foreach (var repository in repositoryResult.Items)
			{
				cancellation.Token.ThrowIfCancellationRequested();
				repositories.Add(new RepositoryNavigationItem(
					repository.Name,
					repository.NameWithOwner,
					repository.IsPrivate));
			}

			_repositories.Clear();
			foreach (var repository in repositories)
				_repositories.Add(repository);

			_repositoriesLoaded = true;
		}
		finally
		{
			if (ReferenceEquals(_repositoryLoadCancellation, cancellation))
				_repositoryLoadCancellation = null;

			IsLoadingRepositories = false;
		}
	}

	public void CancelLoading()
		=> _repositoryLoadCancellation?.Cancel();
}

public sealed class RepositoryNavigationItem
{
	public RepositoryNavigationItem(string name, string fullName, bool isPrivate)
	{
		Name = name;
		FullName = fullName;
		IsPrivate = isPrivate;
	}

	public string Name { get; }

	public string FullName { get; }

	public bool IsPrivate { get; }
}
