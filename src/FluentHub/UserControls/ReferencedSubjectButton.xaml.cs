using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.UserControls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Contracts;

namespace FluentHub.UserControls
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
