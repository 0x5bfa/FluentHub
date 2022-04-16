using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml.Controls;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FluentHub.Helpers;

namespace FluentHub.UserControls.Labels
{
    public sealed partial class StateLabel : UserControl
    {
        #region propdp
        public static readonly DependencyProperty StatusProperty
            = DependencyProperty.Register(
                  nameof(Status),
                  typeof(Status),
                  typeof(StateLabel),
                  new PropertyMetadata(null)
                );

        public Status Status
        {
            get => (Status)GetValue(StatusProperty);
            set { SetValue(StatusProperty, value); SetContents(); }
        }
        #endregion

        public StateLabel()
        {
            this.InitializeComponent();
        }

        private void SetContents()
        {
            switch (Status)
            {
                case Status.IssueOpened: // #1a7f37 [github light color bind theme]
                    LabelBackground.Background = ColorHelpers.HexCodeToSolidColorBrush("#347d39");
                    StateLabelFont.Glyph = "\uE9EA";
                    StateLabelTextBlock.Text = "Open";
                    break;
                case Status.IssueClosed: // #8250df [github light color bind theme]
                    LabelBackground.Background = ColorHelpers.HexCodeToSolidColorBrush("#8256d0");
                    StateLabelFont.Glyph = "\uE9E6";
                    StateLabelTextBlock.Text = "Closed";
                    break;

                case Status.PullOpened: // #1a7f37 [github light color bind theme]
                    LabelBackground.Background = ColorHelpers.HexCodeToSolidColorBrush("#347d39");
                    StateLabelFont.Glyph = "\uE9BF";
                    StateLabelTextBlock.Text = "Open";
                    break;
                case Status.PullClosed: // #cf222e [github light color bind theme]
                    LabelBackground.Background = ColorHelpers.HexCodeToSolidColorBrush("#c93c37");
                    StateLabelFont.Glyph = "\uE9C1";
                    StateLabelTextBlock.Text = "Closed";
                    break;
                case Status.PullMerged: // #8250df [github light color bind theme]
                    LabelBackground.Background = ColorHelpers.HexCodeToSolidColorBrush("#8256d0");
                    StateLabelFont.Glyph = "\uE9BD";
                    StateLabelTextBlock.Text = "Marged";
                    break;
                case Status.PullDraft: // #57606a [github light color bind theme]
                    LabelBackground.Background = ColorHelpers.HexCodeToSolidColorBrush("#636e7b");
                    StateLabelFont.Glyph = "\uE9C3";
                    StateLabelTextBlock.Text = "Draft";
                    break;
            }
        }
    }

    public enum Status
    {
        IssueOpened,
        IssueClosed,
        PullOpened,
        PullClosed,
        PullMerged,
        PullDraft,
    }
}
