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

                    PrimaryLanguage = x.PrimaryLanguage.Select(y => new
                    {
                        y.Name,
                        y.Color,
                    }).SingleOrDefault(),

                    LicenseName = x.LicenseInfo.Select(y => y.Name).SingleOrDefault(),
                    DefaultBranchName = x.DefaultBranchRef.Name,
                    x.HomepageUrl,

                    x.StargazerCount,
                    x.ForkCount,
                    IssueCount = x.Issues(null, null, null, null, null, null, null, null).TotalCount,
                    OpenIssueCount = x.Issues(null, null, null, null, null, null, null, null).TotalCount,
                    PullCount = x.PullRequests(null, null, null, null, null, null, null, null, null).TotalCount,
                    OpenPullCount = x.PullRequests(null, null, null, null, null, null, null, null, null).TotalCount,
                    WatcherCount = x.Watchers(null, null, null, null).TotalCount,
                    HeadRefsCount = x.Refs("refs/heads/", null, null, null, null, null, null, null).TotalCount,
                    ReleaseCount = x.Releases(null, null, null, null, null).TotalCount,

                    x.ForkingAllowed,
                    x.HasIssuesEnabled,
                    x.HasProjectsEnabled,
                    x.IsArchived,
                    x.IsFork,
                    x.IsInOrganization,
                    x.IsPrivate,
                    x.IsTemplate,
                    x.ViewerHasStarred,

                    x.ViewerSubscription,

                    x.UpdatedAt,
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

            item.PrimaryLangName = response.PrimaryLanguage?.Name;
            item.PrimaryLangColor = response.PrimaryLanguage?.Color;
            item.LicenseName = response.LicenseName;
            item.DefaultBranchName = response.DefaultBranchName;
            item.HomepageUrl = response.HomepageUrl;

            item.StargazerCount = response.StargazerCount;
            item.ForkCount = response.ForkCount;
            item.IssueCount = response.IssueCount;
            item.OpenIssueCount = response.OpenIssueCount;
            item.PullCount = response.PullCount;
            item.OpenPullCount = response.OpenPullCount;
            item.WatcherCount = response.WatcherCount;
            item.HeadRefsCount = response.HeadRefsCount;
            item.ReleaseCount = response.ReleaseCount;

            item.ViewerHasStarred = response.ViewerHasStarred;
            item.HasIssuesEnabled = response.HasIssuesEnabled;
            item.HasProjectsEnabled = response.HasProjectsEnabled;
            item.IsArchived = response.IsArchived;
            item.IsFork = response.IsFork;
            item.IsInOrganization = response.IsInOrganization;
            item.IsPrivate = response.IsPrivate;
            item.IsTemplate = response.IsTemplate;

            item.ViewerSubscription = response.ViewerSubscription;

            item.UpdatedAt = response.UpdatedAt;
            #endregion

            return item;
        }

        // Details such as releases, contributors
        public void GetDetails()
        {
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
