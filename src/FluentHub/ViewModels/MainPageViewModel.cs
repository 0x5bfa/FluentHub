using FluentHub.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Input;

namespace FluentHub.ViewModels
{
    public class MainPageViewModel
    {
        private readonly INavigationService navigationService;

        #region commands
        public ICommand AddNewTabAcceleratorCommand { get; private set; }
        public ICommand CloseTabAcceleratorCommand { get; private set; }
        public ICommand GoToNextTabAcceleratorCommand { get; private set; }
        public ICommand GoToPreviousTabAcceleratorCommand { get; private set; }
        public ICommand AddNewTabWithMouseAcceleratorCommand { get; private set; }
        public ICommand CloseTabWithMouseAcceleratorCommand { get; private set; }

        public ICommand GoBackCommand { get; private set; }
        public ICommand GoForwardCommand { get; private set; }
        public ICommand GoHomeCommand { get; private set; }
        #endregion

        public MainPageViewModel()
        {
            navigationService = App.Current.Services.GetService<INavigationService>();

            AddNewTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(AddNewTabAccelerator);
            CloseTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(CloseTabAccelerator);
            GoToNextTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(GoToNextTabAccelerator);
            GoToPreviousTabAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(GoToPreviousTabAccelerator);
            AddNewTabWithMouseAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(AddNewTabWithMouseAccelerator);
            CloseTabWithMouseAcceleratorCommand = new RelayCommand<KeyboardAcceleratorInvokedEventArgs>(CloseTabWithMouseAccelerator);

            GoBackCommand = new RelayCommand(GoBack);
            GoForwardCommand = new RelayCommand(GoForward);
            GoHomeCommand = new RelayCommand(GoHome);
        }

        #region command-methods
        private void AddNewTabAccelerator(KeyboardAcceleratorInvokedEventArgs e)
        {
            navigationService.OpenTab<Views.Home.UserHomePage>();
            e.Handled = true;
        }

        private void CloseTabAccelerator(KeyboardAcceleratorInvokedEventArgs e)
        {
            navigationService.CloseTab(navigationService.TabView.SelectedItem.Guid);
            e.Handled = true;
        }

        private void GoToNextTabAccelerator(KeyboardAcceleratorInvokedEventArgs e)
        {
            if (navigationService.TabView.SelectedIndex == navigationService.TabView.Items.Count - 1)
            {
                navigationService.TabView.SelectedIndex = 0;
            }
            else
            {
                navigationService.TabView.SelectedIndex++;
            }

            e.Handled = true;
        }

        private void GoToPreviousTabAccelerator(KeyboardAcceleratorInvokedEventArgs e)
        {
            if (navigationService.TabView.SelectedIndex == navigationService.TabView.Items.Count - 1)
            {
                navigationService.TabView.SelectedIndex = 0;
            }
            else
            {
                navigationService.TabView.SelectedIndex--;
            }

            e.Handled = true;
        }

        private void AddNewTabWithMouseAccelerator(KeyboardAcceleratorInvokedEventArgs e)
        {
            navigationService.OpenTab<Views.Home.UserHomePage>();
            e.Handled = true;
        }

        private void CloseTabWithMouseAccelerator(KeyboardAcceleratorInvokedEventArgs e)
        {
            navigationService.CloseTab(navigationService.TabView.SelectedItem.Guid);
            e.Handled = true;
        }

        private void GoBack()
        {
            navigationService.GoBack();
        }

        private void GoForward()
        {
            navigationService.GoForward();
        }

        private void GoHome()
        {
            navigationService.Navigate<Views.Home.UserHomePage>();
        }
        #endregion


    }
}
