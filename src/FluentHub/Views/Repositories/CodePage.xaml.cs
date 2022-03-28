using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using FluentHub.ViewModels.Repositories;

namespace FluentHub.Views.Repositories
{
    public sealed partial class CodePage : Page
    {
        private CommonRepoViewModel CommonRepoViewModel { get; set; }

        public CodePage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CommonRepoViewModel = e.Parameter as CommonRepoViewModel;

            CodeViewLayout.Navigate(typeof(Layouts.DetailsLayoutView), CommonRepoViewModel);
        }
    }
}
