using Humanizer;
using Octokit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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


namespace FluentHub.UserControls.Block
{
    public sealed partial class IssueBlock : Windows.UI.Xaml.Controls.Page
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

        public static readonly DependencyProperty IssueIndexProperty
            = DependencyProperty.Register(
                  nameof(IssueIndex),
                  typeof(int),
                  typeof(RepoBlock),
                  new PropertyMetadata(null)
                );

        public int IssueIndex
        {
            get => (int)GetValue(IssueIndexProperty);
            set => SetValue(IssueIndexProperty, value);
        }

        private ObservableCollection<LabelItem> _items = new ObservableCollection<LabelItem>();

        public IssueBlock()
        {
            this.InitializeComponent();
        }

        private void IssueBlockButton_Click(object sender, RoutedEventArgs e)
        {

        }

        public SolidColorBrush GetSolidColorBrush(string hex)
        {
            var r = (byte)Convert.ToUInt32(hex.Substring(0, 2), 16);
            var g = (byte)Convert.ToUInt32(hex.Substring(2, 2), 16);
            var b = (byte)Convert.ToUInt32(hex.Substring(4, 2), 16);
            var brush = new SolidColorBrush(Color.FromArgb(0xFF, r, g, b));
            return brush;
        }

        private async void ItemsRepeater_Loaded(object sender, RoutedEventArgs e)
        {
            var issue = await App.Client.Issue.Get(RepositoryId, IssueIndex);

            if (issue.State == ItemState.Closed)
            {
                StatusFontGlyph.Foreground = new SolidColorBrush(Color.FromArgb(0xFF, 0x98, 0x6E, 0xE2)); // #986EE2
                StatusFontGlyph.Glyph = "\uE9E6";
            }

            // #57AB5A - issue/PR opened
            // #768390 - draft
            // #E5534B - PR closed

            var repo = await App.Client.Repository.Get(RepositoryId);

            RepoNameWithOwnerAndNumber.Text = repo.Owner + " / " + repo.Name + " #" + issue.Number;

            UpdatedAtHumanized.Text = issue.UpdatedAt.Humanize();

            TitleTextBlock.Text = issue.Title;

            foreach (var label in issue.Labels)
            {
                LabelItem labelItem = new LabelItem();

                var brush = GetSolidColorBrush(label.Color);

                labelItem.AccentColor = brush;

                labelItem.LabelText = label.Name;

                _items.Add(labelItem);
            }
        }
    }

    public class LabelItem
    {
        public string LabelText { get; set; }
        public Brush AccentColor { get; set; }
    }

}
