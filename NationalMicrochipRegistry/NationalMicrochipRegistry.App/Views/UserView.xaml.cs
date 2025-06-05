using System.Windows;
using System.Windows.Controls;
using NationalMicrochipRegistry.App.ViewModels;

namespace NationalMicrochipRegistry.App.Views
{
    /// <summary>
    /// Interaction logic for UserView.xaml
    /// </summary>
    public partial class UserView : UserControl
    {
        public UserView()
        {
            InitializeComponent();
            DataContext = new UserViewModel();
        }

        // Route the PasswordBox’s password into the VM’s CurrentUser.PasswordHashw
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm && sender is PasswordBox pb)
            {
                // Instead of vm.CurrentUser.PasswordHash = pb.Password;
                vm.Password = pb.Password;
            }
        }
    }
}
