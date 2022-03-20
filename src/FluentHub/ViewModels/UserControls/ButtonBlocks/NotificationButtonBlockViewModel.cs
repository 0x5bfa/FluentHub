using FluentHub.Helpers;
using FluentHub.Models.Items;
using FluentHub.OctokitEx.Queries.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace FluentHub.ViewModels.UserControls.ButtonBlocks
{
    public class NotificationButtonBlockViewModel : INotifyPropertyChanged
    {
        public Octokit.Notification NotificationItem { get; set; } = new();

        private string nameWithOwner;
        public string NameWithOwner { get => nameWithOwner; set => SetProperty(ref nameWithOwner, value); }

        private string updatedAtHumanized;
        public string UpdatedAtHumanized { get => updatedAtHumanized; set => SetProperty(ref updatedAtHumanized, value); }

        private string stateGlyph;
        public string StateGlyph { get => stateGlyph; set => SetProperty(ref stateGlyph, value); }

        private Brush stateGlyphForeground;
        public Brush StateGlyphForeground { get => stateGlyphForeground; set => SetProperty(ref stateGlyphForeground, value); }

        public async Task SetStateContents()
        {
            if (!string.IsNullOrEmpty(NotificationItem.Subject.Type))
            {
                switch (NotificationItem.Subject.Type)
                {
                    case "Issue":
                        IssueQueries issueQueries = new();
                        var issue = await issueQueries.GetOverview(
                            NotificationItem.Repository.Owner.Login,
                            NotificationItem.Repository.Name,
                            Convert.ToInt32(NotificationItem.Subject.Url.Split("/")[NotificationItem.Subject.Url.Split("/").Count() - 1]));
                        NameWithOwner += $" #{issue.Number}";
                        if (issue.IsClosed)
                        {
                            StateGlyph = "\uE9E6";
                            StateGlyphForeground = ColorHelpers.HexCodeToSolidColorBrush("#986EE2");
                        }
                        else
                        {
                            StateGlyph = "\uE9EA";
                            StateGlyphForeground = ColorHelpers.HexCodeToSolidColorBrush("#57AB5A");
                        }
                        break;
                    case "PullRequest":
                        PullRequestQueries pullQueries = new();
                        var pull = await pullQueries.GetOverview(
                            NotificationItem.Repository.Owner.Login,
                            NotificationItem.Repository.Name,
                            Convert.ToInt32(NotificationItem.Subject.Url.Split("/")[NotificationItem.Subject.Url.Split("/").Count() - 1]));
                        NameWithOwner += $" #{pull.Number}";
                        if (pull.IsClosed)
                        {
                            if (pull.IsMerged)
                            {
                                StateGlyph = "\uE9BD";
                                StateGlyphForeground = ColorHelpers.HexCodeToSolidColorBrush("#986EE2");
                            }
                            else
                            {
                                StateGlyph = "\uE9C1";
                                StateGlyphForeground = ColorHelpers.HexCodeToSolidColorBrush("#E5534B");
                            }
                        }
                        else
                        {
                            if (pull.IsDraft)
                            {
                                StateGlyph = "\uE9C3";
                                StateGlyphForeground = ColorHelpers.HexCodeToSolidColorBrush("#768390");
                            }
                            else
                            {
                                StateGlyph = "\uE9BF";
                                StateGlyphForeground = ColorHelpers.HexCodeToSolidColorBrush("#57AB5A");
                            }
                        }
                        break;
                    case "Discussion":
                        StateGlyph = "\uE95D";
                        StateGlyphForeground = ColorHelpers.HexCodeToSolidColorBrush("#768390");
                        break;
                    case "Commit":
                        StateGlyph = "\uE9B9";
                        StateGlyphForeground = ColorHelpers.HexCodeToSolidColorBrush("#768390");
                        break;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }

            return false;
        }
    }
}
