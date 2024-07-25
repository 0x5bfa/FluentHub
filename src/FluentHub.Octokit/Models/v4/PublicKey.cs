// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// A user's public key.
	/// </summary>
	public class PublicKey
	{
		/// <summary>
		/// The last time this authorization was used to perform an action. Values will be null for keys not owned by the user.
		/// </summary>
		public DateTimeOffset? AccessedAt { get; set; }

		/// <summary>
		/// Humanized string of "The last time this authorization was used to perform an action. Values will be null for keys not owned by the user."
		/// <summary>
		public string AccessedAtHumanized { get; set; }

		/// <summary>
		/// Identifies the date and time when the key was created. Keys created before March 5th, 2014 have inaccurate values. Values will be null for keys not owned by the user.
		/// </summary>
		public DateTimeOffset? CreatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the key was created. Keys created before March 5th, 2014 have inaccurate values. Values will be null for keys not owned by the user."
		/// <summary>
		public string CreatedAtHumanized { get; set; }

		/// <summary>
		/// The fingerprint for this PublicKey.
		/// </summary>
		public string Fingerprint { get; set; }

		/// <summary>
		/// The Node ID of the PublicKey object
		/// </summary>
		public ID Id { get; set; }

		/// <summary>
		/// Whether this PublicKey is read-only or not. Values will be null for keys not owned by the user.
		/// </summary>
		public bool? IsReadOnly { get; set; }

		/// <summary>
		/// The public key string.
		/// </summary>
		public string Key { get; set; }

		/// <summary>
		/// Identifies the date and time when the key was updated. Keys created before March 5th, 2014 may have inaccurate values. Values will be null for keys not owned by the user.
		/// </summary>
		public DateTimeOffset? UpdatedAt { get; set; }

		/// <summary>
		/// Humanized string of "Identifies the date and time when the key was updated. Keys created before March 5th, 2014 may have inaccurate values. Values will be null for keys not owned by the user."
		/// <summary>
		public string UpdatedAtHumanized { get; set; }
	}
}
