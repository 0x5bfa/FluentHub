using FluentHub.Octokit.Models;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Repository
{
    public class RepositoryQueries
    {
        public RepositoryQueries() => new App();

        public async Task<RepositoryBlockItem> GetOverview(string name, string owner)
        {
            #region queries
            var query = new Query()
                .Repository(name, owner)
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
            #endregion

            var response = await App.Connection.Run(query);

            RepositoryBlockItem item = new();

            item.Description = response.Description;

            if (response.PrimaryLanguage != null && response.PrimaryLanguage.Count() != 0)
            {
                item.PrimaryLangName = response.PrimaryLanguage[0].Name;
                item.PrimaryLangColor = response.PrimaryLanguage[0].Color;
            }

            item.Owner = response.Owner;
            item.Name = response.Name;
            item.StargazerCount = response.StargazerCount;

            item.LicenseName = response.LicenseName;
            item.ForkCount = response.ForkCount;
            item.IssueCount = response.IssueCount;
            item.PullCount = response.PullCount;
            item.UpdatedAt = response.UpdatedAt;

            return item;
        }
    }
}
