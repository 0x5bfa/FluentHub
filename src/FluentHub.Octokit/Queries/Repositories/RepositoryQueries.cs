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
                    OwnerAvatarUrl = x.Owner.AvatarUrl(100),
                    OwnerLoginName = x.Owner.Login,

                    //PrimaryLanguageName = x.PrimaryLanguage.Name,
                    //PrimaryLanguageColor = x.PrimaryLanguage.Color,
                    x.StargazerCount,

                    //LicenseName = x.LicenseInfo.Name,
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

            item.Name = response.Name;
            item.Owner = response.OwnerLoginName;
            item.OwnerAvatarUrl = response.OwnerAvatarUrl;
            item.Description = response.Description;
            item.StargazerCount = response.StargazerCount;

            //item.PrimaryLangName = response.PrimaryLanguageName;
            //item.PrimaryLangColor = response.PrimaryLanguageColor;

            //item.LicenseName = res.LicenseName;
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
