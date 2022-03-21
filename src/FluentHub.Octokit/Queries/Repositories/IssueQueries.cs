using FluentHub.Octokit.Models;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Repositories
{
    public class IssueQueries
    {
        public IssueQueries() => new App();

        public async Task<List<Models.Issue>> GetOverviewAll(string name, string owner)
        {
            IssueOrder order = new() { Direction = OrderDirection.Desc, Field = IssueOrderField.CreatedAt };

            #region queries
            var query = new Query()
                .Repository(name, owner)
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
                    x.Number,
                    x.Title,
                    x.UpdatedAt,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            List<Models.Issue> items = new();

            foreach (var res in response)
            {
                Models.Issue item = new();
                item.Labels = new();

                item.IsClosed = res.Closed;

                foreach(var label in res.Labels)
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

            return items;
        }

        public async Task<Models.Issue> GetOverview(string owner, string name, int number)
        {
            #region query
            var query = new Query()
                .Repository(name, owner)
                .Issue(number)
                .Select(x => new
                {
                    x.Closed,
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
            Models.Issue item = new();
            item.Labels = new();
            item.IsClosed = response.Closed;
            foreach (var label in response.Labels)
            {
                Models.Label labels = new();
                labels.Color = label.Color;
                labels.Name = label.Name;

                item.Labels.Add(labels);
            }
            item.Number = response.Number;
            item.Title = response.Title;
            item.UpdatedAt = response.UpdatedAt;
            item.Name = name;
            item.Owner = owner;
            #endregion

            return item;
        }
    }
}
