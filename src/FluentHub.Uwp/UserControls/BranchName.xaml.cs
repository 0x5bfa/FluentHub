using FluentHub.Uwp.Models;
using FluentHub.Uwp.Services;
using FluentHub.Uwp.ViewModels.UserControls;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace FluentHub.Uwp.UserControls
{
    public sealed partial class BranchName : UserControl
    {
        #region propdp
        public static readonly DependencyProperty BranchNameProperty =
            DependencyProperty.Register(
                nameof(Branch),
                typeof(string),
                typeof(BranchName),
                new PropertyMetadata(null));

        public string Branch
        {
            get => (string)GetValue(BranchNameProperty);
            set
            {
                SetValue(BranchNameProperty, value);
                DataContext = Branch;
            }
        }
        #endregion

        public BranchName()
            => InitializeComponent();
    }
}
