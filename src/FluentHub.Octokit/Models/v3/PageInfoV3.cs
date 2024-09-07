namespace FluentHub.Octokit.Models.v3
{
	public class PageInfoV3
	{
		/// <summary>
		/// Specify the start page for pagination actions
		/// </summary>
		/// <remarks>
		/// Page numbering is 1-based on the server
		/// </remarks>
		public int? StartPage { get; set; }

		/// <summary>
		/// Specify the number of pages to return
		/// </summary>
		public int? PageCount { get; set; }

		/// <summary>
		/// Specify the number of results to return for each page
		/// </summary>
		/// <remarks>
		/// Results returned may be less than this total if you reach the final page of results
		/// </remarks>
		public int? PageSize { get; set; }
	}
}
