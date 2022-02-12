using Humanizer;
using FluentHub.Services.OctokitEx;
using Octokit;
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


namespace FluentHub.Views.RepoPages
{
    public sealed partial class CodePage : Windows.UI.Xaml.Controls.Page
    {
        private long RepoId { get; set; }

        public CodePage()
        {
            this.InitializeComponent();
            this.SizeChanged += CodePage_SizeChanged;
        }

        private void CodePage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (e.NewSize.Width >= 1024)
            {
                // Change frame content to treeview
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            RepoId = Convert.ToInt64(e.Parameter as string);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            // Default page layout is detailsview
            CodeViewLayout.Navigate(typeof(Layouts.DetailsLayoutView), RepoId);
        }
    }
}
