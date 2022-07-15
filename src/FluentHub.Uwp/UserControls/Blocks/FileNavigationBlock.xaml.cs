using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.ViewModels.UserControls.Blocks;
using FluentHub.Uwp.Views.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using muxc = Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls.Blocks
{
    public sealed partial class FileNavigationBlock : UserControl
    {
        #region propdp
        public static readonly DependencyProperty ContextViewModelProperty =
            DependencyProperty.Register(
                nameof(ContextViewModel),
                typeof(RepoContextViewModel),
                typeof(FileNavigationBlock),
                new PropertyMetadata(null));

        public RepoContextViewModel ContextViewModel
        {
            get => (RepoContextViewModel)GetValue(ContextViewModelProperty);
            set
            {
                SetValue(ContextViewModelProperty, value);
                if (ContextViewModel is not null)
                    ViewModel.ContextViewModel = ContextViewModel;
            }
        }
        #endregion

        public FileNavigationBlock()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<FileNavigationBlockViewModel>();
            navigationService = App.Current.Services.GetRequiredService<INavigationService>();
        }

        private readonly INavigationService navigationService;
        public FileNavigationBlockViewModel ViewModel { get; }
        private bool FirstSelectionComplete { get; set; }

        #region Chevron Amination
        private void OnCloneButtonLoaded(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            button.AddHandler(PointerPressedEvent, new PointerEventHandler(OnCloneButtonPointerPressed), true);
            button.AddHandler(PointerReleasedEvent, new PointerEventHandler(OnCloneButtonPointerReleased), true);
        }

        private void OnCloneButtonPointerPressed(object sender, PointerRoutedEventArgs e) => SetState(sender as UIElement, "Pressed");

        private void OnCloneButtonPointerReleased(object sender, PointerRoutedEventArgs e) => SetState(sender as UIElement, "Normal");

        public void SetState(UIElement target, string state)
        {
            if (target != null)
            {                
                muxc.AnimatedIcon.SetState(target, state);
            }
        }
        #endregion

        private void OnFileNavigationBlockLoaded(object sender, RoutedEventArgs e)
        {
            var command = ViewModel.LoadBranchNameAllCommand;
            if (command.CanExecute(null))
                command.Execute(null);

            // Default selected branch name is current branch
            BranchNameSelector.SelectedIndex = 0;
        }

        private void OnBranchSelectorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FirstSelectionComplete == false)
            {
                FirstSelectionComplete = true;
                return;
            }

            ViewModel.ContextViewModel.BranchName = ContextViewModel.BranchName = BranchNameSelector.SelectedItem as string;

            var objType = ViewModel.ContextViewModel.IsFile ? "blob" : "tree";
            var path = string.IsNullOrEmpty(ViewModel.ContextViewModel.Path) ? $"{ViewModel.ContextViewModel.Path}" : $"/{ViewModel.ContextViewModel.Path}";
            navigationService.Navigate<OverviewPage>($"{App.DefaultGitHubDomain}/{ViewModel.ContextViewModel.Repository.Owner.Login}/{ViewModel.ContextViewModel.Repository.Name}/{objType}/{ViewModel.ContextViewModel.BranchName}{path}");
        }
    }
}
