// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.Issues;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using FluentHub.App.Data.Parameters;

namespace FluentHub.App.Views.Repositories.Issues
{
    public sealed partial class IssuesPage : LocatablePage
    {
        public IssuesViewModel ViewModel { get; }

        private readonly INavigationService _navigation;

        public IssuesPage()
            : base(NavigationPageKind.Repository, NavigationPageKey.Issues)
        {
            InitializeComponent();

            ViewModel = Ioc.Default.GetRequiredService<IssuesViewModel>();
            _navigation = Ioc.Default.GetRequiredService<INavigationService>();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationParameter;
            ViewModel.Login = param.UserLogin;
            ViewModel.Name = param.RepositoryName;

            var command = ViewModel.LoadRepositoryIssuesPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
