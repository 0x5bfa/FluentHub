using FluentHub.Octokit.Models;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Users
{
    public class OrganizationQueries
    {
        public OrganizationQueries() => new App();

        public async Task<List<Models.Organization>> GetAllAsync(string login)
        {
            #region queries
            var query = new Query()
                .User(login)
                .Organizations(first: 30)
                .Nodes
                .Select(x => new
                {
                    AvatarUrl = x.AvatarUrl(100),
                    x.Description,
                    x.Name,
                    x.Login,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            #region copying
            List<Models.Organization> items = new();

            foreach (var res in response)
            {
                Models.Organization item = new();

                item.Name = res.Name;
                item.Login = res.Login;
                item.AvatarUrl = res.AvatarUrl;
                item.Description = res.Description;

                items.Add(item);
            }
            #endregion

            return items;
        }
    }
}
