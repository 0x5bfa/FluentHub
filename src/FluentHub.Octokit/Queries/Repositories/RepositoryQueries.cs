using FluentHub.Octokit.Models;
using Humanizer;
using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphQLCore = global::Octokit.GraphQL.Core;
using GraphQLModel = global::Octokit.GraphQL.Model;

namespace FluentHub.Octokit.Queries.Repositories
{
    public class RepositoryQueries
    {
        public RepositoryQueries() => new App();

        public async Task<Repository> GetDetailsAsync(string owner, string name)
        {
            GraphQLCore.Arg<IEnumerable<GraphQLModel.IssueState>> issueState = new(new GraphQLModel.IssueState[] { GraphQLModel.IssueState.Open });
            GraphQLCore.Arg<IEnumerable<GraphQLModel.PullRequestState>> pullRequestState = new(new GraphQLModel.PullRequestState[] { GraphQLModel.PullRequestState.Open });

            var query = new Query()
                    .Repository(owner: owner, name: name)
                    .Select(x => new Repository
                    {
                        HomepageUrl = x.HomepageUrl,
                        ForkingAllowed = x.ForkingAllowed,
                        HasIssuesEnabled = x.HasIssuesEnabled,
                        HasProjectsEnabled = x.HasProjectsEnabled,
                        IsArchived = x.IsArchived,
                        IsPrivate = x.IsPrivate,
                        IsTemplate = x.IsTemplate,
                        ViewerSubscription = x.ViewerSubscription,
                        Name = x.Name,
                        Description = x.Description,
                        StargazerCount = x.StargazerCount,
                        ForkCount = x.ForkCount,
                        IsFork = x.IsFork,
                        IsInOrganization = x.IsInOrganization,
                        ViewerHasStarred = x.ViewerHasStarred,
                        UpdatedAt = x.UpdatedAt,
                        UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),
                        LicenseName = x.LicenseInfo.Select(y => y.Name).SingleOrDefault(),
                        DefaultBranchName = x.DefaultBranchRef.Select(defaultbranchref => defaultbranchref.Name).SingleOrDefault(),
                        WatcherCount = x.Watchers(null, null, null, null).TotalCount,
                        HeadRefsCount = x.Refs("refs/heads/", null, null, null, null, null, null, null).TotalCount,
                        TagCount = x.Refs("refs/tags/", null, null, null, null, null, null, null).TotalCount,
                        ReleaseCount = x.Releases(null, null, null, null, null).TotalCount,
                        OpenIssueCount = x.Issues(null, null, null, null, null, null, null, issueState).TotalCount,
                        OpenPullCount = x.PullRequests(null, null, null, null, null, null, null, null, pullRequestState).TotalCount,

                        Owner = x.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(100),
                            Id = owner.Id.ToString(),
                            Login = owner.Login,
                        })
                        .Single(),

                        LatestRelease = x.Releases(null, null, 1, null, null).Nodes.Select(release => new Release
                        {
                            AuthorLogin = release.Author.Login,
                            AuthorAvatarUrl = release.Author.AvatarUrl(100),
                            DescriptionHTML = release.DescriptionHTML,
                            IsDraft = release.IsDraft,
                            IsLatest = release.IsLatest,
                            IsPrerelease = release.IsPrerelease,
                            Name = release.Name,
                            PublishedAt = release.PublishedAt,
                            PublishedAtHumanized = release.PublishedAt.Humanize(null, null),
                        })
                        .ToList().FirstOrDefault(),
                    })
                    .Compile();

            var response = await App.Connection.Run(query);

            return response;
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
            string bodyHtml;

            try
            {
                bodyHtml = await App.Client.Repository.Content.GetReadmeHtml(owner, name);

                string missedPath = "https://raw.githubusercontent.com/" + owner + "/" + name + "/" + branch + "/";

                MarkdownQueries markdown = new();
                var html = await markdown.GetHtmlAsync(bodyHtml, missedPath, theme, true);

                return html;
            }
            catch (global::Octokit.NotFoundException)
            {
                return null;
            }
        }
    }
}
