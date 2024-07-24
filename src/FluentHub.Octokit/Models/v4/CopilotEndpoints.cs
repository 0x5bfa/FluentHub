// Copyright (c) 2022-2024 0x5BFA
// Licensed under the MIT License. See the LICENSE.

namespace FluentHub.Octokit.Models.v4
{
	/// <summary>
	/// Copilot endpoint information
	/// </summary>
	public class CopilotEndpoints
	{
		/// <summary>
		/// Copilot API endpoint
		/// </summary>
		public string Api { get; set; }

		/// <summary>
		/// Copilot origin tracker endpoint
		/// </summary>
		public string OriginTracker { get; set; }

		/// <summary>
		/// Copilot proxy endpoint
		/// </summary>
		public string Proxy { get; set; }

		/// <summary>
		/// Copilot telemetry endpoint
		/// </summary>
		public string Telemetry { get; set; }
	}
}
