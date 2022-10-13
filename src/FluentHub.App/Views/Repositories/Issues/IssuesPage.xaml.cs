using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.Issues;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Issues
{
    public sealed partial class IssuesPage : Page
    {
        public IssuesPage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<IssuesViewModel>();
            _navigation = provider.GetRequiredService<INavigationService>();
        }

        public IssuesViewModel ViewModel { get; }
        private readonly INavigationService _navigation;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationArgs;
            ViewModel.Login = param.Login;
            ViewModel.Name = param.Name;

            var command = ViewModel.LoadRepositoryIssuesPageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
