using FluentHub.App.Models;
using FluentHub.App.Services;
using FluentHub.App.ViewModels.UserControls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.App.UserControls
{
	public sealed partial class ReferencedSubjectButton : UserControl
	{
		#region propdp
		public static readonly DependencyProperty SubjectProperty =
			DependencyProperty.Register(
				nameof(Subject),
				typeof(ReferencedSubject),
				typeof(ReferencedSubjectButton),
				new PropertyMetadata(null));

		public ReferencedSubject Subject
		{
			get => (ReferencedSubject)GetValue(SubjectProperty);
			set
			{
				SetValue(SubjectProperty, value);
			}
		}
		#endregion

		public ReferencedSubjectButton()
		{
			InitializeComponent();
		}
	}
}
