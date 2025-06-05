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
    /// ViewModel for ClinicView.
    /// Only RegistryAdministrator, SystemAdministrator, or Tester may add/edit/delete.
    /// A plain User sees the clinic list as read‐only.
    /// </summary>
    public class ClinicViewModel : INotifyPropertyChanged
    {
        private readonly IClinicService _clinicService;
        private readonly UserRole _currentRole;

        public ClinicViewModel()
        {
            // Determine current user role
            var user = SessionManager.CurrentUser;
            _currentRole = user?.Role ?? UserRole.User;

            var clinicRepo = new ClinicRepository();
            _clinicService = new ClinicService(clinicRepo);

            Clinics = new ObservableCollection<Clinic>();
            RefreshClinics();

            AddCommand = new RelayCommand(_ => ExecuteAdd(), _ => CanExecuteAdd());
            EditCommand = new RelayCommand(_ => ExecuteEdit(), _ => CanExecuteEdit());
            DeleteCommand = new RelayCommand(_ => ExecuteDelete(), _ => CanExecuteDelete());
            RefreshCommand = new RelayCommand(_ => RefreshClinics());

            // Initialize CurrentClinic
            CurrentClinic = new Clinic();
        }

        /// <summary>
        /// True if the logged‐in user’s role is “RegistryAdministrator”, 
        /// “SystemAdministrator”, or “Tester”.
        /// </summary>
        public bool IsAdmin =>
               _currentRole == UserRole.RegistryAdministrator
            || _currentRole == UserRole.SystemAdministrator
            || _currentRole == UserRole.Tester;

        /// <summary>
        /// True if the logged‐in user’s role is plain “User”.
        /// </summary>
        public bool IsOwner => _currentRole == UserRole.User;

        public ObservableCollection<Clinic> Clinics { get; }

        private Clinic? _selectedClinic;
        public Clinic? SelectedClinic
        {
            get => _selectedClinic;
            set
            {
                _selectedClinic = value;
                OnPropertyChanged(nameof(SelectedClinic));

                if (_selectedClinic != null)
                {
                    // Copy into CurrentClinic for editing
                    CurrentClinic = new Clinic
                    {
                        Id = _selectedClinic.Id,
                        Name = _selectedClinic.Name,
                        Address = _selectedClinic.Address,
                        PhoneNumber = _selectedClinic.PhoneNumber
                    };
                }
                else
                {
                    CurrentClinic = new Clinic();
                }

                RaiseAllCanExecuteChanged();
            }
        }

        private Clinic _currentClinic = null!;
        /// <summary>
        /// Bound to the “Add / Edit” fields.
        /// </summary>
        public Clinic CurrentClinic
        {
            get => _currentClinic;
            set
            {
                _currentClinic = value;
                OnPropertyChanged(nameof(CurrentClinic));
                RaiseAllCanExecuteChanged();
            }
        }

        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        #region Refresh

        private void RefreshClinics()
        {
            Clinics.Clear();
            foreach (var c in _clinicService.GetAllClinics())
                Clinics.Add(c);
            OnPropertyChanged(nameof(Clinics));
        }

        #endregion

        #region Add

        private bool CanExecuteAdd()
        {
            if (!IsAdmin) return false;

            // Name, Address, PhoneNumber required
            return !string.IsNullOrWhiteSpace(CurrentClinic.Name)
                && !string.IsNullOrWhiteSpace(CurrentClinic.Address)
                && !string.IsNullOrWhiteSpace(CurrentClinic.PhoneNumber);
        }

        private void ExecuteAdd()
        {
            try
            {
                _clinicService.AddClinic(CurrentClinic);
                RefreshClinics();
                CurrentClinic = new Clinic();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding clinic:\n{ex.Message}",
                                "Add Failed",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        #endregion

        #region Edit

        private bool CanExecuteEdit()
        {
            // Must be admin, and a clinic selected, and fields non‐empty
            if (!IsAdmin || SelectedClinic == null) return false;
            return !string.IsNullOrWhiteSpace(CurrentClinic.Name)
                && !string.IsNullOrWhiteSpace(CurrentClinic.Address)
                && !string.IsNullOrWhiteSpace(CurrentClinic.PhoneNumber);
        }

        private void ExecuteEdit()
        {
            if (SelectedClinic == null) return;

            try
            {
                var toUpdate = new Clinic
                {
                    Id = CurrentClinic.Id,
                    Name = CurrentClinic.Name,
                    Address = CurrentClinic.Address,
                    PhoneNumber = CurrentClinic.PhoneNumber
                };
                _clinicService.EditClinic(toUpdate);
                RefreshClinics();
                SelectedClinic = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error editing clinic:\n{ex.Message}",
                                "Edit Failed",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        #endregion

        #region Delete

        private bool CanExecuteDelete()
        {
            return IsAdmin && SelectedClinic != null;
        }

        private void ExecuteDelete()
        {
            if (SelectedClinic == null) return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete clinic '{SelectedClinic.Name}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes) return;

            try
            {
                _clinicService.DeleteClinic(SelectedClinic.Id);
                RefreshClinics();
                SelectedClinic = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting clinic:\n{ex.Message}",
                                "Delete Failed",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        #endregion

        private void RaiseAllCanExecuteChanged()
        {
            ((RelayCommand)AddCommand).RaiseCanExecuteChanged();
            ((RelayCommand)EditCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
        }

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        #endregion
    }
}
