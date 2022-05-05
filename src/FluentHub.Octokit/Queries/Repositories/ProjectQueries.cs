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
    public class ProjectQueries
    {
        public ProjectQueries() => new App();

        public async Task<List<Project>> GetAllAsync(string owner, string name)
        {
            var query = new Query()
                .Repository(name, owner)
                .Projects(first: 30)
                .Nodes
                .Select(x => new Project
                {
                    Body = x.Body,
                    Closed = x.Closed,
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Number = x.Number,
                    State = x.State,
                    Url = x.Url,
                    ViewerCanUpdate = x.ViewerCanUpdate,

                    ClosedAt = x.ClosedAt,
                    ClosedAtHumanized = x.ClosedAt.Humanize(null, null),
                    CreatedAt = x.CreatedAt,
                    CreatedAtHumanized = x.CreatedAt.Humanize(null, null),
                    UpdatedAt = x.UpdatedAt,
                    UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),

                    Progress = x.Progress.Select(y => new ProjectProgress
                    {
                        DoneCount = y.DoneCount,
                        DonePercentage = y.DonePercentage,
                        Enabled = y.Enabled,
                        InProgressCount = y.InProgressCount,
                        InProgressPercentage = y.InProgressPercentage,
                        TodoCount = y.TodoCount,
                        TodoPercentage = y.TodoPercentage,
                    })
                    .Single(),

                    Repository = x.Owner.Cast<GraphQLModel.Repository>().Select(repo => new Repository
                    {
                        Name = repo.Name,

                        Owner = repo.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(100),
                            Id = owner.Id.ToString(),
                            Login = owner.Login,
                        })
                        .Single(),
                    })
                    .Single(),
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response.ToList();
        }

        public async Task<Project> GetAsync(string owner, string name, int number)
        {
            var query = new Query()
                .Repository(name, owner)
                .Project(number)
                .Select(x => new Project
                {
                    Body = x.Body,
                    Closed = x.Closed,
                    Id = x.Id.ToString(),
                    Name = x.Name,
                    Number = x.Number,
                    State = x.State,
                    Url = x.Url,
                    ViewerCanUpdate = x.ViewerCanUpdate,

                    ClosedAt = x.ClosedAt,
                    ClosedAtHumanized = x.ClosedAt.Humanize(null, null),
                    CreatedAt = x.CreatedAt,
                    CreatedAtHumanized = x.CreatedAt.Humanize(null, null),
                    UpdatedAt = x.UpdatedAt,
                    UpdatedAtHumanized = x.UpdatedAt.Humanize(null, null),

                    Progress = x.Progress.Select(y => new ProjectProgress
                    {
                        DoneCount = y.DoneCount,
                        DonePercentage = y.DonePercentage,
                        Enabled = y.Enabled,
                        InProgressCount = y.InProgressCount,
                        InProgressPercentage = y.InProgressPercentage,
                        TodoCount = y.TodoCount,
                        TodoPercentage = y.TodoPercentage,
                    })
                    .Single(),

                    Repository = x.Owner.Cast<GraphQLModel.Repository>().Select(repo => new Repository
                    {
                        Name = repo.Name,

                        Owner = repo.Owner.Select(owner => new RepositoryOwner
                        {
                            AvatarUrl = owner.AvatarUrl(100),
                            Id = owner.Id.ToString(),
                            Login = owner.Login,
                        })
                        .Single(),
                    })
                    .Single(),
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response;
        }
    }
}
