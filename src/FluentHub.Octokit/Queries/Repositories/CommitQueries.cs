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
    public class CommitQueries
    {
        public CommitQueries() => new App();

        public async Task<List<Commit>> GetAllAsync(string name, string owner, string refs, string path)
        {
            path = path.Remove(0, 1);

            if (string.IsNullOrEmpty(path)) path = ".";

            #region query
            var query = new Query()
                    .Repository(name, owner)
                    .Ref(refs)
                    .Target
                    .Cast<GraphQLModel.Commit>()
                    .History(first: 30, path: path)
                    .Nodes
                    .Select(x => new Commit
                    {
                        Owner = x.Repository.Owner.Login,
                        Name = x.Repository.Name,
                        Refs = refs,
                        AbbreviatedOid = x.AbbreviatedOid,
                        Oid = x.Oid,
                        AuthorAvatarUrl = x.Author.AvatarUrl(100),
                        AuthorName = x.Author.User.Login,
                        CommittedAt = x.CommittedDate,
                        CommitMessage = x.Message,
                        CommitMessageHeadline = x.MessageHeadline,
                    })
                    .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }

        public async Task<List<Commit>> GetAllAsync(string owner, string name, int number)
        {
            #region query
            var query = new Query()
                    .Repository(name, owner)
                    .PullRequest(number)
                    .Commits(first: 30)
                    .Nodes
                    .Select(x => x.Commit.Select(y => new Commit
                    {
                        Owner = owner,
                        Name = name,
                        PullRequestNumber = number,
                        AbbreviatedOid = y.AbbreviatedOid,
                        Oid = y.Oid,
                        AuthorAvatarUrl = y.Author.AvatarUrl(100),
                        AuthorName = y.Author.User.Login,
                        CommittedAt = y.CommittedDate,
                        CommitMessage = y.Message,
                        CommitMessageHeadline = y.MessageHeadline,
                    })
                    .Single())
                    .Compile();
            #endregion

            var result = await App.Connection.Run(query);

            return result.ToList();
        }

        public async Task<Commit> GetAsync(string name, string owner, string refs, string path)
        {
            path = path.Remove(0, 1);

            if (string.IsNullOrEmpty(path)) path = ".";

            #region query
            var query = new Query()
                    .Repository(name, owner)
                    .Ref(refs)
                    .Target
                    .Cast<GraphQLModel.Commit>()
                    .History(first: 1, path: path)
                    .Select(x => new
                    {
                        Commit = x.Nodes.Select(y => new Commit
                        {
                            AbbreviatedOid = y.AbbreviatedOid,
                            Oid =y.Oid,
                            AuthorAvatarUrl = y.Author.Select(author => author.AvatarUrl(100)).SingleOrDefault(),
                            AuthorName = y.Author.Select(author => author.User.Select(user => user.Login).SingleOrDefault()).SingleOrDefault(),
                            CommitMessage =y.Message,

                            CommittedAt =y.CommittedDate,
                            CommittedAtHumanized = y.CommittedDate.Humanize(null, null),
                        })
                        .ToList().FirstOrDefault(),

                        x.TotalCount,
                    })
                    .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            response.Commit.TotalCount = response.TotalCount;

            return response.Commit;
        }

        public async Task<List<Commit>> GetWithObjectNameAsync(string name, string owner, string refs, string path)
        {
            path = path.Remove(0, 1);

            #region objectQuery
            var queryToGetFileInfo = new Query()
                .Repository(name, owner)
                .Object(expression: refs + ":" + path)
                .Cast<GraphQLModel.Tree>()
                .Entries
                .Select(x => new
                {
                    x.Name,
                    x.Path,
                    x.Type,
                })
                .Compile();
            #endregion

            var response1 = await App.Connection.Run(queryToGetFileInfo);

            List<Commit> items = new();

            foreach (var res1 in response1)
            {
                #region commitQuery
                var commitQuery = new Query()
                    .Repository(name, owner)
                    .Ref(refs)
                    .Target
                    .Cast<GraphQLModel.Commit>()
                    .History(first: 1, path: res1.Path)
                    .Select(x => new 
                    {
                        Commit = x.Nodes.Select(y => new Commit
                        {
                            FileName = res1.Name,
                            FilePath = res1.Path,
                            ObjectType = res1.Type,
                            AbbreviatedOid = y.AbbreviatedOid,
                            AuthorAvatarUrl = y.Author.AvatarUrl(100),
                            AuthorName = y.Author.Name,
                            CommitMessage = y.Message,

                            CommittedAt = y.CommittedDate,
                            CommittedAtHumanized = y.CommittedDate.Humanize(null, null),
                        })
                        .ToList()
                        .FirstOrDefault(),

                        x.TotalCount,
                    })
                    .Compile();
                #endregion

                var response2 = await App.Connection.Run(commitQuery);

                response2.Commit.TotalCount = response2.TotalCount;

                items.Add(response2.Commit);
            }

            return items;
        }
    }
}
