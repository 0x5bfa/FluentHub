using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable enable

namespace FluentHub.Octokit.Queries.Users
{
    public class PinnedItemQueries
    {
        public PinnedItemQueries() => new App();

        public async Task<List<Models.Repository>> GetAllAsync(string login, bool isUser)
        {
            try
            {
                #region userquery
                var usersQuery = new Query()
                        .User(login)
                        .PinnedItems(first: 6)
                        .Nodes
                        .OfType<Repository>()
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

                #region orgquery
                var orgsQuery = new Query()
                        .Organization(login)
                        .PinnedItems(first: 6)
                        .Nodes
                        .OfType<Repository>()
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

                var result = await App.Connection.Run(isUser ? usersQuery : orgsQuery);

                #region copying
                List<Models.Repository> items = new();

                foreach (var res in result)
                {
                    Models.Repository item = new();
                    var repository = await App.Client.Repository.Get(res.OwnerLoginName, res.Name);

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
