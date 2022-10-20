namespace FluentHub.Octokit.Queries.Repositories
{
    public class CommitQueries
    {
        public async Task<List<Commit>> GetAllAsync(string name, string owner, string refs, string path)
        {
            if (string.IsNullOrEmpty(path))
                path = ".";

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
                    CommittedDateHumanized = x.CommittedDate.Humanize(null, null),
                    Message = x.Message,
                    MessageHeadline = x.MessageHeadline,

                    Author = x.Author.Select(author => new GitActor
                    {
                        AvatarUrl = author.AvatarUrl(100),

                        User = author.User.Select(user => new User
                        {
                            Login = user.Login,
                        })
                        .SingleOrDefault(),
                    })
                    .SingleOrDefault(),

                    Repository = x.Repository.Select(repo => new Repository
                    {
                        Name = repo.Name,

                        Owner = repo.Owner.Select(owner => new RepositoryOwner
                        {
                            Login = owner.Login,
                        })
                        .SingleOrDefault(),
                    })
                    .SingleOrDefault(),

                    Signature = x.Signature.Select(signature => new GitSignature
                    {
                        IsValid = signature.IsValid,
                        Payload = signature.Payload,
                        Signature = signature.Payload,
                        State = (GitSignatureState)signature.State,
                        WasSignedByGitHub = signature.WasSignedByGitHub,

                        Signer = signature.Signer.Select(user => new User
                        {
                            AvatarUrl = user.AvatarUrl(100),
                            Login = user.Login
                        })
                        .SingleOrDefault(),
                    })
                    .SingleOrDefault(),
                })
                .Compile();

            var response = await App.Connection.Run(query);

            return response.ToList();
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
                    History = commit.History(1, null, null, null, null, path, null, null).Select(history => new CommitHistoryConnection
                    {
                        Nodes = history.Nodes.Select(y => new Commit
                        {
                            AbbreviatedOid = y.AbbreviatedOid,
                            Oid = y.Oid,
                            CommittedDate = y.CommittedDate,
                            CommittedDateHumanized = y.CommittedDate.Humanize(null, null),
                            Message = y.Message,
                            MessageHeadline = y.MessageHeadline,
                            MessageBody = y.MessageBody,

                            Author = y.Author.Select(author => new GitActor
                            {
                                AvatarUrl = author.AvatarUrl(100),
                                User = author.User.Select(user => new User
                                {
                                    Login = user.Login,
                                })
                                .SingleOrDefault(),
                            })
                            .SingleOrDefault(),
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
    }
}
