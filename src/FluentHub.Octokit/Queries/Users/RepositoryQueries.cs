using Octokit.GraphQL;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Users
{
    public class RepositoryQueries
    {
        public RepositoryQueries() => new App();

        public async Task<List<Models.Repository>> GetAllAsync(string login)
        {
            #region query
            var query = new Query()
                    .User(login)
                    .Repositories(first: 30)
                    .Nodes
                    .Select(x => new
                    {
                        x.Name,
                        x.Description,
                        Owner = x.Owner.Select(y => new
                        {
                            AvatarUrl = y.AvatarUrl(100),
                            y.Login,
                        }),

                        PrimaryLanguage = x.PrimaryLanguage.Select(language => new { language.Name, language.Color }),
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

            var result = await App.Connection.Run(query);

            #region copying
            List<Models.Repository> items = new();

            foreach (var res in result)
            {
                Models.Repository item = new();
                var repository = await App.Client.Repository.Get(res.Owner?.Single().Login, res.Name);

                item.PrimaryLangName = res.PrimaryLanguage?.Single().Name;
                item.PrimaryLangColor = res.PrimaryLanguage?.Single().Color;

                item.Name = res.Name;
                item.Owner = res.Owner?.Single().Login;
                item.OwnerAvatarUrl = res.Owner?.Single().Login;
                item.Description = res.Description;
                item.StargazerCount = res.StargazerCount;

                item.LicenseName = res.LicenseName;
                item.ForkCount = res.ForkCount;
                item.IssueCount = res.IssueCount;
                item.PullCount = res.PullCount;
                item.UpdatedAt = res.UpdatedAt;
                item.WatcherCount = res.WatcherCount;
                item.DefaultBranchName = res.DefaultBranchName;

                item.CloneUrl = repository.CloneUrl;
                item.SshUrl = repository.SshUrl;
                item.GitUrl = repository.GitUrl;

                items.Add(item);
            }
            #endregion

            return items;
        }
    }
}
