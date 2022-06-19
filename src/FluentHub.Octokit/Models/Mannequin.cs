namespace FluentHub.Octokit.Models
{
    public class Mannequin
    {
        public string AvatarUrl { get; set; }

        public User Claimant { get; set; }

        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedAtHumanized { get; set; }

        public int? DatabaseId { get; set; }

        public string Email { get; set; }

        public string Id { get; set; }

        public string Login { get; set; }

        public string ResourcePath { get; set; }

        public DateTimeOffset UpdatedAt { get; set; }
        public string UpdatedAtHumanized { get; set; }

        public string Url { get; set; }
    }
}
