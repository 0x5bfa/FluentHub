using FluentHub.Octokit.Models;
using Octokit.GraphQL;
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

        public async Task<List<Organization>> GetAllAsync(string login)
        {
            #region queries
            var query = new Query()
                .User(login)
                .Organizations(first: 30)
                .Nodes
                .Select(x => new Organization
                {
                    AvatarUrl = x.AvatarUrl(100),
                    Description = x.Description,
                    Name = x.Name,
                    Login = x.Login,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
