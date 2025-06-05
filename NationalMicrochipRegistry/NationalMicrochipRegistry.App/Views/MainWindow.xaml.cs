using System.Windows;
using NationalMicrochipRegistry.App.Helpers;    
using NationalMicrochipRegistry.Data.Enums;    

namespace NationalMicrochipRegistry.App.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var currentRole = SessionManager.CurrentUser?.Role ?? UserRole.User;

            if (currentRole == UserRole.User)
            {
                // Plain “User”: hide both Microchips and Users tabs
                MicrochipsTab.Visibility = Visibility.Collapsed;
                UsersTab.Visibility = Visibility.Collapsed;
            }
            else if (currentRole == UserRole.RegistryAdministrator)
            {
                // Registry Administrator: hide only the Users tab
                UsersTab.Visibility = Visibility.Collapsed;
            }
            // SystemAdministrator and Tester keep all tabs visible
        }
    }
}
