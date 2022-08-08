using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.UserControls.Blocks;
using FluentHub.Uwp.ViewModels.UserControls;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;

namespace FluentHub.Uwp.ViewModels.Repositories.Issues
{
    public class IssueViewModel : ObservableObject
    {
        public IssueViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;

            _eventBlocks = new();
            EventBlocks = new(_eventBlocks);

            _timelineItems = new();
            TimelineItems = new(_timelineItems);

            RefreshIssuePageCommand = new AsyncRelayCommand(LoadRepositoryOneIssueAsync);
        }

        #region Fields and Properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private RepositoryOverviewViewModel _repositoryOverviewViewModel;
        public RepositoryOverviewViewModel RepositoryOverviewViewModel { get => _repositoryOverviewViewModel; set => SetProperty(ref _repositoryOverviewViewModel, value); }

        private int _number;
        public int Number { get => _number; set => SetProperty(ref _number, value); }

        private Issue _issueItem;
        public Issue IssueItem { get => _issueItem; private set => SetProperty(ref _issueItem, value); }

        private readonly ObservableCollection<object> _timelineItems;
        public ReadOnlyObservableCollection<object> TimelineItems { get; set; }

        private TimelineViewModel _eventBlockViewModel;
        public TimelineViewModel EventBlockViewModel { get => _eventBlockViewModel; private set => SetProperty(ref _eventBlockViewModel, value); }

        private readonly ObservableCollection<Timeline> _eventBlocks;
        public ReadOnlyObservableCollection<Timeline> EventBlocks { get; }

        public IAsyncRelayCommand RefreshIssuePageCommand { get; }
        #endregion

