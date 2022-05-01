using FluentHub.Octokit.Models;
using FluentHub.Octokit.Queries.Repositories;
using FluentHub.ViewModels.UserControls.ButtonBlocks;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Threading.Tasks;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class ActivityBlockViewModel : ObservableObject
    {
        private Activity _payload;
        public Activity Payload { get => _payload; set => SetProperty(ref _payload, value); }

        //private RepoButtonBlockViewModel repoBlockViewModel;
        //public RepoButtonBlockViewModel RepoBlockViewModel { get => repoBlockViewModel; set => SetProperty(ref repoBlockViewModel, value); }

        //private UserButtonBlockViewModel userBlockViewModel;
        //public UserButtonBlockViewModel UserBlockViewModel { get => userBlockViewModel; set => SetProperty(ref userBlockViewModel, value); }

        //private CommitActivityBlockViewModel commitBlockViewModel;
        //public CommitActivityBlockViewModel CommitBlockViewModel { get => commitBlockViewModel; set => SetProperty(ref commitBlockViewModel, value); }

        public void LoadContents()
        {
        }
    }
}
