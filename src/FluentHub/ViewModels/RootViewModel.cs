// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.Mvvm.ComponentModel;
using FluentHub.Core;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Services;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FluentHub.ViewModels;

public sealed partial class RootViewModel : ObservableObject
{
	private readonly IFluentHubGitHubClient _gitHub;
	private readonly GitHubSessionManager _session;
	private readonly JsonSettingsStore _settings;
	private readonly ObservableCollection<RepositoryNavigationItem> _repositories = new();
	private CancellationTokenSource? _repositoryLoadCancellation;
	private CancellationTokenSource? _profileLoadCancellation;
	private bool _repositoriesLoaded;
	private bool _profileLoaded;
	private bool _isLoadingProfile;

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

	public bool IsAuthenticated
		=> _session.IsAuthenticated && _settings.HasSession;

	public string ProfileDisplayName
		=> string.IsNullOrWhiteSpace(ProfileDisplayNameValue)
			? _settings.SignedInUserName ?? Strings.RootViewModel_SignInText.GetLocalized()
			: ProfileDisplayNameValue;

	public string ProfileUsername
		=> string.IsNullOrWhiteSpace(_settings.SignedInUserName)
			? string.Empty
			: $"@{_settings.SignedInUserName}";

	public string ProfileActionText
		=> IsAuthenticated
			? Strings.RootViewModel_LogOutText.GetLocalized()
			: Strings.RootViewModel_LogInText.GetLocalized();

	public ImageSource? ProfilePicture => ProfilePictureValue;

	[ObservableProperty]
	public partial bool IsLoadingRepositories { get; private set; }

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(ProfileDisplayName))]
	private partial string? ProfileDisplayNameValue { get; set; }

	[ObservableProperty]
	[NotifyPropertyChangedFor(nameof(ProfilePicture))]
	private partial ImageSource? ProfilePictureValue { get; set; }

	public async Task LoadRepositoriesAsync()
	{
		if (_repositoriesLoaded || IsLoadingRepositories)
		{
			return;
		}

		var login = _settings.SignedInUserName;
		if (!IsAuthenticated || string.IsNullOrWhiteSpace(login))
		{
			return;
		}

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

			if (!IsAuthenticated)
			{
				return;
			}

			_repositories.Clear();
			foreach (var repository in repositories)
			{
				_repositories.Add(repository);
			}

			_repositoriesLoaded = true;
		}
		finally
		{
			if (ReferenceEquals(_repositoryLoadCancellation, cancellation))
			{
				_repositoryLoadCancellation = null;
				IsLoadingRepositories = false;
			}
		}
	}

	public async Task LoadProfileAsync()
	{
		if (_profileLoaded || _isLoadingProfile || !IsAuthenticated)
		{
			return;
		}

		var login = _settings.SignedInUserName;
		if (string.IsNullOrWhiteSpace(login))
		{
			return;
		}

		using var cancellation = new CancellationTokenSource();
		_profileLoadCancellation = cancellation;
		_isLoadingProfile = true;

		try
		{
			var user = await _gitHub.Users.Users.GetAsync(login, cancellation.Token);
			if (!IsAuthenticated)
			{
				return;
			}

			ProfileDisplayNameValue = string.IsNullOrWhiteSpace(user.Name) ? user.Login : user.Name;
			ProfilePictureValue = CreateProfilePicture(user.AvatarUrl);
			_profileLoaded = true;
		}
		catch (OperationCanceledException) when (cancellation.IsCancellationRequested)
		{
			// The view was unloaded while the request was in flight.
		}
		catch (Exception)
		{
			// Keep the title bar usable when the profile cannot be loaded.
		}
		finally
		{
			if (ReferenceEquals(_profileLoadCancellation, cancellation))
			{
				_profileLoadCancellation = null;
				_isLoadingProfile = false;
			}
		}
	}

	public void CancelLoading()
	{
		var repositoryCancellation = _repositoryLoadCancellation;
		_repositoryLoadCancellation = null;
		repositoryCancellation?.Cancel();
		IsLoadingRepositories = false;

		var profileCancellation = _profileLoadCancellation;
		_profileLoadCancellation = null;
		profileCancellation?.Cancel();
		_isLoadingProfile = false;
	}

	public async Task SignOutAsync(CancellationToken cancellationToken = default)
	{
		CancelLoading();
		await _settings.ClearAsync(cancellationToken);
		_session.SignOut();

		_repositories.Clear();
		_repositoriesLoaded = false;
		_profileLoaded = false;
		ProfileDisplayNameValue = null;
		ProfilePictureValue = null;
		RefreshAuthenticationState();
	}

	public void RefreshAuthenticationState()
	{
		// Session and settings are not observable, so this is the notification bridge for their state.
		OnPropertyChanged(nameof(IsAuthenticated));
		OnPropertyChanged(nameof(ProfileDisplayName));
		OnPropertyChanged(nameof(ProfileUsername));
		OnPropertyChanged(nameof(ProfileActionText));
	}

	private static ImageSource? CreateProfilePicture(string? avatarUrl)
	{
		if (!Uri.TryCreate(avatarUrl, UriKind.Absolute, out var uri) ||
			(uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
		{
			return null;
		}

		return new BitmapImage(uri);
	}
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
