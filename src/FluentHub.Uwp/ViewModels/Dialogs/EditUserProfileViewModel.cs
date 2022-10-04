using FluentHub.Uwp.Utils;
using FluentHub.Uwp.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.ViewModels.UserControls.BlockButtons;

namespace FluentHub.Uwp.ViewModels.Dialogs
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
