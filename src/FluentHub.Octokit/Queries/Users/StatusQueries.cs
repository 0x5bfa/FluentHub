using FluentHub.Octokit.Models;
using Humanizer;
using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Users
{
    public class StatusQueries
    {
        public StatusQueries() => new App();

        public async Task<UserStatus> GetAsync(string login)
        {
            var query = new Query()
                .User(login)
                .Status
                .Select(x => new UserStatus
                {
                    Emoji = x.Emoji,
                    ExpiresAt = x.ExpiresAt,
                    Message = x.Message,
                    UpdatedAt = x.UpdatedAt,
                });

            var response = await App.Connection.Run(query);

            return response;
        }
    }
}
