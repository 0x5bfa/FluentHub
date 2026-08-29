namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Model;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Represents an owner of a Project.
    /// </summary>
    [GraphQLIdentifier("ProjectOwner")]
    public interface IProjectOwner : IQueryableValue<IProjectOwner>, IQueryableInterface
    {
        /// <summary>
        /// The Node ID of the ProjectOwner object
        /// </summary>
        ID Id { get; }

        /// <summary>
        /// Find project by number.
        /// </summary>
        /// <param name="number">The project number to find.</param>
        Project Project(Arg<int> number);

        /// <summary>
        /// A list of projects under the owner.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for projects returned from the connection</param>
        /// <param name="search">Query to search projects by, currently only searching by name.</param>
        /// <param name="states">A list of states to filter the projects by.</param>
        ProjectConnection Projects(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectOrder>? orderBy = null, Arg<string>? search = null, Arg<IEnumerable<ProjectState>>? states = null);

        /// <summary>
        /// The HTTP path listing owners projects
        /// </summary>
        string ProjectsResourcePath { get; }

        /// <summary>
        /// The HTTP URL listing owners projects
        /// </summary>
        string ProjectsUrl { get; }

        /// <summary>
        /// Can the current viewer create new projects on this owner.
        /// </summary>
        bool ViewerCanCreateProjects { get; }
    }
}

namespace Octokit.GraphQL.Model.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    [GraphQLIdentifier("ProjectOwner")]
    internal class StubIProjectOwner : QueryableValue<StubIProjectOwner>, IProjectOwner
    {
        internal StubIProjectOwner(Expression expression) : base(expression)
        {
        }

        public ID Id { get; }

        public Project Project(Arg<int> number) => this.CreateMethodCall(x => x.Project(number), Octokit.GraphQL.Model.Project.Create);

        public ProjectConnection Projects(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectOrder>? orderBy = null, Arg<string>? search = null, Arg<IEnumerable<ProjectState>>? states = null) => this.CreateMethodCall(x => x.Projects(first, after, last, before, orderBy, search, states), Octokit.GraphQL.Model.ProjectConnection.Create);

        public string ProjectsResourcePath { get; }

        public string ProjectsUrl { get; }

        public bool ViewerCanCreateProjects { get; }

        internal static StubIProjectOwner Create(Expression expression)
        {
            return new StubIProjectOwner(expression);
        }
    }
}