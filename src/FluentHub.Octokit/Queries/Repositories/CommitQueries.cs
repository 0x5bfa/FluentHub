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
    public class CommitQueries
    {
        public CommitQueries() => new App();

        public async Task<Models.Commit> GetOverview(string name, string owner, string branchName, string path)
        {
            try
            {
                path = path.Remove(0, 1);

                if (string.IsNullOrEmpty(path)) path = ".";

                #region query
                var query = new Query()
                        .Repository(name, owner)
                        .Ref(branchName)
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
                            }).ToList(),
                            x.TotalCount,
                        })
                        .Compile();
                #endregion

                var result = await App.Connection.Run(query);

                #region copying
                Models.Commit item = new();
                item.AbbreviatedOid = result.CommitSummary[0].AbbreviatedOid;
                item.AuthorAvatarUrl = result.CommitSummary[0].AuthorAvatarUrl;
                item.AuthorName = result.CommitSummary[0].Login;
                item.CommitMessage = result.CommitSummary[0].Message;
                item.CommittedDate = result.CommitSummary[0].CommittedDate;
                item.TotalCount = result.TotalCount;
                #endregion

                return item;
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                return null;
            }
        }

        public async Task<List<Models.Commit>> GetOverviewAllFilesAndLatestCommit(string name, string owner, string branchName, string path)
        {
            try
            {
                path = path.Remove(0, 1);

                #region query
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
                #endregion

                var response1 = await App.Connection.Run(queryToGetFileInfo);

                #region copying
                List<Models.Commit> items = new();

                foreach (var res1 in response1)
                {
                    Models.Commit item = new();

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
