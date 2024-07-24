// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A repository ruleset.
	/// </summary>
	public class RepositoryRuleset
	{
		/// <summary>
		/// The actors that can bypass this ruleset
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		public RepositoryRulesetBypassActorConnection BypassActors { get; set; }

		/// <summary>
		/// The set of conditions that must evaluate to true for this ruleset to apply
		/// </summary>
		public RepositoryRuleConditions Conditions { get; set; }

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
		/// The enforcement level of this ruleset
		/// </summary>
		public RuleEnforcement Enforcement { get; set; }

		/// <summary>
		/// The Node ID of the RepositoryRuleset object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Name of the ruleset.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// List of rules.
		/// </summary>
		/// <param name="first">Returns the first _n_ elements from the list.</param>
		/// <param name="after">Returns the elements in the list that come after the specified cursor.</param>
		/// <param name="last">Returns the last _n_ elements from the list.</param>
		/// <param name="before">Returns the elements in the list that come before the specified cursor.</param>
		/// <param name="type">The type of rule.</param>
		public RepositoryRuleConnection Rules { get; set; }

		/// <summary>
		/// Source of ruleset.
		/// </summary>
		public RuleSource Source { get; set; }

		/// <summary>
		/// Target of the ruleset.
		/// </summary>
		public RepositoryRulesetTarget? Target { get; set; }

		/// <summary>
		/// Identifies the date and time when the object was last updated.
		/// </summary>
		public DateTimeOffset UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the object was last updated."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }
	}
}
