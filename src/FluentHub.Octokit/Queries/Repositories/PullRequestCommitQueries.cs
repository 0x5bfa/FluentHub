namespace FluentHub.Octokit.Queries.Repositories
{
    public class PullRequestCommitQueries
    {
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
                        .SingleOrDefault(),
                    },
                })
                .Single())
                .Compile();

            var result = await App.Connection.Run(query);

            return result.ToList();
        }
    }
}
