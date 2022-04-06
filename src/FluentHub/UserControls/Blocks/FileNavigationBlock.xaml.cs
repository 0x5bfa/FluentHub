using FluentHub.ViewModels;
using FluentHub.ViewModels.Repositories;
using FluentHub.Views.Repositories.Codes.Layouts;
using FluentHub.ViewModels.UserControls.Blocks;
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

namespace FluentHub.UserControls.Blocks
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
            set => SetValue(ContextViewModelProperty, value);
        }
        #endregion

        public FileNavigationBlock()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<FileNavigationBlockViewModel>();
        }

        public FileNavigationBlockViewModel ViewModel { get; }
        private bool FirstSelectionComplete { get; set; }

        #region chevronamination
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
            ViewModel.ContextViewModel = ContextViewModel;

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

            ContextViewModel.BranchName = BranchNameSelector.SelectedItem as string;

            MainPageViewModel.RepositoryContentFrame.Navigate(typeof(DetailsLayoutView), ContextViewModel);
        }
    }
}