        private async Task LoadRepositoryOneIssueAsync(CancellationToken token)
        {
            try
            {
                _messenger?.Send(new LoadingMessaging(true));

                IssueQueries issueQueries = new();
                IssueItem = await issueQueries.GetAsync(
                    Repository.Owner.Login,
                    Repository.Name,
                    Number
                    );

                var bodyComment = await issueQueries.GetBodyAsync(
                    Repository.Owner.Login,
                    Repository.Name,
                    Number
                    );

                _eventBlocks.Clear();
                _eventBlocks.Add(new Timeline()
                {
                    ViewModel = new TimelineViewModel()
                    {
                        EventType = nameof(IssueComment),
                        IssueComment = bodyComment,
                        CommentBlockViewModel = new()
                        {
                            IssueComment = bodyComment,
                        },
                    },
                });

                _timelineItems.Clear();
                _timelineItems.Add(bodyComment as object);

                IssueEventQueries queries = new();
                var issueEvents = await queries.GetAllAsync(
                    Repository.Owner.Login,
                    Repository.Name,
                    Number
                    );

                foreach (var item in issueEvents)
                    _timelineItems.Add(item);

                /*
                foreach (var eventItem in issueEvents)
                {
                    if (eventItem is null)
                        continue;

                    var viewmodel = new TimelineViewModel()
                    {
                        // FluentHub.Octokit.Models.Events.*
                        EventType = eventItem.GetType().ToString().Split(".").ElementAtOrDefault(4),
                        Event = eventItem,
                    };

                    switch (viewmodel.EventType)
                    {
                        case nameof(AddedToProjectEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA48";
                            viewmodel.AddedToProjectEvent = eventItem as AddedToProjectEvent;
                            viewmodel.Actor = viewmodel?.AddedToProjectEvent.Actor as Actor;
                            break;
                        case nameof(AssignedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.AssignedEvent = eventItem as AssignedEvent;
                            viewmodel.Actor = viewmodel?.AssignedEvent.Actor as Actor;
                            break;
                        case nameof(ClosedEvent):
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#8256D0");
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ClosedEvent = eventItem as ClosedEvent;
                            viewmodel.Actor = viewmodel?.ClosedEvent.Actor as Actor;
                            break;
                        case nameof(CommentDeletedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.CommentDeletedEvent = eventItem as CommentDeletedEvent;
                            viewmodel.Actor = viewmodel?.CommentDeletedEvent.Actor as Actor;
                            break;
                        case nameof(ConnectedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE924";
                            viewmodel.ConnectedEvent = eventItem as ConnectedEvent;
                            viewmodel.Actor = viewmodel?.ConnectedEvent.Actor as Actor;
                            break;
                        case nameof(ConvertedNoteToIssueEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ConvertedNoteToIssueEvent = eventItem as ConvertedNoteToIssueEvent;
                            viewmodel.Actor = viewmodel?.ConvertedNoteToIssueEvent.Actor as Actor;
                            break;
                        case nameof(CrossReferencedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.CrossReferencedEvent = eventItem as CrossReferencedEvent;
                            viewmodel.Actor = viewmodel?.CrossReferencedEvent.Actor as Actor;
                            break;
                        case nameof(DemilestonedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.DemilestonedEvent = eventItem as DemilestonedEvent;
                            viewmodel.Actor = viewmodel?.DemilestonedEvent.Actor as Actor;
                            break;
                        case nameof(DisconnectedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.DisconnectedEvent = eventItem as DisconnectedEvent;
                            viewmodel.Actor = viewmodel?.DisconnectedEvent.Actor as Actor;
                            break;
                        case nameof(IssueComment):
                            viewmodel.CommentBlockViewModel = new() { IssueComment = viewmodel.IssueComment = eventItem as IssueComment };
                            break;
                        case nameof(LabeledEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEAA5";
                            viewmodel.LabeledEvent = eventItem as LabeledEvent;
                            viewmodel.LabelControlViewModel = new()
                            {
                                Name = viewmodel?.LabeledEvent.Label.Name,
                                Color = viewmodel?.LabeledEvent.Label.Color,
                            };
                            viewmodel.Actor = viewmodel?.LabeledEvent.Actor as Actor;
                            break;
                        case nameof(LockedEvent):
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#636E7B");
                            viewmodel.TimelineBadgeGlyph = "\uEA05";
                            viewmodel.LockedEvent = eventItem as LockedEvent;
                            viewmodel.Actor = viewmodel?.LockedEvent.Actor as Actor;
                            break;
                        case nameof(MarkedAsDuplicateEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE924";
                            viewmodel.MarkedAsDuplicateEvent = eventItem as MarkedAsDuplicateEvent;
                            viewmodel.Actor = viewmodel?.MarkedAsDuplicateEvent.Actor as Actor;
                            break;
                        //case nameof(MentionedEvent):
                        case nameof(MilestonedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA12";
                            viewmodel.MilestonedEvent = eventItem as MilestonedEvent;
                            viewmodel.Actor = viewmodel?.MilestonedEvent.Actor as Actor;
                            break;
                        case nameof(MovedColumnsInProjectEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.MovedColumnsInProjectEvent = eventItem as MovedColumnsInProjectEvent;
                            viewmodel.Actor = viewmodel?.MovedColumnsInProjectEvent.Actor as Actor;
                            break;
                        case nameof(PinnedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.PinnedEvent = eventItem as PinnedEvent;
                            viewmodel.Actor = viewmodel?.PinnedEvent.Actor as Actor;
                            break;
                        case nameof(ReferencedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uE968";
                            viewmodel.ReferencedEvent = eventItem as ReferencedEvent;
                            viewmodel.Actor = viewmodel?.ReferencedEvent.Actor as Actor;
                            break;
                        case nameof(RemovedFromProjectEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA48";
                            viewmodel.RemovedFromProjectEvent = eventItem as RemovedFromProjectEvent;
                            viewmodel.Actor = viewmodel?.RemovedFromProjectEvent.Actor as Actor;
                            break;
                        case nameof(RenamedTitleEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEA34";
                            viewmodel.RenamedTitleEvent = eventItem as RenamedTitleEvent;
                            viewmodel.Actor = viewmodel?.RenamedTitleEvent.Actor as Actor;
                            break;
                        case nameof(ReopenedEvent):
                            viewmodel.TimelineBadgeBackground = Helpers.ColorHelpers.HexCodeToSolidColorBrush("#347D39");
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.ReopenedEvent = eventItem as ReopenedEvent;
                            viewmodel.Actor = viewmodel?.ReopenedEvent.Actor as Actor;
                            break;
                        //case nameof(SubscribedEvent):
                        case nameof(UnassignedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnassignedEvent = eventItem as UnassignedEvent;
                            viewmodel.Actor = viewmodel?.UnassignedEvent.Actor as Actor;
                            break;
                        case nameof(UnlabeledEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnlabeledEvent = eventItem as UnlabeledEvent;
                            viewmodel.LabelControlViewModel = new()
                            {
                                Name = viewmodel?.UnlabeledEvent.Label.Name,
                                Color = viewmodel?.UnlabeledEvent.Label.Color,
                            };
                            viewmodel.Actor = viewmodel?.UnlabeledEvent.Actor as Actor;
                            break;
                        case nameof(UnlockedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnlockedEvent = eventItem as UnlockedEvent;
                            viewmodel.Actor = viewmodel?.UnlockedEvent.Actor as Actor;
                            break;
                        case nameof(UnmarkedAsDuplicateEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnmarkedAsDuplicateEvent = eventItem as UnmarkedAsDuplicateEvent;
                            viewmodel.Actor = viewmodel?.UnmarkedAsDuplicateEvent.Actor as Actor;
                            break;
                        case nameof(UnpinnedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UnpinnedEvent = eventItem as UnpinnedEvent;
                            viewmodel.Actor = viewmodel?.UnpinnedEvent.Actor as Actor;
                            break;
                        //case nameof(UnsubscribedEvent):
                        case nameof(UserBlockedEvent):
                            viewmodel.TimelineBadgeGlyph = "\uEADB";
                            viewmodel.UserBlockedEvent = eventItem as UserBlockedEvent;
                            viewmodel.Actor = viewmodel?.UserBlockedEvent.Actor as Actor;
                            break;
                    }

                    var eventBlock = new Timeline()
                    {
                        ViewModel = viewmodel
                    };

                    _eventBlocks.Add(eventBlock);
                }
                */
            }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryOneIssueAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
            finally
            {
                _messenger?.Send(new LoadingMessaging(false));
            }
        }

        public async Task LoadRepositoryAsync(string owner, string name)
        {
            try
            {
                RepositoryQueries queries = new();
                Repository = await queries.GetDetailsAsync(owner, name);

                RepositoryOverviewViewModel = new()
                {
                    Repository = Repository,
                    RepositoryName = Repository.Name,
                    RepositoryOwnerLogin = Repository.Owner.Login,
                    RepositoryVisibilityLabel = new()
                    {
                        Name = Repository.IsPrivate ? "Private" : "Public",
                        Color = "#64000000",
                    },
                    ViewerSubscriptionState = Repository.ViewerSubscription?.Humanize(),

                    SelectedTag = "issues",
                };
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error(nameof(LoadRepositoryAsync), ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
    }
}
