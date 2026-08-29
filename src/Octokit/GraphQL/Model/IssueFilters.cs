namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Ways in which to filter lists of issues.
    /// </summary>
    public class IssueFilters
    {
        /// <summary>
        /// List issues assigned to given name. Pass in `null` for issues with no assigned user, and `*` for issues assigned to any user.
        /// </summary>
        public string Assignee { get; set; }

        /// <summary>
        /// List issues created by given name.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// List issues where the list of label names exist on the issue.
        /// </summary>
        public IEnumerable<string> Labels { get; set; }

        /// <summary>
        /// List issues where the given name is mentioned in the issue.
        /// </summary>
        public string Mentioned { get; set; }

        /// <summary>
        /// List issues by given milestone argument. If an string representation of an integer is passed, it should refer to a milestone by its database ID. Pass in `null` for issues with no milestone, and `*` for issues that are assigned to any milestone.
        /// </summary>
        public string Milestone { get; set; }

        /// <summary>
        /// List issues by given milestone argument. If an string representation of an integer is passed, it should refer to a milestone by its number field. Pass in `null` for issues with no milestone, and `*` for issues that are assigned to any milestone.
        /// </summary>
        public string MilestoneNumber { get; set; }

        /// <summary>
        /// List issues that have been updated at or after the given date.
        /// </summary>
        public DateTimeOffset? Since { get; set; }

        /// <summary>
        /// List issues filtered by the list of states given.
        /// </summary>
        public IEnumerable<IssueState> States { get; set; }

        /// <summary>
        /// List issues subscribed to by viewer.
        /// </summary>
        public bool? ViewerSubscribed { get; set; }
    }
}