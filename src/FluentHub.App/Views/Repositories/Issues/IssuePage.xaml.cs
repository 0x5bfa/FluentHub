using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.Repositories.Issues;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;

namespace FluentHub.App.Views.Repositories.Issues
{
    public sealed partial class IssuePage : Page
    {
        public IssuePage()
        {
            InitializeComponent();

            var provider = App.Current.Services;
            ViewModel = provider.GetRequiredService<IssueViewModel>();
            _navigation = provider.GetRequiredService<INavigationService>();
        }

        public IssueViewModel ViewModel { get; }
        private readonly INavigationService _navigation;

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var param = e.Parameter as FrameNavigationParameter;

            if (param == null)
            {
                throw new ArgumentNullException(nameof(param), "OnNavigateTo() failed to load.");
            }

            ViewModel.Login = param.Login;
            ViewModel.Name = param.Name;
            ViewModel.Number = param.Number;

            var command = ViewModel.LoadRepositoryIssuePageCommand;
            if (command.CanExecute(null))
                command.Execute(null);
        }
    }
}
