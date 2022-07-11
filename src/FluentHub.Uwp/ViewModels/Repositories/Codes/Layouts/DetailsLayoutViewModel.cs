using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;
using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.Uwp.ViewModels.Repositories.Codes.Layouts
{
    public class DetailsLayoutViewModel : ObservableObject
    {
        #region constructor
        public DetailsLayoutViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;
            _items = new();
            Items = new(_items);

            RefreshDetailsLayoutPageCommand = new AsyncRelayCommand<string>(RefreshDetailsLayoutPageAsync);
            LoadRepositoryCommand = new AsyncRelayCommand<string>(LoadRepositoryAsync);
        }
        #endregion

        #region fields and properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private RepoContextViewModel contextViewModel;
        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }

        private Repository _repository;
        public Repository Repository { get => _repository; set => SetProperty(ref _repository, value); }

        private readonly ObservableCollection<DetailsLayoutListViewModel> _items;
        public ReadOnlyObservableCollection<DetailsLayoutListViewModel> Items { get; }

        public IAsyncRelayCommand RefreshDetailsLayoutPageCommand { get; }
        public IAsyncRelayCommand LoadRepositoryCommand { get; }
        #endregion

        #region methods
        private async Task RefreshDetailsLayoutPageAsync(string url, CancellationToken token)
        {
            try
            {
                // There aren't no contents in the repository
                if (ContextViewModel.Repository.DefaultBranchName == null) return;

                if (ContextViewModel.IsFile) return;
                ContextViewModel.IsDir = true;

                CommitQueries queries = new();
                var fileOverviews = await queries.GetWithObjectNameAsync(
                    ContextViewModel.Repository.Name,
                    ContextViewModel.Repository.Owner.Login,
                    ContextViewModel.BranchName,
                    ContextViewModel.Path);

                if (string.IsNullOrEmpty(ContextViewModel.Path))
                    ContextViewModel.IsRootDir = true;
                else
                    ContextViewModel.IsSubDir = true;

                foreach (var overview in fileOverviews)
                {
                    DetailsLayoutListViewModel listItem = new();

                    if (overview.ObjectType == "tree")
                    {
                        listItem.ObjectTypeIconGlyph = "\uE9A0";
                        listItem.ObjectTag = "tree/" + overview.FileName;
                    }
                    else
                    {
                        listItem.ObjectTypeIconGlyph = "\uE996";
                        listItem.ObjectTag = "blob/" + overview.FileName;
                    }

                    listItem.ObjectName = overview.FileName;
                    listItem.ObjectLatestCommitMessage = overview.CommitMessage.Split("\n").FirstOrDefault();
                    listItem.ObjectUpdatedAtHumanized = overview.CommittedAtHumanized;

                    _items.Add(listItem);
                }

                var orderedByItemType = new ObservableCollection<DetailsLayoutListViewModel>(Items.OrderByDescending(x => x.ObjectTypeIconGlyph));
                _items.Clear();
                foreach (var orderedItem in orderedByItemType) _items.Add(orderedItem);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("RefreshDetailsLayoutPageAsync", ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }

        private async Task LoadRepositoryAsync(string url, CancellationToken token)
        {
            try
            {
                var uri = new Uri(url);
                var pathSegments = uri.AbsolutePath.Split("/").ToList();
                pathSegments.RemoveAt(0);

                RepositoryQueries queries = new();
                Repository = await queries.GetDetailsAsync(pathSegments[0], pathSegments[1]);
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("RefreshDetailsLayoutPageAsync", ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }
        }
        #endregion
    }
}
