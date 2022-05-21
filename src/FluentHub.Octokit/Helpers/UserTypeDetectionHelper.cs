namespace FluentHub.Octokit.Helpers
{
    public static class UserTypeDetectionHelper
    {
        /// <summary>
        /// Detects whether user type is <see cref="Organization"/> or <see cref="User"/> from GraphQL Id
        /// </summary>
        /// <param name="id">ID for detecting</param>
        /// <returns></returns>
        public static bool IsOrganization(ID id)
        {
            // Starts with "O_"
            if (id.ToString()[0] == 'O' && id.ToString()[1] == '_')
                return true;
            else
                return false;
        }
    }
}
