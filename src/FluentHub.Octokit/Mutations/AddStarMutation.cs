using Octokit.GraphQL;
using System;
using System.Collections.Generic;
using System.Text;

namespace FluentHub.Octokit.Mutations
{
	public class AddStarMutation
	{
		public async Task Set(ID starrableRepoId)
		{
			var payload = new Mutation()
			.AddStar(new(new() { StarrableId = starrableRepoId }));

			var response = await App.Connection.Run(payload);
		}
	}
}
