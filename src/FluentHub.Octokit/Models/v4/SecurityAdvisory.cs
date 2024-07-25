// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A GitHub Security Advisory
	/// </summary>
	public class SecurityAdvisory
	{
		/// <summary>
		/// The classification of the advisory
		/// </summary>
		public SecurityAdvisoryClassification Classification { get; set; }

		/// <summary>
		/// The CVSS associated with this advisory
		/// </summary>
		public CVSS Cvss { get; set; }

		/// <summary>
		/// CWEs associated with this Advisory
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public CWEConnection Cwes { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// This is a long plaintext description of the advisory
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The GitHub Security Advisory ID
		/// </summary>
		public string GhsaId { get; set; }

		/// <summary>
		/// The Node ID of the SecurityAdvisory object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// A list of identifiers for this advisory
		/// </summary>
		public List<SecurityAdvisoryIdentifier> Identifiers { get; set; }

		/// <summary>
		/// The permalink for the advisory's dependabot alerts page
		/// </summary>
		public string NotificationsPermalink { get; set; }

		/// <summary>
		/// The organization that originated the advisory
		/// </summary>
		public string Origin { get; set; }

		/// <summary>
		/// The permalink for the advisory
		/// </summary>
		public string Permalink { get; set; }

		/// <summary>
		/// When the advisory was published
		/// </summary>
		public DateTimeOffset PublishedAt { get; set; }

		/// <summary>
		/// Humanized string of "When the advisory was published"
		/// <summary>
		public string PublishedAtHumanized { get; set; }

		/// <summary>
		/// A list of references for this advisory
		/// </summary>
		public List<SecurityAdvisoryReference> References { get; set; }

		/// <summary>
		/// The severity of the advisory
		/// </summary>
		public SecurityAdvisorySeverity Severity { get; set; }

		/// <summary>
		/// A short plaintext summary of the advisory
		/// </summary>
		public string Summary { get; set; }

		/// <summary>
		/// When the advisory was last updated
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "When the advisory was last updated"
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// Vulnerabilities associated with this Advisory
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="classifications">A list of advisory classifications to filter vulnerabilities by.</param>
		/// <param name="ecosystem">An ecosystem to filter vulnerabilities by.</param>
		/// <param name="orderBy">Ordering options for the returned topics.</param>
		/// <param name="package">A package name to filter vulnerabilities by.</param>
		/// <param name="severities">A list of severities to filter vulnerabilities by.</param>
		public SecurityVulnerabilityConnection Vulnerabilities { get; set; }

		/// <summary>
		/// When the advisory was withdrawn, if it has been withdrawn
		/// </summary>
		public DateTimeOffset? WithdrawnAt { get; set; }

		/// <summary>
		/// Humanized string of "When the advisory was withdrawn, if it has been withdrawn"
		/// <summary>
		public string WithdrawnAtHumanized { get; set; }
	}
}
