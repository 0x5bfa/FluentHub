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
    public class PullRequestQueries
    {
        public PullRequestQueries() => new App();

        public async Task<List<Models.PullRequest>> GetOverviewAll(string name, string owner)
        {
            try
            {
                IssueOrder order = new() { Direction = OrderDirection.Desc, Field = IssueOrderField.CreatedAt };

                #region queries
                var query = new Query()
                    .Repository(name, owner)
                    .PullRequests(first: 30, orderBy: order)
                    .Nodes
                    .Select(x => new
                    {
                        x.Closed,
                        x.Merged,
                        x.IsDraft,
                        Labels = x.Labels(10, null, null, null, null).Nodes.Select(y => new
                        {
                            y.Color,
                            y.Name,
                        }).ToList(),
                        x.Number,
                        x.Title,
                        x.UpdatedAt,
                    })
                    .Compile();
                #endregion

                var response = await App.Connection.Run(query);

                #region copying
                List<Models.PullRequest> items = new();

                foreach (var res in response)
                {
                    Models.PullRequest item = new();
                    item.Labels = new();

                    item.IsClosed = res.Closed;
                    item.IsMerged = res.Merged;
                    item.IsDraft = res.IsDraft;

                    foreach (var label in res.Labels)
                    {
                        Models.Label labels = new();
                        labels.Color = label.Color;
                        labels.Name = label.Name;

                        item.Labels.Add(labels);
                    }

                    item.Number = res.Number;
                    item.Title = res.Title;
                    item.UpdatedAt = res.UpdatedAt;

                    item.Name = name;
                    item.Owner = owner;

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

        public async Task<Models.PullRequest> GetOverview(string owner, string name, int number)
        {
            try
            {
                #region queries
                var query = new Query()
                    .Repository(name, owner)
                    .PullRequest(number)
                    .Select(x => new
                    {
                        x.Closed,
                        x.Merged,
                        x.IsDraft,
                        Labels = x.Labels(10, null, null, null, null).Nodes.Select(y => new
                        {
                            y.Color,
                            y.Name,
                        }).ToList(),
                        x.Number,
                        x.Title,
                        x.UpdatedAt,
                    })
                    .Compile();
                #endregion

                var response = await App.Connection.Run(query);

                #region copying
                Models.PullRequest item = new();
                item.IsClosed = response.Closed;
                item.IsMerged = response.Merged;
                item.IsDraft = response.IsDraft;
                item.Number = response.Number;
                item.Title = response.Title;
                item.UpdatedAt = response.UpdatedAt;
                item.Name = name;
                item.Owner = owner;
                #endregion

                return item;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return null;
            }
        }
    }
}
