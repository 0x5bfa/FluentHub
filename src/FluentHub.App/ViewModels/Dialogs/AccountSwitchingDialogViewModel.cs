using FluentHub.App.Utils;

namespace FluentHub.App.ViewModels.Dialogs
{
	public class AccountSwitchingDialogViewModel : ObservableObject
	{
		public AccountSwitchingDialogViewModel(IMessenger messenger = null, ILogger logger = null)
		{
			_logger = logger;
			_messenger = messenger;
		}

		#region Fields and Properties
		private readonly ILogger _logger;
		private readonly IMessenger _messenger;
		#endregion
	}
}
