// Copyright (c) 0x5BFA. All rights reserved.
// Licensed under the MIT License. See the LICENSE.

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentHub.Core;
using FluentHub.Core.Application.Models;
using FluentHub.Core.Infrastructure.GitHub.Clients;
using FluentHub.Services;
using System.Collections.ObjectModel;
using OctokitRest = Octokit.Rest;

namespace FluentHub.ViewModels;

public sealed class InboxViewModel : ObservableObject
{
	private readonly IFluentHubGitHubClient _gitHub;
	private readonly GitHubSessionManager _session;
	private readonly JsonSettingsStore _settings;
	private readonly ObservableCollection<InboxItemViewModel> _items = new();
	private CancellationTokenSource? _loadCancellation;
	private bool _isLoading;
	private int _unreadCount;
	private string? _errorMessage;

	public InboxViewModel(
		IFluentHubGitHubClient gitHub,
		GitHubSessionManager session,
		JsonSettingsStore settings)
	{
		_gitHub = gitHub ?? throw new ArgumentNullException(nameof(gitHub));
		_session = session ?? throw new ArgumentNullException(nameof(session));
		_settings = settings ?? throw new ArgumentNullException(nameof(settings));
		Items = new ReadOnlyObservableCollection<InboxItemViewModel>(_items);
		RefreshCommand = new AsyncRelayCommand(LoadAsync, () => !IsLoading);
	}

	public ReadOnlyObservableCollection<InboxItemViewModel> Items { get; }

	public AsyncRelayCommand RefreshCommand { get; }

	public bool IsLoading
	{
		get => _isLoading;
		private set
		{
			if (!SetProperty(ref _isLoading, value))
				return;

			RefreshCommand.NotifyCanExecuteChanged();
			OnPropertyChanged(nameof(IsEmpty));
		}
	}

	public int UnreadCount
	{
		get => _unreadCount;
		private set
		{
			if (!SetProperty(ref _unreadCount, value))
				return;

			OnPropertyChanged(nameof(UnreadSummary));
		}
	}

	public string UnreadSummary
		=> UnreadCount == 0
			? "No unread notifications"
			: $"{UnreadCount} unread notification{(UnreadCount == 1 ? string.Empty : "s")}";

	public bool HasItems => Items.Count > 0;

	public bool IsEmpty => !IsLoading && !HasItems && !HasError;

	public string? ErrorMessage
	{
		get => _errorMessage;
		private set
		{
			if (!SetProperty(ref _errorMessage, value))
				return;

			OnPropertyChanged(nameof(HasError));
			OnPropertyChanged(nameof(IsEmpty));
		}
	}

	public bool HasError => !string.IsNullOrWhiteSpace(ErrorMessage);

	public async Task LoadAsync()
	{
		if (IsLoading)
			return;

		var login = _settings.SignedInUserName;
		if (string.IsNullOrWhiteSpace(login) || !_session.IsAuthenticated)
		{
			ErrorMessage = "Sign in to GitHub to load your inbox.";
			return;
		}

		using var cancellation = new CancellationTokenSource();
		_loadCancellation = cancellation;
		IsLoading = true;
		ErrorMessage = null;

		try
		{
			var notifications = await _gitHub.Users.Notifications.GetAllAsync(
				new OctokitRest.NotificationRequest { All = true },
				new OctokitRest.PageOptions
				{
					PageCount = 1,
					PageSize = 30,
					StartPage = 1,
				},
				cancellation.Token);

			cancellation.Token.ThrowIfCancellationRequested();
			_items.Clear();
			UnreadCount = 0;

			foreach (var notification in notifications)
			{
				cancellation.Token.ThrowIfCancellationRequested();
				var item = new InboxItemViewModel(notification);
				_items.Add(item);
				if (item.IsUnread)
					UnreadCount++;
			}

			OnPropertyChanged(nameof(HasItems));
			OnPropertyChanged(nameof(IsEmpty));
		}
		catch (OperationCanceledException) when (cancellation.IsCancellationRequested)
		{
			// The view was unloaded while the request was in flight.
		}
		catch (Exception)
		{
			ErrorMessage = "Couldn't load your inbox. Try again.";
		}
		finally
		{
			if (ReferenceEquals(_loadCancellation, cancellation))
				_loadCancellation = null;

			IsLoading = false;
		}
	}

	public void CancelLoading()
		=> _loadCancellation?.Cancel();
}

public sealed class InboxItemViewModel
{
	private readonly Notification _notification;

	public InboxItemViewModel(Notification notification)
		=> _notification = notification ?? throw new ArgumentNullException(nameof(notification));

	public long Id => _notification.Id;

	public bool IsUnread => _notification.Unread;

	public string RepositoryName
		=> _notification.Repository is { } repository
			? repository.Owner is { } owner
				? $"{owner.Login}/{repository.Name}"
				: repository.Name
			: "GitHub";

	public string Title
		=> string.IsNullOrWhiteSpace(_notification.Subject.Title)
			? "Untitled notification"
			: _notification.Subject.Title;

	public string TypeLabel
		=> _notification.Subject.Type switch
		{
			NotificationSubjectType.Issue or
			NotificationSubjectType.IssueOpen or
			NotificationSubjectType.IssueClosedAsCompleted or
			NotificationSubjectType.IssueClosedAsNotPlanned => "Issue",
			NotificationSubjectType.PullRequest or
			NotificationSubjectType.PullRequestOpen or
			NotificationSubjectType.PullRequestClosed or
			NotificationSubjectType.PullRequestMerged or
			NotificationSubjectType.PullRequestDraft => "Pull request",
			NotificationSubjectType.Discussion => "Discussion",
			NotificationSubjectType.Commit => "Commit",
			NotificationSubjectType.Release => "Release",
			_ => "Notification",
		};

	public string Reason => _notification.Reason ?? string.Empty;

	public string UpdatedAt
		=> _notification.UpdatedAtHumanized
			?? _notification.UpdatedAt.ToLocalTime().ToString("g");

	public string? Url => _notification.Url;
}
