using FluentHub.Octokit.Models;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.User
{
    public class OrganizationQueries
    {
        public OrganizationQueries() => new App();

        public async Task<List<OrganizationOverviewItem>> GetOverviewAll(string login)
        {
            IssueOrder order = new() { Direction = OrderDirection.Desc, Field = IssueOrderField.CreatedAt };

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

            List<OrganizationOverviewItem> items = new();

            foreach (var res in response)
            {
                OrganizationOverviewItem item = new();

                item.Name = res.Name;
                item.Login = res.Login;
                item.AvatarUrl = res.AvatarUrl;
                item.Description = res.Description;

                items.Add(item);
            }

            return items;
        }
    }
}
