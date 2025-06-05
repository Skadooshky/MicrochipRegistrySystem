using System.Windows;
using System.Windows.Controls;
using NationalMicrochipRegistry.App.ViewModels;

namespace NationalMicrochipRegistry.App.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }

        // Bind the password box’s Password to the VM
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel vm)
            {
                vm.Password = (sender as PasswordBox)?.Password ?? string.Empty;
            }
        }
    }
}
