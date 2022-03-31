using Octokit.GraphQL.Model;
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

        public async Task<Models.UserStatus> GetAsync(string login)
        {
            var query = new Query()
                .User(login)
                .Status
                .Select(x => new
                {
                    x.Emoji,
                    x.ExpiresAt,
                    x.Message,
                    x.UpdatedAt,
                });

            var result = await App.Connection.Run(query);

            Models.UserStatus item = new();

            item.Emoji = result.Emoji;
            item.ExpiresAt = result.ExpiresAt;
            item.Message = result.Message;
            item.UpdatedAt = result.UpdatedAt;

            return item;
        }

        public async Task<Models.UserStatus> GetAsync()
        {
            var query = new Query()
                .Viewer
                .Status
                .Select(x => new
                {
                    x.Emoji,
                    x.ExpiresAt,
                    x.Message,
                    x.UpdatedAt,
                });

            var result = await App.Connection.Run(query);

            Models.UserStatus item = new();

            item.Emoji = result.Emoji;
            item.ExpiresAt = result.ExpiresAt;
            item.Message = result.Message;
            item.UpdatedAt = result.UpdatedAt;

            return item;
        }
    }
}
