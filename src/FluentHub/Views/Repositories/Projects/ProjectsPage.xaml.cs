using FluentHub.Services;
using FluentHub.ViewModels.Repositories.Projects;
using Microsoft.Extensions.DependencyInjection;
using System;
using muxc = Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.Views.Repositories.Projects
{
    public sealed partial class ProjectsPage : Page
    {
        public ProjectsPage()
        {
            this.InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<ProjectsViewModel>();
            navigationService = provider.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public ProjectsViewModel ViewModel { get; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var nameWithOwner = e.Parameter as string;
            var nameAndOwner = nameWithOwner.Split("/");

            var currentItem = navigationService.TabView.SelectedItem.NavigationHistory.CurrentItem;
            currentItem.Header = "Projects";
            currentItem.Description = "Projects";
            currentItem.Url = $"https://github.com/{nameAndOwner[0]}/{nameAndOwner[1]}/projects";
            currentItem.DisplayUrl = $"{nameAndOwner[0]} / {nameAndOwner[1]} / Projects";
            currentItem.Icon = new muxc.ImageIconSource
            {
                ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Icons/Projects.targetsize-96.png"))
            };

            var command = ViewModel.LoadProjectsPageCommand;
            if (command.CanExecute(nameWithOwner))
                command.Execute(nameWithOwner);
        }
    }
}
