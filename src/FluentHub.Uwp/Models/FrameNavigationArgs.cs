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
        public List<object> Parameters { get; set; } = new();

        /// <summary>
        /// Page type of the URL
        /// </summary>
        public Utils.UrlPageType PageType { get; set; }

        public static FrameNavigationArgs Create(string owner, string name, Utils.UrlPageType pageType, int number)
        {
            return new()
            {
                Login = owner,
                Name = name,
                Number = number,
                PageType = pageType,
            };
        }
    }
}
