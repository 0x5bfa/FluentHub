using FluentHub.OctokitEx.Models;
using Octokit.GraphQL;
using Octokit.GraphQL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.OctokitEx.Queries.Repository
{
    public class LatestCommitQueries
    {
        public LatestCommitQueries() => new App();

        public async Task<CommitOverviewItem> Get(string name, string owner, string qualifiedRefName, string path = ".")
        {
            var query = new Query()
                .Repository(name, owner)
                .Ref(qualifiedRefName)
                .Target
                .Cast<Commit>()
                .History(path: path, first: 1)
                .Select(x => new
                {
                    CommitSummary = x.Nodes.Select(y => new
                    {
                        y.AbbreviatedOid,
                        AuthorAvatarUrl = y.Author.AvatarUrl(100),
                        y.Author.Name,
                        y.CommittedDate,
                        y.Message,
                    }).ToList(),
                    x.TotalCount,
                })
                .Compile();

            var result = await App.Connection.Run(query);

            CommitOverviewItem item = new();
            item.AbbreviatedOid = result.CommitSummary[0].AbbreviatedOid;
            item.AuthorAvatarUrl = result.CommitSummary[0].AuthorAvatarUrl;
            item.AuthorName = result.CommitSummary[0].Name;
            item.CommitMessage = result.CommitSummary[0].Message;
            item.CommittedDate = result.CommitSummary[0].CommittedDate;

            return item;
        }

        public async Task<List<CommitOverviewItem>> EnumFilesAndItsLatestCommit(string name, string owner, string branchName, string path = "")
        {
            path = path.Remove(0, 1);

            // query to get file info
            var queryToGetFileInfo = new Query()
                .Repository(name, owner)
                .Object(expression: branchName + ":" + path)
                .Cast<Tree>()
                .Entries
                .Select(x => new
                {
                    x.Name,
                    x.Path,
                    x.Type,
                })
                .Compile();

            var response1 = await App.Connection.Run(queryToGetFileInfo);

            List<CommitOverviewItem> items = new();

            foreach (var res1 in response1)
            {
                CommitOverviewItem item = new();

                item.FileName = res1.Name;
                item.FilePath = res1.Path;
                item.ObjectType = res1.Type;

                // query to get its file's latest commit
                var queryToGetLatestCommit = new Query()
                    .Repository(name, owner)
                    .Ref(branchName)
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
                        }).ToList(),
                        x.TotalCount,
                    })
                    .Compile();

                var response2 = await App.Connection.Run(queryToGetLatestCommit);

                var res2 = response2.CommitSummary.ToList()[0];

                item.TotalCount = response2.TotalCount;
                item.AbbreviatedOid = res2.AbbreviatedOid;
                item.AuthorAvatarUrl = res2.AuthorAvatarUrl;
                item.AuthorName = res2.Name;
                item.CommitMessage = res2.Message;
                item.CommittedDate = res2.CommittedDate;

                items.Add(item);
            }

            return items;
        }
    }
}
