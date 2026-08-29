namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A draft issue within a project.
    /// </summary>
    public class DraftIssue : QueryableValue<DraftIssue>
    {
        internal DraftIssue(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// A list of users to assigned to this draft issue.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public UserConnection Assignees(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Assignees(first, after, last, before), Octokit.GraphQL.Model.UserConnection.Create);

        /// <summary>
        /// The body of the draft issue.
        /// </summary>
        public string Body { get; }

        /// <summary>
        /// The body of the draft issue rendered to HTML.
        /// </summary>
        public string BodyHTML { get; }

        /// <summary>
        /// The body of the draft issue rendered to text.
        /// </summary>
        public string BodyText { get; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// The actor who created this draft issue.
        /// </summary>
        public IActor Creator => this.CreateProperty(x => x.Creator, Octokit.GraphQL.Model.Internal.StubIActor.Create);

        /// <summary>
        /// The Node ID of the DraftIssue object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// List of items linked with the draft issue (currently draft issue can be linked to only one item).
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProjectV2ItemConnection ProjectV2Items(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.ProjectV2Items(first, after, last, before), Octokit.GraphQL.Model.ProjectV2ItemConnection.Create);

        /// <summary>
        /// Projects that link to this draft issue (currently draft issue can be linked to only one project).
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProjectV2Connection ProjectsV2(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.ProjectsV2(first, after, last, before), Octokit.GraphQL.Model.ProjectV2Connection.Create);

        /// <summary>
        /// The title of the draft issue
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        internal static DraftIssue Create(Expression expression)
        {
            return new DraftIssue(expression);
        }
    }
}