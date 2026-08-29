namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The values that can be used to update a field of an item inside a Project. Only 1 value can be updated at a time.
    /// </summary>
    public class ProjectV2FieldValue
    {
        /// <summary>
        /// The text to set on the field.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The number to set on the field.
        /// </summary>
        public double? Number { get; set; }

        /// <summary>
        /// The ISO 8601 date to set on the field.
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// The id of the single select option to set on the field.
        /// </summary>
        public string SingleSelectOptionId { get; set; }

        /// <summary>
        /// The id of the iteration to set on the field.
        /// </summary>
        public string IterationId { get; set; }
    }
}