using Humanizer;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Users
{
    public class PullRequestQueries
    {
        public PullRequestQueries() => new App();

        public async Task<List<Models.PullRequest>> GetAllAsync(string login)
        {
            IssueOrder order = new() { Direction = OrderDirection.Desc, Field = IssueOrderField.CreatedAt };

            #region queries
            var query = new Query()
                .User(login)
                .PullRequests(first: 30, orderBy: order)
                .Nodes
                .Select(x => new
                {
                    OwnerAvatarUrl = x.Repository.Owner.AvatarUrl(100),
                    OwnerLogin = x.Repository.Owner.Login,
                    x.Repository.Name,
                    x.Title,

                    x.Closed,
                    x.Merged,
                    x.IsDraft,

                    x.Number,
                    CommentCount = x.Comments(null, null, null, null, null).TotalCount,

                    Labels = x.Labels(10, null, null, null, null).Nodes.Select(y => new
                    {
                        y.Color,
                        y.Name,
                    })
                    .ToList(),

                    ReviewState = x.Reviews(null, null, 1, null, null, null).Nodes.Select(y => new
                    {
                        y.State,
                    })
                    .ToList(),

                    StatusState = x.Commits(null, null, 1, null).Nodes.Select(y => new
                    {
                        State = y.Commit.StatusCheckRollup.Select(z => new
                        {
                            z.State,
                        })
                        .SingleOrDefault(),
                    })
                    .ToList(),

                    x.UpdatedAt,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            #region copying
            List<Models.PullRequest> items = new();

            foreach (var res in response)
            {
                Models.PullRequest item = new()
                {
                    Name = res.Name,
                    OwnerAvatarUrl = res.OwnerAvatarUrl,
                    OwnerLogin = res.OwnerLogin,
                    Title = res.Title,

                    Number = res.Number,
                    CommentCount = res.CommentCount,

                    IsClosed = res.Closed,
                    IsMerged = res.Merged,
                    IsDraft = res.IsDraft,

                    UpdatedAt = res.UpdatedAt,
                    UpdatedAtHumanized = res.UpdatedAt.Humanize(),
                };

                if (res.Labels.Count() != 0)
                {
                    foreach (var label in res.Labels)
                    {
                        Models.Label labels = new()
                        {
                            Color = label.Color,
                            Name = label.Name,
                            ColorBrush = Helpers.ColorHelper.HexCodeToSolidColorBrush(label.Color),
                        };

                        item.Labels.Add(labels);
                    }
                }

                if (res.ReviewState.Count() != 0)
                    item.ReviewState = res.ReviewState[0].State.ToString();

                if (res.StatusState.Count() != 0 && res.StatusState[0].State != null)
                    item.StatusState = res.StatusState[0].State.State.ToString();

                items.Add(item);
            }
            #endregion

            return items;
        }
    }
}
