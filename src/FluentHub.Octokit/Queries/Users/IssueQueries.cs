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
    public class IssueQueries
    {
        public IssueQueries() => new App();

        public async Task<List<Models.Issue>> GetOverviewAllAsync(string login)
        {
            try
            {
                IssueOrder order = new() { Direction = OrderDirection.Desc, Field = IssueOrderField.CreatedAt };

                #region queries
                var query = new Query()
                    .User(login)
                    .Issues(first: 30, orderBy: order)
                    .Nodes
                    .Select(x => new
                    {
                        x.Closed,
                        Labels = x.Labels(10, null, null, null, null).Nodes.Select(y => new
                        {
                            y.Color,
                            y.Name,
                        }).ToList(),
                        x.Repository.Name,
                        x.Repository.Owner.Login,
                        x.Number,
                        x.Title,
                        x.UpdatedAt,
                    })
                    .Compile();
                #endregion

                var response = await App.Connection.Run(query);

                #region copying
                List<Models.Issue> items = new();

                foreach (var res in response)
                {
                    Models.Issue item = new();
                    item.Labels = new();

                    item.IsClosed = res.Closed;

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
                    item.Name = res.Name;
                    item.Owner = res.Login;

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
