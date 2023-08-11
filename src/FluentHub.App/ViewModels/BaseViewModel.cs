using FluentHub.Octokit.Queries.Repositories;
using FluentHub.App.Helpers;
using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories;
using FluentHub.App.ViewModels.UserControls;
using FluentHub.App.ViewModels.UserControls.Overview;
using FluentHub.App.ViewModels.UserControls.BlockButtons;
using FluentHub.App.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;

namespace FluentHub.App.ViewModels
{
	public abstract class BaseViewModel : ObservableObject
	{
		protected readonly IMessenger _messenger;
		protected readonly ILogger _logger;
		protected readonly INavigationService _navigation;

		protected string _login;
		public string Login { get => _login; set => SetProperty(ref _login, value); }

		protected string _name;
		public string Name { get => _name; set => SetProperty(ref _name, value); }

		protected int _number;
		public int Number { get => _number; set => SetProperty(ref _number, value); }

		private Exception _taskException;
		public Exception TaskException { get => _taskException; set => SetProperty(ref _taskException, value); }

		protected bool _IsTaskFaulted;
		public bool IsTaskFaulted { get => _IsTaskFaulted; set => SetProperty(ref _IsTaskFaulted, value); }

		protected BaseViewModel()
		{
			// Dependency Injection
			_logger = Ioc.Default.GetRequiredService<ILogger>();
			_messenger = Ioc.Default.GetRequiredService<IMessenger>();
			_navigation = Ioc.Default.GetRequiredService<INavigationService>();
		}

		protected void SetTabInformation(
			string? header = null,
			string? description = null,
			string? imageIconSourceSimplified = null)
		{
			var currentItem = _navigation.TabView.SelectedItem.NavigationHistory.CurrentItem;
			if (currentItem is null)
				return;

			if (!string.IsNullOrEmpty(header))
				currentItem.Header = header;

			if (!string.IsNullOrEmpty(header))
				currentItem.Description = description;

			if (!string.IsNullOrEmpty(header))
			{
				currentItem.Icon = new ImageIconSource()
				{
					ImageSource = new BitmapImage(new Uri($"ms-appx:///Assets/Icons/{imageIconSourceSimplified}.png"))
				};
			}
		}
	}
}
