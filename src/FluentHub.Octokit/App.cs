namespace FluentHub.Octokit
{
    public static class App
    {
        #region Properties
        public static ProductHeaderValue ProductInformation { get; set; } = new ProductHeaderValue("FluentHub");
        public static Connection Connection { get; set; }

        public static OctokitOriginal.GitHubClient Client { get; set; }
            = new OctokitOriginal.GitHubClient(new OctokitOriginal.ProductHeaderValue("FluentHub"));

        public static string AccessToken { get; set; }
        public static string SignedInUserName { get; set; }
        #endregion

        static App()
        {
        }

        public static void Initialize()
        {
            Client.Credentials = new OctokitOriginal.Credentials(AccessToken);
            Connection = new(ProductInformation, AccessToken);
        }
    }
}
