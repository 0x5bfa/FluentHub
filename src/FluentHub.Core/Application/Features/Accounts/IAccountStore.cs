namespace FluentHub.Core.Application
{
	public interface IAccountStore
	{
		string SignedInUserLogins { get; set; }
	}
}
