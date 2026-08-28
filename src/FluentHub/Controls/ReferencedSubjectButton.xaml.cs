using FluentHub.Models;
using FluentHub.Services;
using FluentHub.ViewModels.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using FluentHub.Core.Application.Models;

namespace FluentHub.Controls
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
