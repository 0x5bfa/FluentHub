namespace FluentHub.Octokit.v4.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a type that can be retrieved by a URL.
    /// </summary>
    public interface IUniformResourceLocatable
    {
        /// <summary>
        /// The HTML path to this resource.
        /// </summary>
        string ResourcePath { get; set; }

        /// <summary>
        /// The URL to this resource.
        /// </summary>
        string Url { get; set; }
    }
}

namespace FluentHub.Octokit.v4.Model.Internal
{
    using System;
    using System.Collections.Generic;

    internal class StubIUniformResourceLocatable
    {
        

        public string ResourcePath { get; set; }

        public string Url { get; set; }
    }
}