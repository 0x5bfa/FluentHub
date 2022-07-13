using FluentHub.Uwp.Services;
using FluentHub.Uwp.Services.Navigation;
using FluentHub.Uwp.ViewModels;
using FluentHub.Uwp.ViewModels.Repositories;
using FluentHub.Uwp.ViewModels.Repositories.Codes;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Media.Imaging;

namespace FluentHub.Uwp.Views.Repositories.Codes
{
    public sealed partial class CodePage : Page
    {
        public CodePage() => InitializeComponent();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _ = App.Settings.UseDetailsView ?
                CodeViewLayout.Navigate(typeof(Layouts.DetailsLayoutView), e.Parameter) :
                CodeViewLayout.Navigate(typeof(Layouts.TreeLayoutView), e.Parameter);
        }
    }
}
