namespace FluentHub.App.Models
{
    public class DetailsLayoutListViewModel
    {
        public string IconGlyph { get; set; }

        public string Name { get; set; }

        public string LatestCommitMessage { get; set; }

        public string Type { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }

        public string UpdatedAtHumanized { get; set; }
    }
}
