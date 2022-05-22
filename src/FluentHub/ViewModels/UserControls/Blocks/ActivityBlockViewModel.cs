using FluentHub.ViewModels.UserControls.ButtonBlocks;
using System.Collections.ObjectModel;
using Windows.UI.Xaml.Controls;

namespace FluentHub.ViewModels.UserControls.Blocks
{
    public class ActivityBlockViewModel : ObservableObject
    {
        public ActivityBlockViewModel()
        {
            _payloadContentItems = new();
            PayloadContentItems = new(_payloadContentItems);
        }

        private Activity _payload;
        public Activity Payload { get => _payload; set => SetProperty(ref _payload, value); }

        private RepoButtonBlockViewModel repoBlockViewModel;
        public RepoButtonBlockViewModel RepoBlockViewModel { get => repoBlockViewModel; set => SetProperty(ref repoBlockViewModel, value); }

        private UserButtonBlockViewModel userBlockViewModel;
        public UserButtonBlockViewModel UserBlockViewModel { get => userBlockViewModel; set => SetProperty(ref userBlockViewModel, value); }

        private readonly ObservableCollection<object> _payloadContentItems;
        public ReadOnlyObservableCollection<object> PayloadContentItems { get; }

        //private CommitActivityBlockViewModel commitBlockViewModel;
        //public CommitActivityBlockViewModel CommitBlockViewModel { get => commitBlockViewModel; set => SetProperty(ref commitBlockViewModel, value); }

        public async Task LoadContentsAsync()
        {
            Octokit.Queries.Repositories.RepositoryQueries repositoryQueries = new();
            Octokit.Queries.Users.UserQueries userQueries = new();

            if (Payload.PayloadType == "WatchEvent" || Payload.PayloadType == "ForkEvent")
            {
                var response = await repositoryQueries.GetAsync(Payload.Repository.Owner.Login, Payload.Repository.Name);

                RepoBlockViewModel = new()
                {
                    DisplayDetails = true,
                    DisplayStarButton = true,
                    Item = response,
                };
            }
            else if (Payload.PayloadType == "FollowEvent") // Never gonna happen
            {
                var response = await userQueries.GetAsync(Payload.Actor.Login);

                UserBlockViewModel = new()
                {
                    User = response,
                };
            }

            TextBlock textblock = new();
            textblock.Text = Payload.PayloadType;

            _payloadContentItems.Add(textblock);
            _payloadContentItems.Add(textblock);
        }
    }
}
