using NationalMicrochipRegistry.App.Commands;
using NationalMicrochipRegistry.App.Helpers;     
using NationalMicrochipRegistry.Business.Interfaces;
using NationalMicrochipRegistry.Business.Repositories;
using NationalMicrochipRegistry.Business.Services;
using NationalMicrochipRegistry.Data.Enums;
using NationalMicrochipRegistry.Data.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NationalMicrochipRegistry.App.ViewModels
{
    /// <summary>
    /// ViewModel for UserView: lists all users and allows add/edit/delete
    /// only if the logged‐in user is SystemAdministrator or Tester.
    /// Plain “User” role sees the grid read‐only.
    /// </summary>
    public class UserViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private readonly UserRole _currentRole;

        public UserViewModel()
        {
            // Determine the logged‐in user from SessionManager (populated during login)
            var loggedIn = SessionManager.CurrentUser;
            _currentRole = loggedIn?.Role ?? UserRole.User;

            // Initialize service
            var userRepo = new UserRepository();
            _userService = new UserService(userRepo);

            Users = new ObservableCollection<User>();
            RefreshUsers();

            // Populate RolesList from the enum
            RolesList = new ObservableCollection<UserRole>(
                Enum.GetValues(typeof(UserRole)).Cast<UserRole>());

            // Initialize commands
            AddCommand = new RelayCommand(_ => ExecuteAdd(), _ => CanExecuteAdd());
            EditCommand = new RelayCommand(_ => ExecuteEdit(), _ => CanExecuteEdit());
            DeleteCommand = new RelayCommand(_ => ExecuteDelete(), _ => CanExecuteDelete());
            RefreshCommand = new RelayCommand(_ => RefreshUsers());

            // Default inputs
            CurrentUsername = string.Empty;
            CurrentPassword = string.Empty;
            SelectedRole = RolesList.FirstOrDefault();
            SelectedUser = null!;
        }

        /// <summary>
        /// True if the logged‐in user’s role is plain “User.”
        /// They may only view the list, not modify it.
        /// </summary>
        public bool IsOwner => _currentRole == UserRole.User;

        /// <summary>
        /// True if the logged‐in user’s role is “SystemAdministrator” or “Tester.”
        /// They may add, edit, or delete other users.
        /// </summary>
        public bool IsAdmin =>
               _currentRole == UserRole.SystemAdministrator
            || _currentRole == UserRole.Tester;

        /// <summary>
        /// The list of all users in the system.
        /// </summary>
        public ObservableCollection<User> Users { get; }

        private User? _selectedUser;
        public User? SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;
                OnPropertyChanged(nameof(SelectedUser));

                if (_selectedUser != null)
                {
                    // Populate input fields for editing
                    CurrentUsername = _selectedUser.Username;
                    CurrentPassword = _selectedUser.PasswordHash;
                    SelectedRole = _selectedUser.Role;
                }
                else
                {
                    // Clear inputs
                    CurrentUsername = string.Empty;
                    CurrentPassword = string.Empty;
                    SelectedRole = RolesList.FirstOrDefault();
                }

                RaiseAllCanExecuteChanged();
            }
        }

        private string _currentUsername = string.Empty;
        public string CurrentUsername
        {
            get => _currentUsername;
            set
            {
                _currentUsername = value;
                OnPropertyChanged(nameof(CurrentUsername));
                RaiseAllCanExecuteChanged();
            }
        }

        private string _currentPassword = string.Empty;
        public string CurrentPassword
        {
            get => _currentPassword;
            set
            {
                _currentPassword = value;
                OnPropertyChanged(nameof(CurrentPassword));
                RaiseAllCanExecuteChanged();
            }
        }

        public ObservableCollection<UserRole> RolesList { get; }

        private UserRole _selectedRole;
        public UserRole SelectedRole
        {
            get => _selectedRole;
            set
            {
                _selectedRole = value;
                OnPropertyChanged(nameof(SelectedRole));
                RaiseAllCanExecuteChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        #region Refresh / Load

        /// <summary>
        /// Reloads the Users list from the service.
        /// </summary>
        private void RefreshUsers()
        {
            Users.Clear();
            foreach (var u in _userService.GetAllUsers())
                Users.Add(u);
            OnPropertyChanged(nameof(Users));
        }

        #endregion

        #region Add Logic

        private bool CanExecuteAdd()
        {
            if (!IsAdmin) return false;

            // Username, Password, and Role must be non‐empty/defined
            return !string.IsNullOrWhiteSpace(CurrentUsername)
                && !string.IsNullOrWhiteSpace(CurrentPassword)
                && Enum.IsDefined(typeof(UserRole), SelectedRole);
        }

        private void ExecuteAdd()
        {
            try
            {
                // Ensure username is unique
                if (_userService.GetAllUsers()
                    .Any(u => u.Username == CurrentUsername.Trim()))
                {
                    MessageBox.Show(
                        $"Username '{CurrentUsername.Trim()}' already exists.",
                        "Add User Failed",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                var newUser = new User
                {
                    Username = CurrentUsername.Trim(),
                    PasswordHash = CurrentPassword, // In real app: hash before storing
                    Role = SelectedRole
                };

                _userService.AddUser(newUser);
                RefreshUsers();
                SelectedUser = null; // Clear inputs
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error adding user:\n{ex.Message}",
                    "Add Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        #endregion

        #region Edit Logic

        private bool CanExecuteEdit()
        {
            if (!IsAdmin || SelectedUser == null) return false;

            return !string.IsNullOrWhiteSpace(CurrentUsername)
                && !string.IsNullOrWhiteSpace(CurrentPassword)
                && Enum.IsDefined(typeof(UserRole), SelectedRole);
        }

        private void ExecuteEdit()
        {
            if (SelectedUser == null) return;

            try
            {
                // Check if another user already has this new username
                if (_userService.GetAllUsers()
                    .Any(u => u.Username == CurrentUsername.Trim() && u.Id != SelectedUser.Id))
                {
                    MessageBox.Show(
                        $"Username '{CurrentUsername.Trim()}' is already taken by another user.",
                        "Edit User Failed",
                        MessageBoxButton.OK,
                        MessageBoxImage.Warning);
                    return;
                }

                var updated = new User
                {
                    Id = SelectedUser.Id,
                    Username = CurrentUsername.Trim(),
                    PasswordHash = CurrentPassword,
                    Role = SelectedRole
                };

                // Remove old then add updated (since no UpdateUser method exists)
                _userService.RemoveUser(SelectedUser.Id);
                _userService.AddUser(updated);

                RefreshUsers();
                SelectedUser = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error editing user:\n{ex.Message}",
                    "Edit Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        #endregion

        #region Delete Logic

        private bool CanExecuteDelete()
        {
            return IsAdmin && SelectedUser != null;
        }

        private void ExecuteDelete()
        {
            if (SelectedUser == null) return;

            // Prevent self‐delete
            if (SelectedUser.Username == SessionManager.CurrentUser.Username)
            {
                MessageBox.Show(
                    "You cannot delete your own account while logged in.",
                    "Delete User Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Are you sure you want to delete user '{SelectedUser.Username}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                _userService.RemoveUser(SelectedUser.Id);
                RefreshUsers();
                SelectedUser = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error deleting user:\n{ex.Message}",
                    "Delete Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        #endregion

        private void RaiseAllCanExecuteChanged()
        {
            (AddCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (EditCommand as RelayCommand)?.RaiseCanExecuteChanged();
            (DeleteCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        #endregion
    }
}
