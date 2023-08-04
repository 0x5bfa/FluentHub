// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Data.Parameters;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.PullRequests;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.PullRequests
{
    public sealed partial class ConversationPage : LocatablePage
    {
        public ConversationViewModel ViewModel { get; }
        private readonly INavigationService _navigation;

        public ConversationPage()
            : base(NavigationPageKind.Repository, NavigationPageKey.PullRequests)
        {
            InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<ConversationViewModel>();
            _navigation = Ioc.Default.GetRequiredService<INavigationService>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationParameter;

            if (param == null)
            {
                throw new ArgumentNullException(nameof(param), "OnNavigateTo() failed to load.");
            }

            ViewModel.Login = param.UserLogin;
            ViewModel.Name = param.RepositoryName;
            ViewModel.Number = param.Number;

            var command = ViewModel.LoadRepositoryPullRequestConversationPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
