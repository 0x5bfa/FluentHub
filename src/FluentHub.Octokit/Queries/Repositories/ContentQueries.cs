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
    public class ContentQueries
    {
        /// <summary>
        /// Not recursive method.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="owner"></param>
        /// <param name="refs"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public async Task<List<Content>> GetAllAsync(string name, string owner, string refs, string path)
        {
            if (path[0] == '/')
                path = path.Remove(0, 1);

            var query = new Query()
                .Repository(name, owner)
                .Object(expression: refs + ":" + path)
                .Cast<GraphQLModel.Tree>()
                .Entries
                .Select(x => new Content
                {
                    Name = x.Name,
                    Path = x.Path,
                    Type = x.Type,
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response.ToList();
        }
    }
}
