namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A property that must match
    /// </summary>
    public class PropertyTargetDefinitionInput
    {
        /// <summary>
        /// The name of the property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The values to match for
        /// </summary>
        public IEnumerable<string> PropertyValues { get; set; }
    }
}