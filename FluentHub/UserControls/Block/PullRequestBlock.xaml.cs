using FluentHub.Views.RepoPages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Control de usuario está documentada en https://go.microsoft.com/fwlink/?LinkId=234236

namespace FluentHub.UserControls.Block
{
    public sealed partial class PullRequestBlock : Windows.UI.Xaml.Controls.Page
    {
        public static readonly DependencyProperty RepositoryIdProperty
            = DependencyProperty.Register(
                  nameof(RepositoryId),
                  typeof(long),
                  typeof(RepoBlock),
                  new PropertyMetadata(null)
                );

        public long RepositoryId
        {
            get => (long)GetValue(RepositoryIdProperty);
            set => SetValue(RepositoryIdProperty, value);
        }

        public static readonly DependencyProperty PRIndexProperty
    = DependencyProperty.Register(
          nameof(PRIndex),
          typeof(int),
          typeof(RepoBlock),
          new PropertyMetadata(null)
        );

        public int PRIndex
        {
            get => (int)GetValue(PRIndexProperty);
            set => SetValue(PRIndexProperty, value);
        }

        private ObservableCollection<LabelItem> _items = new ObservableCollection<LabelItem>();


        public PullRequestBlock()
        {
            this.InitializeComponent();
        }


        private void PRBlockButton_Click(object sender, RoutedEventArgs e)
        {
            string param = RepositoryId + "/" + PRIndex;
            App.MainViewModel.RepoMainFrame.Navigate(typeof(PullRequestPage), param);
        }

        private void ItemsRepeater_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
