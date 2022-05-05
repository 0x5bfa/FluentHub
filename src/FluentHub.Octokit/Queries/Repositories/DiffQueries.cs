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
    public class DiffQueries
    {
        public DiffQueries() => new App();

        public async Task<CommitDetails> GetAllAsync(string owner, string name, string refs)
        {
            // This API v3 getter is very heavy(because of N+1 Problem),
            // but there is no API for changed files in GitHub API v4
            var commit = await App.Client.Repository.Commit.Get(owner, name, refs);

            CommitDetails item = new()
            {
                CommitMessage = commit.Commit.Message,
                CommitMessageHeadline = commit.Commit.Message.Split("\n")[0],
                Oid = commit.Sha,
                AbbreviatedOid = commit.Sha.Substring(0, 7),
                ParentOid = commit.Parents[0].Sha,
                AbbreviatedParentOid = commit.Parents[0].Sha.Substring(0, 7),
                TotalAdditions = commit.Stats.Additions,
                TotalDeletions = commit.Stats.Deletions,
                TotalChangedFileCount = commit.Stats.Total,
            };

            foreach (var file in commit.Files)
            {
                ChangedFile changedFile = new()
                {
                    TotalLineCount = file.Changes,
                    LineAdditions = file.Additions,
                    LineDeletions = file.Deletions,
                    Patch = file.Patch,
                    ChangeType = file.Status,
                    FileName = file.Filename,
                    PreviousFileName = file.PreviousFileName,
                };

                item.ChangedFiles.Add(changedFile);
            }

            return item;
        }

        public async Task<List<ChangedFile>> GetAllAsync(string owner, string name, int number)
        {
            var files = await App.Client.Repository.PullRequest.Files(owner, name, number);

            List<ChangedFile> changedFiles = new();
            foreach (var file in files)
            {
                ChangedFile changedFile = new()
                {
                    TotalLineCount = file.Changes,
                    LineAdditions = file.Additions,
                    LineDeletions = file.Deletions,
                    Patch = file.Patch,
                    ChangeType = file.Status,
                    FileName = file.FileName,
                    PreviousFileName = file.PreviousFileName,
                };

                changedFiles.Add(changedFile);
            }

            return changedFiles;
        }
    }
}
