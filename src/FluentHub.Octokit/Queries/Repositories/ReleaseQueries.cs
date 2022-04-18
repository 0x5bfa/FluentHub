using Humanizer;
using global::Octokit.GraphQL.Core;
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
    public class ReleaseQueries
    {
        public ReleaseQueries() => new App();

        public async Task<List<Models.Release>> GetAllAsync(string owner, string name)
        {
            Arg<ReleaseOrder> releaseOrder = new(new ReleaseOrder { Direction = OrderDirection.Desc});

            var query = new Query()
                .Repository(name, owner)
                .Releases(null, null, 20, null, releaseOrder)
                .Select(x => new
                {
                    Releases = x.Nodes.Select(y => new
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
                })
                .Compile();

            var response = await App.Connection.Run(query);

            List<Models.Release> items = new();

            foreach(var res in response.Releases)
            {
                Models.Release item;
                item = new()
                {
                    AuthorAvatarUrl = res.AuthorAvatarUrl,
                    AuthorLogin = res.AuthorLogin,
                    DescriptionHTML = res.DescriptionHTML,
                    IsDraft = res.IsDraft,
                    IsLatest = res.IsLatest,
                    IsPrerelease = res.IsPrerelease,
                    Name = res.Name,
                    PublishedAt = res.PublishedAt,
                    PublishedAtHumanized = res.PublishedAt.Humanize(),
                };

                items.Add(item);
            }

            return items;
        }
    }
}
