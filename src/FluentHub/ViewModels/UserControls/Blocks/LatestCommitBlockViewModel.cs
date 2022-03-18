using FluentHub.OctokitEx.Models;
using FluentHub.OctokitEx.Queries.Repository;
using FluentHub.ViewModels.Repositories;
using Humanizer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class LatestCommitBlockViewModel : INotifyPropertyChanged
    {
        private CommonRepoViewModel commonRepoViewModel;
        public CommonRepoViewModel CommonRepoViewModel { get => commonRepoViewModel; set => SetProperty(ref commonRepoViewModel, value); }

        private CommitOverviewItem commitOverviewItem;
        public CommitOverviewItem CommitOverviewItem { get => commitOverviewItem; set => SetProperty(ref commitOverviewItem, value); }

        private string commitUpdatedAtHumanized;
        public string CommitUpdatedAtHumanized { get => commitUpdatedAtHumanized; set => SetProperty(ref commitUpdatedAtHumanized, value); }

        private string commitMessageHeadline;
        public string CommitMessageHeadline { get => commitMessageHeadline; set => SetProperty(ref commitMessageHeadline, value); }

        private bool hasMoreCommitMessage;
        public bool HasMoreCommitMessage { get => hasMoreCommitMessage; set => SetProperty(ref hasMoreCommitMessage, value); }

        private string subCommitMessages;
        public string SubCommitMessages { get => subCommitMessages; set => SetProperty(ref subCommitMessages, value); }

        public async Task GetCommitDetails()
        {
            if (CommonRepoViewModel == null) return;

            CommitQueries queries = new();
            CommitOverviewItem = await queries.GetOverview(CommonRepoViewModel.Name, CommonRepoViewModel.Owner, CommonRepoViewModel.BranchName, CommonRepoViewModel.Path);

            CommitUpdatedAtHumanized = CommitOverviewItem.CommittedDate.Humanize();

            var commitMessageLines = CommitOverviewItem.CommitMessage.Split("\n");
            CommitMessageHeadline = commitMessageLines[0];

            // prepare
            if (commitMessageLines.Count() > 1)
            {
                HasMoreCommitMessage = true;

                if(commitMessageLines[1] == "")
                {
                    var messageTextLinesList = commitMessageLines.ToList();
                    messageTextLinesList.RemoveAt(1);
                    commitMessageLines = messageTextLinesList.ToArray();
                }

                SubCommitMessages = string.Join('\n', commitMessageLines, 1, commitMessageLines.Count() - 1);
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
