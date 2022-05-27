namespace FluentHub.Octokit.Settings
{
    public class SettingsModel : SettingsManager
    {
        public string AccessToken
        {
            get => Get(nameof(AccessToken), "");
            set => Set(nameof(AccessToken), value);
        }

        public string SignedInUserName
        {
            get => Get(nameof(SignedInUserName), "");
            set => Set(nameof(SignedInUserName), value);
        }
    }
}
