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
using Windows.UI.Xaml.Navigation;

namespace FluentHub.UserControls
{
    public sealed partial class RepositoryTreeContentBlock : UserControl
    {
        #region RepositoryIdProperty
        public static readonly DependencyProperty RepositoryIdProperty
            = DependencyProperty.Register(
                  nameof(RepositoryId),
                  typeof(long),
                  typeof(RepositoryTreeContentBlock),
                  new PropertyMetadata(null)
                );

        public long RepositoryId
        {
            get => (long)GetValue(RepositoryIdProperty);
            set
            {
                SetValue(RepositoryIdProperty, value);
                GetSources();
            }
        }
        #endregion

        #region PathProperty
        public static readonly DependencyProperty PathProperty
            = DependencyProperty.Register(
                  nameof(Path),
                  typeof(string),
                  typeof(RepositoryTreeContentBlock),
                  new PropertyMetadata(null)
                );

        public string Path
        {
            get => (string)GetValue(PathProperty);
            set => SetValue(PathProperty, value);
        }
        #endregion

        public RepositoryTreeContentBlock()
        {
            this.InitializeComponent();
        }

        public async void GetSources()
        {
            if (RepositoryId == 0) return;

            RepositoryLatestCommitOverview.RepositoryId = RepositoryId;
            RepositoryRootReadmeContentBlock.RepositoryId = RepositoryId;
        }
    }
}
