namespace FluentHub.Uwp.Models
{
    public class FrameNavigationArgs
    {
        /// <summary>
        /// The original URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// User login
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Repository name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Issue, PR, Discussion, Project, and more number
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// Other needed parameters
        /// </summary>
        public object Parameters { get; set; }

        /// <summary>
        /// Page type of the URL
        /// </summary>
        public Utils.UrlPageType PageType { get; set; }
    }
}
