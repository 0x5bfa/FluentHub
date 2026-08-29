namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// The value of the labels field in a Project item.
    /// </summary>
    public class ProjectV2ItemFieldLabelValue : QueryableValue<ProjectV2ItemFieldLabelValue>
    {
        internal ProjectV2ItemFieldLabelValue(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The field that contains this value.
        /// </summary>
        public ProjectV2FieldConfiguration Field => this.CreateProperty(x => x.Field, Octokit.GraphQL.Model.ProjectV2FieldConfiguration.Create);

        /// <summary>
        /// Labels value of a field
        /// </summary>
        /// <param name="first">Returns the first _n_ elements from the list.</param>
        /// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
        /// <param name="last">Returns the last _n_ elements from the list.</param>
        /// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
        public LabelConnection Labels(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null) => this.CreateMethodCall(x => x.Labels(first, after, last, before), Octokit.GraphQL.Model.LabelConnection.Create);

        internal static ProjectV2ItemFieldLabelValue Create(Expression expression)
        {
            return new ProjectV2ItemFieldLabelValue(expression);
        }
    }
}