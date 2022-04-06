using Humanizer;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
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

        public async Task<Models.Repository> Get(string owner, string name)
        {
            #region query
            var query = new Query()
                .Repository(name, owner)
                .Select(x => new
                {
                    x.Name,
                    OwnerAvatarUrl = x.Owner.AvatarUrl(100),
                    OwnerLoginName = x.Owner.Login,
                    OwnerId = x.Owner.Id,
                    x.Description,
                    LicenseName = x.LicenseInfo.Select(y => y.Name).SingleOrDefault(),

                    PrimaryLanguage = x.PrimaryLanguage.Select(y => new
                    {
                        y.Name,
                        y.Color,
                    }).SingleOrDefault(),

                    x.StargazerCount,
                    x.ForkCount,
                    OpenIssueCount = x.Issues(null, null, null, null, null, null, null, null).TotalCount,
                    OpenPullCount = x.PullRequests(null, null, null, null, null, null, null, null, null).TotalCount,

                    x.IsFork,
                })
                .Compile();
            #endregion

            var res = await App.Connection.Run(query);

            #region copying
            Models.Repository item = new()
            {
                Name = res.Name,
                Owner = res.OwnerLoginName,
                OwnerAvatarUrl = res.OwnerAvatarUrl,
                OwnerIsOrganization = Helpers.UserTypeDetectionHelper.IsOrganization(res.OwnerId),
                Description = res.Description,

                PrimaryLangName = res.PrimaryLanguage?.Name,
                PrimaryLangColor = res.PrimaryLanguage?.Color,
                LicenseName = res.LicenseName,

                StargazerCount = res.StargazerCount,
                ForkCount = res.ForkCount,
                OpenIssueCount = res.OpenIssueCount,
                OpenPullCount = res.OpenPullCount,

                IsFork = res.IsFork
            };

            #endregion

            return item;
        }

        public async Task<Models.RepositoryDetails> GetDetailsAsync(string owner, string name)
        {
            #region query
            var query = new Query()
                    .Repository(owner: owner, name: name)
                    .Select(x => new
                    {
                        x.HomepageUrl,
                        DefaultBranchName = x.DefaultBranchRef.Name,

                        WatcherCount = x.Watchers(null, null, null, null).TotalCount,
                        HeadRefsCount = x.Refs("refs/heads/", null, null, null, null, null, null, "heads/").TotalCount,
                        TagCount = x.Refs("refs/heads/", null, null, null, null, null, null, "tags/").TotalCount,
                        ReleaseCount = x.Releases(null, null, null, null, null).TotalCount,

                        LatestReleaseOverview = x.Releases(null, null, 1, null, null).Nodes.Select(y => new
                        {
                            AuthorLogin = y.Author.Login,
                            AuthorAvatarUrl = y.Author.AvatarUrl(100),
                            y.DescriptionHTML,
                            y.IsDraft,
                            y.IsLatest,
                            y.IsPrerelease,
                            y.Name,
                            y.PublishedAt,
                        })
                        .ToList(),

                        x.ForkingAllowed,
                        x.HasIssuesEnabled,
                        x.HasProjectsEnabled,
                        x.IsArchived,
                        x.IsPrivate,
                        x.IsTemplate,

                        x.ViewerSubscription,

                        x.UpdatedAt,
                    })
                    .Compile();
            #endregion

            var res = await App.Connection.Run(query);

            #region copying
            List<Models.RepositoryDetails> items = new();

            Models.RepositoryDetails item = new()
            {
                DefaultBranchName = res.DefaultBranchName,
                HomepageUrl = res.HomepageUrl,

                WatcherCount = res.WatcherCount,
                HeadRefsCount = res.HeadRefsCount,
                ReleaseCount = res.ReleaseCount,

                HasIssuesEnabled = res.HasIssuesEnabled,
                HasProjectsEnabled = res.HasProjectsEnabled,
                IsArchived = res.IsArchived,
                IsPrivate = res.IsPrivate,
                IsTemplate = res.IsTemplate,

                LatestReleaseOverview = new()
                {
                    AuthorAvatarUrl = res.LatestReleaseOverview[0].AuthorAvatarUrl,
                    AuthorLogin = res.LatestReleaseOverview[0].AuthorLogin,
                    DescriptionHTML = res.LatestReleaseOverview[0].DescriptionHTML,
                    IsDraft = res.LatestReleaseOverview[0].IsDraft,
                    IsLatest = res.LatestReleaseOverview[0].IsLatest,
                    IsPrerelease = res.LatestReleaseOverview[0].IsPrerelease,
                    Name = res.LatestReleaseOverview[0].Name,
                    PublishedAt = res.LatestReleaseOverview[0]?.PublishedAt,
                    PublishedAtHumanized = res.LatestReleaseOverview[0]?.PublishedAt.Humanize(),
                },

                ViewerSubscription = res.ViewerSubscription,

                UpdatedAt = res.UpdatedAt,
            };
            #endregion

            return item;
        }

        public async Task<List<string>> GetBranchNameAllAsync(string owner, string name)
        {
            #region query
            var query = new Query()
                .Repository(name, owner)
                .Refs(refPrefix: "refs/", first: 30, query: "heads/")
                .Select(x => new
                {
                    BranchNames = x.Nodes.Select(y => new
                    {
                        y.Name,
                    })
                    .ToList()
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            List<string> branchNames = new();
            foreach (var branch in response.BranchNames)
            {
                // Delete "heads/"
                branchNames.Add(branch.Name.Remove(0, 6));
            }

            return branchNames;
        }

        public async Task<string> GetReadmeHtml(string owner, string name, string branch, string theme)
        {
            string bodyHtml = await App.Client.Repository.Content.GetReadmeHtml(owner, name);

            string missedPath = "https://raw.githubusercontent.com/" + owner + "/" + name + "/" + branch + "/";

            MarkdownQueries markdown = new();
            var html = await markdown.GetHtmlAsync(bodyHtml, missedPath, theme, true);

            return html;
        }
    }
}
