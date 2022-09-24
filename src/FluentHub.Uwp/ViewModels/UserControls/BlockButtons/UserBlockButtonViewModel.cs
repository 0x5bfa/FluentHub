using FluentHub.Uwp.Helpers;
using FluentHub.Uwp.Models;
using FluentHub.Uwp.Utils;

namespace FluentHub.Uwp.ViewModels.UserControls.BlockButtons
{
    public class UserBlockButtonViewModel : ObservableObject
    {
        private User _user;
        public User User { get => _user; set => SetProperty(ref _user, value); }
    }
}
