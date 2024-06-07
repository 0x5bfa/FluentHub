// Copyright (c) 2023 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A team of users in an organization.
	/// </summary>
	public class Team
	{
		/// <summary>
		/// A list of teams that are ancestors of this team.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public TeamConnection Ancestors { get; set; }

		/// <summary>
		/// A URL pointing to the team's avatar.
		/// </summary>
		/// <param name="size">The size in pixels of the resulting square image.</param>
		public string AvatarUrl { get; set; }

		/// <summary>
		/// List of child teams belonging to this team
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="immediateOnly">Whether to list immediate child teams or all descendant child teams.</param>
		/// <param name="orderBy">Order for connection</param>
		/// <param name="userLogins">User logins to filter by</param>
		public TeamConnection ChildTeams { get; set; }

		/// <summary>
		/// The slug corresponding to the organization and team.
		/// </summary>
		public string CombinedSlug { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was created.
		/// </summary>
		public DateTimeOffset CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was created."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// Identifies the primary key from the database.
		/// </summary>
		public int? DatabaseId { get; set; }

		/// <summary>
		/// The description of the team.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Find a team discussion by its number.
		/// </summary>
		/// <param name="number">The sequence number of the discussion to find.</param>
		public TeamDiscussion Discussion { get; set; }

		/// <summary>
		/// A list of team discussions.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="isPinned">If provided, filters discussions according to whether or not they are pinned.</param>
		/// <param name="orderBy">Order for connection</param>
		public TeamDiscussionConnection Discussions { get; set; }

		/// <summary>
		/// The HTTP path for team discussions
		/// </summary>
		public string DiscussionsResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for team discussions
		/// </summary>
		public string DiscussionsUrl { get; set; }

		/// <summary>
		/// The HTTP path for editing this team
		/// </summary>
		public string EditTeamResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for editing this team
		/// </summary>
		public string EditTeamUrl { get; set; }

		/// <summary>
		/// The Node ID of the Team object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// A list of pending invitations for users to this team
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public OrganizationInvitationConnection Invitations { get; set; }

		/// <summary>
		/// Get the status messages members of this entity have set that are either public or visible only to the organization.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Ordering options for user statuses returned from the connection.</param>
		public UserStatusConnection MemberStatuses { get; set; }

		/// <summary>
		/// A list of users who are members of this team.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="membership">Filter by membership type</param>
		/// <param name="orderBy">Order for the connection.</param>
		/// <param name="query">The search string to look for.</param>
		/// <param name="role">Filter by team member role</param>
		public TeamMemberConnection Members { get; set; }

		/// <summary>
		/// The HTTP path for the team' members
		/// </summary>
		public string MembersResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for the team' members
		/// </summary>
		public string MembersUrl { get; set; }

		/// <summary>
		/// The name of the team.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The HTTP path creating a new team
		/// </summary>
		public string NewTeamResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL creating a new team
		/// </summary>
		public string NewTeamUrl { get; set; }

		/// <summary>
		/// The notification setting that the team has set.
		/// </summary>
		public TeamNotificationSetting NotificationSetting { get; set; }

		/// <summary>
		/// The organization that owns this team.
		/// </summary>
		public Organization Organization { get; set; }

		/// <summary>
		/// The parent team of the team.
		/// </summary>
		public Team ParentTeam { get; set; }

		/// <summary>
		/// The level of privacy the team has.
		/// </summary>
		public TeamPrivacy Privacy { get; set; }

		/// <summary>
		/// Finds and returns the project according to the provided project number.
		/// </summary>
		/// <param name="number">The Project number.</param>
		public ProjectV2 ProjectV2 { get; set; }

		/// <summary>
		/// List of projects this team has collaborator access to.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="filterBy">Filtering options for projects returned from this connection</param>
		/// <param name="orderBy">How to order the returned projects.</param>
		/// <param name="query">The query to search projects by.</param>
		public ProjectV2Connection ProjectsV2 { get; set; }

		/// <summary>
		/// A list of repositories this team has access to.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="orderBy">Order for the connection.</param>
		/// <param name="query">The search string to look for. Repositories will be returned where the name contains your search string.</param>
		public TeamRepositoryConnection Repositories { get; set; }

		/// <summary>
		/// The HTTP path for this team's repositories
		/// </summary>
		public string RepositoriesResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for this team's repositories
		/// </summary>
		public string RepositoriesUrl { get; set; }

		/// <summary>
		/// The HTTP path for this team
		/// </summary>
		public string ResourcePath { get; set; }

		/// <summary>
		/// What algorithm is used for review assignment for this team
		/// </summary>
		public TeamReviewAssignmentAlgorithm? ReviewRequestDelegationAlgorithm { get; set; }

		/// <summary>
		/// True if review assignment is enabled for this team
		/// </summary>
		public bool ReviewRequestDelegationEnabled { get; set; }

		/// <summary>
		/// How many team members are required for review assignment for this team
		/// </summary>
		public int? ReviewRequestDelegationMemberCount { get; set; }

		/// <summary>
		/// When assigning team members via delegation, whether the entire team should be notified as well.
		/// </summary>
		public bool ReviewRequestDelegationNotifyTeam { get; set; }

		/// <summary>
		/// The slug corresponding to the team.
		/// </summary>
		public string Slug { get; set; }

		/// <summary>
		/// The HTTP path for this team's teams
		/// </summary>
		public string TeamsResourcePath { get; set; }

		/// <summary>
		/// The HTTP URL for this team's teams
		/// </summary>
		public string TeamsUrl { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }

		/// <summary>
		/// The HTTP URL for this team
		/// </summary>
		public string Url { get; set; }

		/// <summary>
		/// Team is adminable by the viewer.
		/// </summary>
		public bool ViewerCanAdminister { get; set; }

		/// <summary>
		/// Check if the viewer is able to change their subscription status for the repository.
		/// </summary>
		public bool ViewerCanSubscribe { get; set; }

		/// <summary>
		/// Identifies if the viewer is watching, not watching, or ignoring the subscribable entity.
		/// </summary>
		public SubscriptionState? ViewerSubscription { get; set; }
	}
}
