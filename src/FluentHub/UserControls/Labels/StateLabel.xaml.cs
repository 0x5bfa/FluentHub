using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace FluentHub.UserControls.Labels
{
    public sealed partial class StateLabel : UserControl
    {
        #region propdp
        public static readonly DependencyProperty StatusProperty =
            DependencyProperty.Register(
                nameof(SubjectType),
                typeof(SubjectType),
                typeof(StateLabel),
                new PropertyMetadata(null));

        public SubjectType SubjectType
        {
            get => (SubjectType)GetValue(StatusProperty);
            set
            {
                SetValue(StatusProperty, value);
                ViewModel.LoadContents(SubjectType.ToString());
            }
        }
        #endregion

        public StateLabel() => InitializeComponent();
    }

    public enum SubjectType
    {
        IssueOpen,
        IssueClosed,
        PullOpen,
        PullClosed,
        PullMerged,
        PullDraft,
    }
}
