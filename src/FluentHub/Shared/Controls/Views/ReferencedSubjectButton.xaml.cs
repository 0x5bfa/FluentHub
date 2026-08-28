using FluentHub.Models;
using FluentHub.Services;
using FluentHub.Shared.Controls.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Application.Models;

namespace FluentHub.Shared.Controls.Views
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
