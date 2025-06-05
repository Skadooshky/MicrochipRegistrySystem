using NationalMicrochipRegistry.App.Commands;
using NationalMicrochipRegistry.App.Helpers;
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;
using NationalMicrochipRegistry.Business.Services;
using NationalMicrochipRegistry.Data.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace NationalMicrochipRegistry.App.ViewModels
{
    /// <summary>
    /// ViewModel for the login screen. 
    /// Uses IUserService to authenticate.
    /// </summary>
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _username = string.Empty;
        private string _password = string.Empty;
        private readonly IUserService _userService;

        public LoginViewModel()
        {
            // Use the UserService with a UserRepository
            _userService = new UserService(new UserRepository());
            LoginCommand = new RelayCommand(_ => ExecuteLogin(), _ => CanExecuteLogin());
        }

        /// <summary>
        /// The username entered.
        /// </summary>
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
                ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// The password entered (plain-text for prototype).
        /// </summary>
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
                ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        /// Command to attempt login.
        /// </summary>
        public ICommand LoginCommand { get; }

        private bool CanExecuteLogin()
        {
            return !string.IsNullOrWhiteSpace(Username)
                   && !string.IsNullOrWhiteSpace(Password);
        }

        private void ExecuteLogin()
        {
            try
            {
                // Authenticate now returns a User? instead of a bool
                User? loggedInUser = _userService.Authenticate(Username.Trim(), Password);

                if (loggedInUser != null)
                {
                    // Store the authenticated user for the rest of the application
                    SessionManager.CurrentUser = loggedInUser;

                    // Open MainWindow
                    var main = new Views.MainWindow();
                    main.Show();

                    // Close the login window (assumes it's the first window in Application.Current.Windows)
                    Application.Current.Windows[0]?.Close();
                }
                else
                {
                    MessageBox.Show(
                        "Invalid username or password.",
                        "Login Failed",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error during login:\n{ex.Message}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        #endregion
    }
}
