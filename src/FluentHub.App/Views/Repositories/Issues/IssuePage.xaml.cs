// Copyright (c) FluentHub
// Licensed under the MIT License. See the LICENSE.

using FluentHub.App.Data.Parameters;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.Issues;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Issues
{
    public sealed partial class IssuePage : LocatablePage
    {
        public IssueViewModel ViewModel { get; }

        private readonly INavigationService _navigation;

        public IssuePage()
            : base(NavigationBarPageKind.Repository, NavigationBarItemKey.Issues)
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<IssueViewModel>();
            _navigation = provider.GetRequiredService<INavigationService>();
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

            var command = ViewModel.LoadRepositoryIssuePageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
