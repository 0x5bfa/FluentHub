using FluentHub.App.ViewModels.UserControls.BlockButtons;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls.BlockButtons
{
	public sealed partial class ProjectBlockButton : UserControl
	{
		#region dprops
		public static readonly DependencyProperty ViewModelProperty
			= DependencyProperty.Register(
				  nameof(Project),
				  typeof(ProjectBlockButtonViewModel),
				  typeof(ProjectBlockButton),
				  new PropertyMetadata(null)
				);

		public ProjectBlockButtonViewModel ViewModel
		{
			get => (ProjectBlockButtonViewModel)GetValue(ViewModelProperty);
			set
			{
				SetValue(ViewModelProperty, value);
				DataContext = ViewModel;
			}
		}
		#endregion

		public ProjectBlockButton()
			=> InitializeComponent();

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			//var navService = Ioc.Default.GetRequiredService<INavigationService>();
			//navService.Navigate<ProjectPage>(
			//	new FrameNavigationArgs()
			//	{
			//		Login = ViewModel.Item.Repository.Owner.Login,
			//		Name = ViewModel.Item.Repository.Name,
			//		Number = ViewModel.Item.Number,
			//	});
		}
	}
}
