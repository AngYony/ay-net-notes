using LearningTagApp.ViewModels;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace LearningTagApp.Views
{
    /// <summary>
    /// Interaction logic for UserRegistWindow.xaml
    /// </summary>
    public partial class UserRegistWindow : Window
    {
        public UserRegistWindow()
        {
            InitializeComponent();
            (DataContext as UserRegistWindowViewModel).View = this;

        }

        private void txtPhone_Error(object sender, System.Windows.Controls.ValidationErrorEventArgs e)
        {
            var errs = Validation.GetErrors(sender as DependencyObject);
            if (errs.Count > 0)
            {
                this.txtPhone.ToolTip = errs[0].ErrorContent.ToString();

            }
            else
            {
                this.txtPhone.ToolTip = null;
            }
        }
    }
}
