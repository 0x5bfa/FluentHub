using Octokit.GraphQL;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.OctokitEx.Queries.User
{
    public class StarredRepoQueries
    {
        public StarredRepoQueries() => new App();

        public async Task<List<Models.RepositoryBlockItem>> GetOverviewAll(string login)
        {
            var query = new Query()
                    .User(login)
                    .StarredRepositories(first: 30)
                    .Nodes
                    .Select(x => new
                    {
                        x.Name,
                        x.Description,
                        Owner = x.Owner.Select(y => y.Login).Single(),
                        PrimaryLanguage = x.Languages(1, null, null, null, null).Nodes.Select(language => new { language.Name, language.Color }).ToList(),
                        x.StargazerCount,
                        LicenseName = x.LicenseInfo.Select(license => license.Name).Single(),
                        x.ForkCount,
                        IssueCount = x.Issues(null, null, null, null, null, null, null, null).TotalCount,
                        PullCount = x.PullRequests(null, null, null, null, null, null, null, null, null).TotalCount,
                        x.UpdatedAt,
                    })
                    .Compile();

            List<Models.RepositoryBlockItem> items = new();

            var result = await App.Connection.Run(query);

            foreach (var res in result)
            {
                Models.RepositoryBlockItem item = new();

                item.Description = res.Description;

                if (res.PrimaryLanguage != null && res.PrimaryLanguage.Count() != 0)
                {
                    item.PrimaryLangName = res.PrimaryLanguage[0].Name;
                    item.PrimaryLangColor = res.PrimaryLanguage[0].Color;
                }

                item.Owner = res.Owner;
                item.Name = res.Name;
                item.StargazerCount = res.StargazerCount;

                item.LicenseName = res.LicenseName;
                item.ForkCount = res.ForkCount;
                item.IssueCount = res.IssueCount;
                item.PullCount = res.PullCount;
                item.UpdatedAt = res.UpdatedAt;

                items.Add(item);
            }

            return items;
        }
    }
}
