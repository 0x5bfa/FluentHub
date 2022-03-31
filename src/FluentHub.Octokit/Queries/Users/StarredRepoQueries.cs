using Octokit.GraphQL;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Users
{
    public class StarredRepoQueries
    {
        public StarredRepoQueries() => new App();

        public async Task<List<Models.Repository>> GetAllAsync(string login)
        {
            #region query
            var query = new Query()
                    .User(login)
                    .StarredRepositories(first: 30)
                    .Nodes
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

            var result = await App.Connection.Run(query);

            #region copying
            List<Models.Repository> items = new();

            foreach (var res in result)
            {
                Models.Repository item = new();

                item.Name = res.Name;
                item.Owner = res.OwnerLoginName;
                item.OwnerAvatarUrl = res.OwnerAvatarUrl;
                item.Description = res.Description;
                item.StargazerCount = res.StargazerCount;

                //item.PrimaryLangName = res.PrimaryLanguageName;
                //item.PrimaryLangColor = res.PrimaryLanguageColor;

                //item.LicenseName = res.LicenseName;
                item.ForkCount = res.ForkCount;
                item.IssueCount = res.IssueCount;
                item.PullCount = res.PullCount;
                item.UpdatedAt = res.UpdatedAt;
                item.WatcherCount = res.WatcherCount;
                item.DefaultBranchName = res.DefaultBranchName;

                items.Add(item);
            }
            #endregion

            return items;
        }

        public async Task<List<Models.Repository>> GetAllAsync()
        {
            #region query
            var query = new Query()
                    .Viewer
                    .StarredRepositories(first: 30)
                    .Nodes
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

            var result = await App.Connection.Run(query);

            #region copying
            List<Models.Repository> items = new();

            foreach (var res in result)
            {
                Models.Repository item = new();

                item.Name = res.Name;
                item.Owner = res.OwnerLoginName;
                item.OwnerAvatarUrl = res.OwnerAvatarUrl;
                item.Description = res.Description;
                item.StargazerCount = res.StargazerCount;

                //item.PrimaryLangName = res.PrimaryLanguageName;
                //item.PrimaryLangColor = res.PrimaryLanguageColor;

                //item.LicenseName = res.LicenseName;
                item.ForkCount = res.ForkCount;
                item.IssueCount = res.IssueCount;
                item.PullCount = res.PullCount;
                item.UpdatedAt = res.UpdatedAt;
                item.WatcherCount = res.WatcherCount;
                item.DefaultBranchName = res.DefaultBranchName;

                items.Add(item);
            }
            #endregion

            return items;
        }
    }
}
