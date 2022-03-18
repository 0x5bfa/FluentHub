using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.OctokitEx.Queries
{
    public class PinnedItemsQueries
    {
        public PinnedItemsQueries() => new App();

        public async Task<List<Models.RepositoryBlockItem>> GetOverviewAll(string login, bool isUser)
        {
            var usersQuery = new Query()
                    .User(login)
                    .PinnedItems(first: 6)
                    .Nodes
                    .OfType<Octokit.GraphQL.Model.Repository>()
                    .Select(x => new
                    {
                        x.Name,
                        x.Description,
                        Owner = x.Owner.Select(y => y.Login).Single(),
                        PrimaryLanguage = 
                        x.Languages(1, null, null, null, null)
                        .Nodes
                        .Select(language => new
                        {
                            language.Name, language.Color
                        }).ToList(),
                        x.StargazerCount,
                    })
                    .Compile();

            var orgsQuery = new Query()
                    .Organization(login)
                    .PinnedItems(first: 6)
                    .Nodes
                    .OfType<Octokit.GraphQL.Model.Repository>()
                    .Select(x => new
                    {
                        x.Name,
                        x.Description,
                        Owner = x.Owner.Select(y => y.Login).Single(),
                        PrimaryLanguage =
                        x.Languages(1, null, null, null, null)
                        .Nodes
                        .Select(language => new
                        {
                            language.Name,
                            language.Color
                        }).ToList(),
                        x.StargazerCount,
                    })
                    .Compile();

            var result = await App.Connection.Run(isUser ? usersQuery : orgsQuery);

            List<Models.RepositoryBlockItem> items = new();

            foreach (var res in result)
            {
                Models.RepositoryBlockItem item = new();

                item.Description = res.Description;
                item.PrimaryLangName = res.PrimaryLanguage[0].Name;
                item.PrimaryLangColor = res.PrimaryLanguage[0].Color;
                item.Owner = res.Owner;
                item.Name = res.Name;
                item.StargazerCount = res.StargazerCount;

                items.Add(item);
            }

            return items;
        }
    }
}
