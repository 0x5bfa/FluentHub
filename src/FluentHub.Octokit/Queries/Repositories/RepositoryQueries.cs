using FluentHub.Octokit.Models;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Repositories
{
    public class RepositoryQueries
    {
        public RepositoryQueries() => new App();

        public async Task<Models.Repository> Get(string name, string owner)
        {
            #region query
            var query = new Query()
                .Repository(name, owner)
                .Select(x => new
                {
                    x.Name,
                    x.Description,
                    Owner = x.Owner.Select(y => new
                    {
                        AvatarUrl = y.AvatarUrl(100),
                        y.Login,
                    }).Single(),

                    PrimaryLanguage = x.Languages(1, null, null, null, null).Nodes.Select(language => new { language.Name, language.Color }).ToList(),
                    x.StargazerCount,

                    LicenseName = x.LicenseInfo.Select(license => license.Name).Single(),
                    x.ForkCount,
                    IssueCount = x.Issues(null, null, null, null, null, null, null, null).TotalCount,
                    PullCount = x.PullRequests(null, null, null, null, null, null, null, null, null).TotalCount,
                    x.UpdatedAt,

                    WatcherCount = x.Watchers(null, null, null, null).TotalCount,
                    DefaultBranchName = x.DefaultBranchRef.Name,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            #region copying
            Models.Repository item = new();

            if (response.PrimaryLanguage != null && response.PrimaryLanguage.Count() != 0)
            {
                item.PrimaryLangName = response.PrimaryLanguage[0].Name;
                item.PrimaryLangColor = response.PrimaryLanguage[0].Color;
            }

            item.Name = response.Name;
            item.Owner = response.Owner.Login;
            item.OwnerAvatarUrl = response.Owner.AvatarUrl;
            item.Description = response.Description;
            item.StargazerCount = response.StargazerCount;

            item.LicenseName = response.LicenseName;
            item.ForkCount = response.ForkCount;
            item.IssueCount = response.IssueCount;
            item.PullCount = response.PullCount;
            item.UpdatedAt = response.UpdatedAt;
            item.WatcherCount = response.WatcherCount;
            item.DefaultBranchName = response.DefaultBranchName;
            #endregion

            return item;
        }

        // Details such as releases, contributors
        public void GetDetails()
        {
        }
    }
}
