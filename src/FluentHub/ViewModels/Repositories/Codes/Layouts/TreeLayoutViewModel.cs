using FluentHub.Models;
using FluentHub.Backend;
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
    public class TreeLayoutViewModel : ObservableObject
    {
        #region constructor
        public TreeLayoutViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _messenger = messenger;
            _items = new();
            Items = new(_items);

            LoadTreeViewContentsCommand = new AsyncRelayCommand(LoadTreeViewContentsAsync);
        }
        #endregion

        #region fields and properties
        private readonly ILogger _logger;
        private readonly IMessenger _messenger;

        private bool isLoading;
        public bool IsLoading { get => isLoading; set => SetProperty(ref isLoading, value); }

        private bool _blobSelected;
        public bool BlobSelected { get => _blobSelected; set => SetProperty(ref _blobSelected, value); }

        private RepoContextViewModel _contextViewModel;
        public RepoContextViewModel ContextViewModel { get => _contextViewModel; set => SetProperty(ref _contextViewModel, value); }

        private RepoContextViewModel _selectedContextViewModel;
        public RepoContextViewModel SelectedContextViewModel { get => _selectedContextViewModel; set => SetProperty(ref _selectedContextViewModel, value); }

        private readonly ObservableCollection<TreeLayoutPageModel> _items;
        public ReadOnlyObservableCollection<TreeLayoutPageModel> Items { get; }

        public IAsyncRelayCommand LoadTreeViewContentsCommand { get; }
        #endregion

        #region methods
        private async Task LoadTreeViewContentsAsync(CancellationToken token)
        {
            try
            {
                if (ContextViewModel.Repository.DefaultBranchName == null) return;
                IsLoading = true;

                ContentQueries queries = new();
                var objects = await queries.GetAllAsync(
                    ContextViewModel.Name,
                    ContextViewModel.Owner,
                    ContextViewModel.BranchName,
                    ContextViewModel.Path);

                foreach (var obj in objects)
                {
                    TreeLayoutPageModel model = new()
                    {
                        Name = obj.Name,
                        Path = obj.Path,
                        Tag = obj.Type,
                        IsBolb = false,
                    };

                    if (obj.Type == "tree")
                    {
                        model.Glyph = "\uE9A0";
                    }
                    else
                    {
                        model.Glyph = "\uE996";
                        model.IsBolb = true;
                    }

                    _items.Add(model);
                }

                var orderedItems = new ObservableCollection<TreeLayoutPageModel>(Items.OrderByDescending(x => x.Glyph));
                _items.Clear();
                foreach (var item in orderedItems) _items.Add(item);
                IsLoading = false;
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                _logger?.Error("LoadTreeViewContentsAsync", ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                IsLoading = false;
                throw;
            }
        }

        public async Task<List<TreeLayoutPageModel>> LoadSubItemsAsync(string path)
        {
            try
            {
                IsLoading = true;

                var pathItems = path.Split("/");
                List<TreeLayoutPageModel> subItems = new();

                if (ContextViewModel.Repository.DefaultBranchName == null)
                    return null;

                ContentQueries queries = new();
                var objects = await queries.GetAllAsync(
                    ContextViewModel.Name,
                    ContextViewModel.Owner,
                    ContextViewModel.BranchName,
                    path);

                foreach (var obj in objects)
                {
                    TreeLayoutPageModel model = new()
                    {
                        Name = obj.Name,
                        Path = obj.Path,
                        Tag = obj.Type,
                        IsBolb = false,
                    };

                    if (obj.Type == "tree")
                    {
                        model.Glyph = "\uE9A0";
                    }
                    else
                    {
                        model.Glyph = "\uE996";
                        model.IsBolb = true;
                    }

                    subItems.Add(model);
                }

                var orderedItems = new List<TreeLayoutPageModel>(subItems.OrderByDescending(x => x.Glyph));
                subItems.Clear();
                foreach (var item in orderedItems) subItems.Add(item);

                IsLoading = false;

                return subItems;
            }
            catch (OperationCanceledException) { }
            catch (Exception ex)
            {
                IsLoading = false;

                _logger?.Error("LoadSubItemsAsync", ex);
                if (_messenger != null)
                {
                    UserNotificationMessage notification = new("Something went wrong", ex.Message, UserNotificationType.Error);
                    _messenger.Send(notification);
                }
                throw;
            }

            return null;
        }
        #endregion
    }
}
