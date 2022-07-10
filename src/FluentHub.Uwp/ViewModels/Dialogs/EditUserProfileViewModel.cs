using FluentHub.Core;
using FluentHub.Octokit.Models;
using FluentHub.Uwp.Models;
using FluentHub.Octokit.Queries.Users;
using FluentHub.Uwp.ViewModels.UserControls.ButtonBlocks;
using Humanizer;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

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
