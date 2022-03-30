using FluentHub.Octokit.Models;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Users
{
    public class DiscussionQueries
    {
        public DiscussionQueries() => new App();

        public async Task<List<Models.Discussion>> GetOverviewAllAsync(string login)
        {
            #region queries
            var query = new Query()
                .User(login)
                .RepositoryDiscussions(first: 30)
                .Nodes
                .Select(x => new
                {
                    x.Title,
                    x.Repository.Name,
                    x.Repository.Owner.Login,
                    x.Number,
                    x.UpvoteCount,
                    x.Comments(null, null, null, null).TotalCount,
                    Category = x.Category.Select(y => new
                    {
                        y.Name,
                        y.Emoji,
                    }).Single(),
                    x.UpdatedAt,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            #region copy
            List<Models.Discussion> items = new();

            foreach (var res in response)
            {
                Models.Discussion item = new();

                item.Title = res.Title;
                item.Name = res.Name;
                item.Owner = res.Login;
                item.Number = res.Number;
                item.UpvoteCount = res.UpvoteCount;
                item.TotalCommentCount = res.TotalCount;
                item.CategoryName = res.Category.Name;
                item.CategoryEmoji = res.Category.Emoji;
                item.UpdatedAt = res.UpdatedAt;

                items.Add(item);
            }
            #endregion

            return items;
        }
    }
}
