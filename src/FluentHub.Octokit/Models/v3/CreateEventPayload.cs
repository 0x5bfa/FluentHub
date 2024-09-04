namespace FluentHub.Octokit.Models.v3
{
	public class CreateEventPayload : ActivityPayload
	{
		public string Ref { get; set; }

		//public StringEnum<RefType> RefType { get; set; }

		public string MasterBranch { get; set; }

		public string Description { get; set; }
	}
}
