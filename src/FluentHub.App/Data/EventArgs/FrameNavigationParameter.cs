namespace FluentHub.App.Models
{
    public class FrameNavigationParameter
    {
        /// <summary>
        /// The original URL.
        /// </summary>
        public string? Url { get; init; }

        /// <summary>
        /// User login.
        /// </summary>
        public string? Login { get; init; }

        /// <summary>
        /// Repository name.
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        /// Issue, PR, Discussion, Project, and more number.
        /// </summary>
        public int Number { get; init; }

        /// <summary>
        /// Gets or Sets whether the user wans to view as the authorized viewer.
        /// </summary>
        public bool AsViewer { get; init; }

        /// <summary>
        /// Other needed parameters.
        /// </summary>
        public List<object>? Parameters { get; init; }

        /// <summary>
        /// Page type of the URL.
        /// </summary>
        public Utils.UrlPageType PageType { get; init; }
    }
}
