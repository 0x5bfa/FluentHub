using FluentHub.Backend;
using FluentHub.Octokit.Models;
using FluentHub.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.Repositories.Projects
{
    public class ProjectsViewModel : ObservableObject
    {
        #region constructor
        public ProjectsViewModel(IMessenger messenger = null, ILogger logger = null)
        {
            _messenger = messenger;
            _logger = logger;
            _items = new();
            Items = new(_items);

            LoadProjectsPageCommand = new AsyncRelayCommand<string>(LoadProjectsPageAsync);
        }
        #endregion

        #region properties
        private readonly IMessenger _messenger;
        private readonly ILogger _logger;

        private readonly ObservableCollection<ProjectButtonBlockViewModel> _items;
        public ReadOnlyObservableCollection<ProjectButtonBlockViewModel> Items { get; }

        public IAsyncRelayCommand LoadProjectsPageCommand { get; }
        #endregion

        #region methods
        private async Task LoadProjectsPageAsync(string nameWithOwner)
        {
            try
            {
                ProjectQueries queries = new();
                var items = await queries.GetAllAsync(nameWithOwner.Split("/")[0], nameWithOwner.Split("/")[1]);

                _items.Clear();
                foreach (var item in items)
                {
                    ProjectButtonBlockViewModel viewmodel = new()
                    {
                        Item = item,
                    };

                    _items.Add(viewmodel);
                }
            }
            catch (Exception ex)
            {
                _logger?.Error("LoadProjectsPageAsync", ex);
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
