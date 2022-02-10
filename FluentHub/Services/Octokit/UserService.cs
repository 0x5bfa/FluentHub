using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Services.Octokit
{
    public class UserService
    {
        public static async Task<User> Get(string login)
        {
            try
            {
                return await App.Client.User.Get(login);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
