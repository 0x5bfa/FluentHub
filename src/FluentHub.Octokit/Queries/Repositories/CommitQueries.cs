using Humanizer;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Octokit.Queries.Repositories
{
    public class CommitQueries
    {
        public CommitQueries() => new App();

        public async Task<List<Models.Commit>> GetAllAsync(string name, string owner, string refs, string path)
        {
            path = path.Remove(0, 1);

            if (string.IsNullOrEmpty(path)) path = ".";

            #region query
            var query = new Query()
                    .Repository(name, owner)
                    .Ref(refs)
                    .Target
                    .Cast<Commit>()
                    .History(first: 30, path: path)
                    .Select(x => new
                    {
                        A = x.Nodes.Select(y => new
                        {
                            y.AbbreviatedOid,
                            AuthorAvatarUrl = y.Author.AvatarUrl(100),
                            y.Author.User.Login,
                            y.CommittedDate,
                            y.Message,
                            y.MessageHeadline,
                        })
                        .ToList(),
                    })
                    .Compile();
            #endregion

            var result = await App.Connection.Run(query);

            #region copying
            List<Models.Commit> items = new();

            foreach (var res in result.A)
            {
                Models.Commit item = new()
                {
                    AbbreviatedOid = res.AbbreviatedOid,
                    AuthorAvatarUrl = res.AuthorAvatarUrl,
                    AuthorName = res.Login,
                    CommitMessage = res.Message,
                    CommitMessageHeadline = res.MessageHeadline,
                    CommittedAt = res.CommittedDate,
                    CommittedAtHumanized = res.CommittedDate.Humanize(),
                };

                items.Add(item);
            }
            #endregion

            return items;
        }

        public async Task<Models.Commit> GetAsync(string name, string owner, string refs, string path)
        {
            path = path.Remove(0, 1);

            if (string.IsNullOrEmpty(path)) path = ".";

            #region query
            var query = new Query()
                    .Repository(name, owner)
                    .Ref(refs)
                    .Target
                    .Cast<Commit>()
                    .History(first: 1, path: path)
                    .Select(x => new
                    {
                        CommitSummary = x.Nodes.Select(y => new
                        {
                            y.AbbreviatedOid,
                            AuthorAvatarUrl = y.Author.AvatarUrl(100),
                            y.Author.User.Login,
                            y.CommittedDate,
                            y.Message,
                        })
                        .ToList(),
                        x.TotalCount,
                    })
                    .Compile();
            #endregion

            var res = await App.Connection.Run(query);

            #region copying
            Models.Commit item = new()
            {
                AbbreviatedOid = res.CommitSummary[0].AbbreviatedOid,
                AuthorAvatarUrl = res.CommitSummary[0].AuthorAvatarUrl,
                AuthorName = res.CommitSummary[0].Login,
                CommitMessage = res.CommitSummary[0].Message,
                TotalCount = res.TotalCount,
                CommittedAt = res.CommitSummary[0].CommittedDate,
                CommittedAtHumanized = res.CommitSummary[0].CommittedDate.Humanize(),
            };
            #endregion

            return item;
        }

        public async Task<List<Models.Commit>> GetWithObjectNameAsync(string name, string owner, string refs, string path)
        {
            path = path.Remove(0, 1);

            #region objectQuery
            var queryToGetFileInfo = new Query()
                .Repository(name, owner)
                .Object(expression: refs + ":" + path)
                .Cast<Tree>()
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

            #region copying
            List<Models.Commit> items = new();

            foreach (var res1 in response1)
            {
                #region commitQuery
                var commitQuery = new Query()
                    .Repository(name, owner)
                    .Ref(refs)
                    .Target
                    .Cast<Commit>()
                    .History(first: 1, path: res1.Path)
                    .Select(x => new
                    {
                        CommitSummary = x.Nodes.Select(y => new
                        {
                            y.AbbreviatedOid,
                            AuthorAvatarUrl = y.Author.AvatarUrl(100),
                            y.Author.Name,
                            y.CommittedDate,
                            y.Message,
                        })
                        .ToList(),
                        x.TotalCount,
                    })
                    .Compile();
                #endregion

                var response2 = await App.Connection.Run(commitQuery);

                var res2 = response2.CommitSummary.ToList()[0];

                Models.Commit item = new()
                {
                    FileName = res1.Name,
                    FilePath = res1.Path,
                    ObjectType = res1.Type,

                    AbbreviatedOid = res2.AbbreviatedOid,
                    AuthorAvatarUrl = res2.AuthorAvatarUrl,
                    AuthorName = res2.Name,
                    CommitMessage = res2.Message,
                    TotalCount = response2.TotalCount,

                    CommittedAt = res2.CommittedDate,
                    CommittedAtHumanized = res2.CommittedDate.Humanize(),
                };


                items.Add(item);
            }
            #endregion

            return items;
        }
    }
}
