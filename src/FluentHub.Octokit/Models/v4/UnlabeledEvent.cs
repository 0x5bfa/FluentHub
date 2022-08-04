namespace FluentHub.Octokit.Models.v4
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    /// <summary>
    /// Represents an 'unlabeled' event on a given issue or pull request.
    /// </summary>
    public class UnlabeledEvent
    {
        /// <summary>
        /// Identifies the actor who performed the event.
        /// </summary>
        public IActor Actor { get; set; }

        /// <summary>
        /// Identifies the date and time when the object was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Humanized string of "Identifies the date and time when the object was created."
        /// <summary>
        public string CreatedAtHumanized { get; set; }

        public ID Id { get; set; }

        /// <summary>
        /// Identifies the label associated with the 'unlabeled' event.
        /// </summary>
        public Label Label { get; set; }

        /// <summary>
        /// Identifies the `Labelable` associated with the event.
        /// </summary>
        public ILabelable Labelable { get; set; }
    }
}