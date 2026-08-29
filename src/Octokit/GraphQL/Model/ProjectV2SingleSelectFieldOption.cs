namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Octokit.GraphQL.Core;
    using Octokit.GraphQL.Core.Builders;

    /// <summary>
    /// Single select field option for a configuration for a project.
    /// </summary>
    public class ProjectV2SingleSelectFieldOption : QueryableValue<ProjectV2SingleSelectFieldOption>
    {
        internal ProjectV2SingleSelectFieldOption(Expression expression) : base(expression)
        {
        }

        /// <summary>
        /// The option's display color.
        /// </summary>
        public ProjectV2SingleSelectFieldOptionColor Color { get; }

        /// <summary>
        /// The option's plain-text description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// The option's description, possibly containing HTML.
        /// </summary>
        public string DescriptionHTML { get; }

        /// <summary>
        /// The option's ID.
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// The option's name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The option's html name.
        /// </summary>
        public string NameHTML { get; }

        internal static ProjectV2SingleSelectFieldOption Create(Expression expression)
        {
            return new ProjectV2SingleSelectFieldOption(expression);
        }
    }
}