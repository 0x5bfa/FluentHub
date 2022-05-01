using FluentHub.Octokit.Models;
using Humanizer;
using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLCore = global::Octokit.GraphQL.Core;
using GraphQLModel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Queries.Organizations
{
    public class OrganizationQueries
    {
        public OrganizationQueries() => new App();

        public async Task<Organization> GetOverview(string org)
        {
                #region query
                var query = new Query()
                        .Organization(org)
                        .Select(x => new Organization
                        {
                            AvatarUrl = x.AvatarUrl(100),
                            Description = x.Description,
                            Email = x.Email,
                            IsVerified = x.IsVerified,
                            Location = x.Location,
                            Login = x.Login,
                            Name = x.Name,
                            WebsiteUrl = x.WebsiteUrl,
                        })
                        .Compile();
                #endregion

                var response = await App.Connection.Run(query);

                return response;
        }
    }
}
