using FluentHub.Models;
using FluentHub.Backend;
using FluentHub.Models;
using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
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

namespace FluentHub.ViewModels.Repositories.Codes.Layouts
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

            RefreshDetailsLayoutPageCommand = new AsyncRelayCommand(RefreshDetailsLayoutPageAsync);
        }
        #endregion

        #region fields
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;
        private RepoContextViewModel contextViewModel;
        private readonly ObservableCollection<DetailsLayoutListViewModel> _items;
        #endregion

        #region properties
        public RepoContextViewModel ContextViewModel { get => contextViewModel; set => SetProperty(ref contextViewModel, value); }
        public ReadOnlyObservableCollection<DetailsLayoutListViewModel> Items { get; }
        public IAsyncRelayCommand RefreshDetailsLayoutPageCommand { get; }
        #endregion

        #region methods
        private async Task RefreshDetailsLayoutPageAsync(CancellationToken token)
        {
            try
            {
                if (ContextViewModel.Repository.DefaultBranchName == null) return;
                if (contextViewModel.IsFile) return;

                CommitQueries queries = new();
                var fileOverviews = await queries.GetWithObjectNameAsync(
                    ContextViewModel.Name,
                    ContextViewModel.Owner,
                    ContextViewModel.BranchName,
                    ContextViewModel.Path);

                ContextViewModel.IsDir = true;
                if (ContextViewModel.Path == "/") ContextViewModel.IsRootDir = true;
                else ContextViewModel.IsSubDir = true;

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
        #endregion
    }
}
