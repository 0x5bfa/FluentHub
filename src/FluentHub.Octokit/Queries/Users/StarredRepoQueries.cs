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
            try
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

                var result = await App.Connection.Run(query);

                #region copying
                List<Models.Repository> items = new();

                foreach (var res in result)
                {
                    Models.Repository item = new();
                    var repository = await App.Client.Repository.Get(res.Owner.Login, res.Name);

                    if (res.PrimaryLanguage != null && res.PrimaryLanguage.Count() != 0)
                    {
                        item.PrimaryLangName = res.PrimaryLanguage[0].Name;
                        item.PrimaryLangColor = res.PrimaryLanguage[0].Color;
                    }

                    item.Name = res.Name;
                    item.Owner = res.Owner.Login;
                    item.OwnerAvatarUrl = res.Owner.AvatarUrl;
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
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return null;
            }
        }
    }
}
