﻿using Humanizer;
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
    public class IssueQueries
    {
        public IssueQueries() => new App();

        public async Task<List<Models.Issue>> GetAllAsync(string name, string owner)
        {
            IssueOrder order = new() { Direction = OrderDirection.Desc, Field = IssueOrderField.CreatedAt };

            #region queries
            var query = new Query()
                .Repository(name, owner)
                .Issues(first: 30, orderBy: order)
                .Nodes
                .Select(x => new
                {
                    OwnerAvatarUrl = x.Repository.Owner.AvatarUrl(100),
                    OwnerLogin = x.Repository.Owner.Login,
                    x.Repository.Name,
                    x.Title,

                    x.Closed,

                    x.Number,
                    CommentCount = x.Comments(null, null, null, null, null).TotalCount,

                    Labels = x.Labels(10, null, null, null, null).Nodes.Select(y => new
                    {
                        y.Color,
                        y.Name,
                    })
                    .ToList(),

                    x.UpdatedAt,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            #region copying
            List<Models.Issue> items = new();

            foreach (var res in response)
            {
                Models.Issue item = new()
                {
                    Name = res.Name,
                    OwnerAvatarUrl = res.OwnerAvatarUrl,
                    OwnerLogin = res.OwnerLogin,
                    Title = res.Title,

                    Number = res.Number,
                    CommentCount = res.CommentCount,

                    IsClosed = res.Closed,

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

                items.Add(item);
            }
            #endregion

            return items;
        }

        public async Task<Models.Issue> GetAsync(string owner, string name, int number)
        {
            #region query
            var query = new Query()
                .Repository(name, owner)
                .Issue(number)
                .Select(x => new
                {
                    OwnerAvatarUrl = x.Repository.Owner.AvatarUrl(100),
                    OwnerLogin = x.Repository.Owner.Login,
                    x.Repository.Name,
                    x.Title,

                    x.Closed,

                    x.Number,
                    CommentCount = x.Comments(null, null, null, null, null).TotalCount,

                    Labels = x.Labels(10, null, null, null, null).Nodes.Select(y => new
                    {
                        y.Color,
                        y.Name,
                    })
                    .ToList(),

                    x.UpdatedAt,
                })
                .Compile();
            #endregion

            var res = await App.Connection.Run(query);

            #region copying
            Models.Issue item = new()
            {
                Name = res.Name,
                OwnerAvatarUrl = res.OwnerAvatarUrl,
                OwnerLogin = res.OwnerLogin,
                Title = res.Title,

                Number = res.Number,
                CommentCount = res.CommentCount,

                IsClosed = res.Closed,

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
            #endregion

            return item;
        }

        public async Task<Models.Issue> GetDetailsAsync(string owner, string name, int number)
        {
            #region query
            var query = new Query()
                .Repository(name, owner)
                .Issue(number)
                .Select(x => new
                {
                    Assignees = x.Assignees(6, null, null, null).Nodes.Select(assignees => new {
                        assignees.Login,
                        AvatarUrl = assignees.AvatarUrl(100),
                    }),
                    Labels = x.Labels(10, null, null, null, null).Nodes.Select(labels => new
                    {
                        labels.Name,
                        labels.Description,
                        labels.Color,
                    }),
                    Projects = x.ProjectCards(6, null, null, null, null).Nodes.Select(projects => new
                    {
                        projects.Note,
                    }),
                    x.Milestone,
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            #region copying

            #endregion

            return null;
        }
    }
}
