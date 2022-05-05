using FluentHub.Services;
using FluentHub.ViewModels.Repositories.Issues;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Repositories.Issues
{
    public sealed partial class IssuePage : Page
    {
        public IssuePage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<IssueViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public IssueViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Octokit.Models.Issue param = e.Parameter as Octokit.Models.Issue;

            DataContext = e.Parameter;

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = $"{param.Title} · Issue #{param.Number} · {param.OwnerLogin}/{param.Name}";
            currentItem.Description = currentItem.Header;
            currentItem.Url = $"https://github.com/{param.OwnerLogin}/{param.Name}/issues/{param.Number}";
            currentItem.Icon = new Microsoft.UI.Xaml.Controls.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Issues.png"))
            };

            var command = ViewModel.RefreshIssuePageCommand;
            if (command.CanExecute(param))
                command.Execute(param);
        }
    }
}
