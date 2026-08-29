namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// A view within a ProjectV2.
    /// </summary>
    public class ProjectV2View : QueryableValue<ProjectV2View>
    {
        internal ProjectV2View(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; }

        /// <summary>
        /// Identifies the primary key from the database.
        /// </summary>
        public long? DatabaseId { get; }

        /// <summary>
        /// The view's visible fields.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for the project v2 fields returned from the connection.</param>
        public ProjectV2FieldConfigurationConnection Fields(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectV2FieldOrder>? orderBy = null) => this.CreateMethodCall(x => x.Fields(first, after, last, before, orderBy), Octokit.GraphQL.Model.ProjectV2FieldConfigurationConnection.Create);

        /// <summary>
        /// The project view's filter.
        /// </summary>
        public string Filter { get; }

        /// <summary>
        /// The view's group-by field.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for the project v2 fields returned from the connection.</param>
        public ProjectV2FieldConnection GroupBy(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectV2FieldOrder>? orderBy = null) => this.CreateMethodCall(x => x.GroupBy(first, after, last, before, orderBy), Octokit.GraphQL.Model.ProjectV2FieldConnection.Create);

        /// <summary>
        /// The view's group-by field.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for the project v2 fields returned from the connection.</param>
        public ProjectV2FieldConfigurationConnection GroupByFields(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectV2FieldOrder>? orderBy = null) => this.CreateMethodCall(x => x.GroupByFields(first, after, last, before, orderBy), Octokit.GraphQL.Model.ProjectV2FieldConfigurationConnection.Create);

        /// <summary>
        /// The Node ID of the ProjectV2View object
        /// </summary>
        public ID Id { get; }

        /// <summary>
        /// The project view's layout.
        /// </summary>
        public ProjectV2ViewLayout Layout { get; }

        /// <summary>
        /// The project view's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The project view's number.
        /// </summary>
        public int Number { get; }

        /// <summary>
        /// The project that contains this view.
        /// </summary>
        public ProjectV2 Project => this.CreateProperty(x => x.Project, Octokit.GraphQL.Model.ProjectV2.Create);

        /// <summary>
        /// The view's sort-by config.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProjectV2SortByConnection SortBy(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.SortBy(first, after, last, before), Octokit.GraphQL.Model.ProjectV2SortByConnection.Create);

        /// <summary>
        /// The view's sort-by config.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public ProjectV2SortByFieldConnection SortByFields(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.SortByFields(first, after, last, before), Octokit.GraphQL.Model.ProjectV2SortByFieldConnection.Create);

        /// <summary>
        /// Identifies the date and time when the object was last updated.
        /// </summary>
        public DateTimeOffset UpdatedAt { get; }

        /// <summary>
        /// The view's vertical-group-by field.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for the project v2 fields returned from the connection.</param>
        public ProjectV2FieldConnection VerticalGroupBy(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectV2FieldOrder>? orderBy = null) => this.CreateMethodCall(x => x.VerticalGroupBy(first, after, last, before, orderBy), Octokit.GraphQL.Model.ProjectV2FieldConnection.Create);

        /// <summary>
        /// The view's vertical-group-by field.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for the project v2 fields returned from the connection.</param>
        public ProjectV2FieldConfigurationConnection VerticalGroupByFields(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectV2FieldOrder>? orderBy = null) => this.CreateMethodCall(x => x.VerticalGroupByFields(first, after, last, before, orderBy), Octokit.GraphQL.Model.ProjectV2FieldConfigurationConnection.Create);

        /// <summary>
        /// The view's visible fields.
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        /// <param name="orderBy">Ordering options for the project v2 fields returned from the connection.</param>
        public ProjectV2FieldConnection VisibleFields(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<ProjectV2FieldOrder>? orderBy = null) => this.CreateMethodCall(x => x.VisibleFields(first, after, last, before, orderBy), Octokit.GraphQL.Model.ProjectV2FieldConnection.Create);

        internal static ProjectV2View Create(Expression expression)
        {
            return new ProjectV2View(expression);
        }
    }
}