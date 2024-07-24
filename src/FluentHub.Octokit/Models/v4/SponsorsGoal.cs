// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A goal associated with a GitHub Sponsors listing, representing a target the sponsored maintainer would like to attain.
	/// </summary>
	public class SponsorsGoal
	{
		/// <summary>
		/// A description of the goal from the maintainer.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// What the objective of this goal is.
		/// </summary>
		public SponsorsGoalKind Kind { get; set; }

		/// <summary>
		/// The percentage representing how complete this goal is, between 0-100.
		/// </summary>
		public int PercentComplete { get; set; }

		/// <summary>
		/// What the goal amount is. Represents an amount in USD for monthly sponsorship amount goals. Represents a count of unique sponsors for total sponsors count goals.
		/// </summary>
		public int TargetValue { get; set; }

		/// <summary>
		/// A brief summary of the kind and target value of this goal.
		/// </summary>
		public string Title { get; set; }
	}
}
