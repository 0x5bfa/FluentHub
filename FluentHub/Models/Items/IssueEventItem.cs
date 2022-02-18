using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FluentHub.Models.Items
{
    public class IssueEventItem
    {
        public long Id;

        public string NodeId;

        public string Url;

        public User Actor;

        public User Assignee;

        public Label Label;

        public StringEnum<EventInfoState> Event;

        public string CommitId;

        public string CommitUrl;

        public DateTimeOffset CreatedAt;

        public Issue Issue;

        public RenameInfo Rename;

        public IssueEventProjectCard ProjectCard;
    }
}
