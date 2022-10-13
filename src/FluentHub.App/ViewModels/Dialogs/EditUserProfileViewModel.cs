using FluentHub.App.Utils;
using FluentHub.App.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.App.ViewModels.UserControls.BlockButtons;

namespace FluentHub.App.ViewModels.Dialogs
{
    public class EditUserProfileViewModel
    {
        private async Task LoadUserAsync(string login)
        {
            try
            {
                UserQueries queries = new();
                var response = await queries.GetAsync(login);
                if (response == null) return;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
