namespace FluentHub.Octokit.Mutations
{
	public class RemoveStarMutation
	{
		public async Task Execute(ID starrableRepoId)
		{
			var payload = new Mutation()
			.RemoveStar(new(new() { StarrableId = starrableRepoId }))
			.Select(x => new RemoveStarPayload
			{
				ClientMutationId = x.ClientMutationId,
			})
			.Compile();

			var response = await App.Connection.Run(payload);
		}
	}
}
