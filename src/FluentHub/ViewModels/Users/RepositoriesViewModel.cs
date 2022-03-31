using FluentHub.Backend;
using FluentHub.Octokit.Models;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Users
{
    public class RepositoriesViewModel : ObservableObject
    {
        public RepositoriesViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _repositories = new();
            Repositories = new(_repositories);

            RefreshRepositoriesCommand = new AsyncRelayCommand<string>(RefreshRepositoriesAsync);
        }

        private readonly IMessenger _messenger;
        private readonly ILogger _logger;
        private readonly ObservableCollection<RepoButtonBlockViewModel> _repositories;
        public ReadOnlyObservableCollection<RepoButtonBlockViewModel> Repositories { get; }

        public IAsyncRelayCommand RefreshRepositoriesCommand { get; }

        private async Task RefreshRepositoriesAsync(string login)
        {
            try
            {
                RepositoryQueries queries = new();
                List<Repository> items;

                items = login == null ?
                    await queries.GetAllAsync() :
                    await queries.GetAllAsync(login);

                if (items == null) return;

                _repositories.Clear();
                foreach (var item in items)
                {
                    RepoButtonBlockViewModel viewModel = new()
                    {
                        Item = item,
                        DisplayDetails = true,
                        DisplayStarButton = true
                    };

                    _repositories.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error("RefreshRepositoriesAsync", ex);
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