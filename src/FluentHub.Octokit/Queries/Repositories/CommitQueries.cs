namespace FluentHub.Octokit.Queries.Repositories
{
    public class CommitQueries
    {
        public async Task<List<Commit>> GetAllAsync(string name, string owner, string refs, string path)
        {
            if (string.IsNullOrEmpty(path)) path = ".";

            #region query
            var query = new Query()
                .Repository(name, owner)
                .Ref(refs)
                .Target
                .Cast<OctokitGraphQLModel.Commit>()
                .History(first: 30, path: path)
                .Nodes
                .Select(x => new Commit
                {
                    AbbreviatedOid = x.AbbreviatedOid,
                    Oid = x.Oid,
                    CommittedDate = x.CommittedDate,
                    Message = x.Message,
                    MessageHeadline = x.MessageHeadline,

                    Repository = x.Repository.Select(repo => new Repository
                    {
                        Name = repo.Name,
                        Owner = repo.Owner.Select(owner => new RepositoryOwner
                        {
                            Login = owner.Login,
                        })
                        .Single(),
                    })
                    .Single(),

                    //Refs = refs,

                    Author = x.Author.Select(author => new GitActor
                    {
                        AvatarUrl = author.AvatarUrl(100),
                        User = author.User.Select(user => new User
                        {
                            Login = user.Login,
                        })
                        .Single(),
                    })
                    .Single(),
                })
                .Compile();
            #endregion

            var response = await App.Connection.Run(query);

            return response.ToList();
        }

        public async Task<List<Commit>> GetAllAsync(string owner, string name, int number)
        {
            var query = new Query()
                .Repository(name, owner)
                .PullRequest(number)
                .Commits(first: 30)
                .Nodes
                .Select(x => x.Commit.Select(y => new Commit
                {
                    AbbreviatedOid = y.AbbreviatedOid,
                    CommittedDate = y.CommittedDate,
                    Message = y.Message,
                    MessageHeadline = y.MessageHeadline,
                    Oid = y.Oid,

                    Author = new()
                    {
                        AvatarUrl = y.Author.AvatarUrl(100),
                        User = y.Author.User.Select(user => new User
                        {
                            Login = user.Login,
                        })
                        .Single(),
                    },
                })
                .Single())
                .Compile();

            var result = await App.Connection.Run(query);

            return result.ToList();
        }

        public async Task<Commit> GetLatestAsync(string name, string owner, string refs, string path)
        {
            if (string.IsNullOrEmpty(path))
                path = ".";

            var query = new Query()
                .Repository(name, owner)
                .Ref(refs)
                .Target
                .Cast<OctokitGraphQLModel.Commit>()
                .Select(commit => new Commit
                {
                    History = commit.History(1, null, null, null, null, null, null, null).Select(history => new CommitHistoryConnection
                    {
                        Nodes = history.Nodes.Select(y => new Commit
                        {
                            AbbreviatedOid = y.AbbreviatedOid,
                            Oid = y.Oid,
                            CommittedDate = y.CommittedDate,
                            Message = y.Message,
                            MessageHeadline = y.MessageHeadline,

                            Author = new()
                            {
                                AvatarUrl = y.Author.AvatarUrl(100),
                                User = y.Author.User.Select(user => new User
                                {
                                    Login = user.Login,
                                })
                            .Single(),
                            },
                        })
                        .ToList(),

                        TotalCount = history.TotalCount,
                    })
                    .SingleOrDefault(),
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response;
        }

        public async Task<(List<TreeEntry> Files, List<Commit> Commits)> GetWithObjectNameAsync(string name, string owner, string refs, string path)
        {
            #region objectQuery
            var queryToGetFileInfo = new Query()
                .Repository(name, owner)
                .Object(expression: refs + ":" + path)
                .Cast<OctokitGraphQLModel.Tree>()
                .Entries
                .Select(x => new TreeEntry
                {
                    Name = x.Name,
                    Path = x.Path,
                    Type = x.Type,
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
                    .Cast<OctokitGraphQLModel.Commit>()
                    .History(first: 1, path: res1.Path)
                    .Select(x => new 
                    {
                        Commit = x.Nodes.Select(y => new Commit
                        {
                            //Name = res1.Name,
                            //Path = res1.Path,
                            //Type = res1.Type,
                            AbbreviatedOid = y.AbbreviatedOid,
                            Author = new()
                            {
                                AvatarUrl = y.Author.AvatarUrl(100),
                                User = y.Author.User.Select(user => new User
                                {
                                    Login = user.Login,
                                })
                            .Single(),
                            },
                            Message = y.Message,

                            CommittedDate = y.CommittedDate,
                        })
                        .ToList()
                        .FirstOrDefault(),

                        x.TotalCount,
                    })
                    .Compile();
                #endregion

                var response2 = await App.Connection.Run(commitQuery);

                //response2.Commit.TotalCount = response2.TotalCount;

                items.Add(response2.Commit);
            }

            (List<TreeEntry> Files, List<Commit> Commits) results = (response1.ToList(), items);

            return results;
        }
    }
}
