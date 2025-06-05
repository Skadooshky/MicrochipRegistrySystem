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
    /// ViewModel for AnimalView:
    /// - If CurrentUser.Role == UserRole.User => show only that user’s animals,
    ///   allow adding a new animal WITHOUT clinic/chip.
    /// - If CurrentUser.Role == RegistryAdministrator => full CRUD + clinic/chip.
    /// </summary>
    public class AnimalViewModel : INotifyPropertyChanged
    {
        private readonly IAnimalService _animalService;
        private readonly IMicrochipService _microchipService;
        private readonly IClinicService _clinicService;

        private readonly string _loggedInUsername;
        private readonly UserRole _currentRole;

        public AnimalViewModel()
        {
            // Read current user from SessionManager
            var user = SessionManager.CurrentUser;
            _loggedInUsername = user?.Username ?? string.Empty;
            _currentRole = user?.Role ?? UserRole.User;

            var animalRepo = new AnimalRepository();
            var chipRepo = new MicrochipRepository();
            var clinicRepo = new ClinicRepository();

            _animalService = new AnimalService(animalRepo, chipRepo);
            _microchipService = new MicrochipService(chipRepo);
            _clinicService = new ClinicService(clinicRepo);

            Animals = new ObservableCollection<Animal>();
            Clinics = new ObservableCollection<Clinic>();
            // Populate clinics (Owners don’t see this, but admins do)
            foreach (var c in _clinicService.GetAllClinics())
                Clinics.Add(c);

            RefreshAnimals();

            // Commands
            AddCommand = new RelayCommand(_ => ExecuteAdd(), _ => CanExecuteAdd());
            UpdateCommand = new RelayCommand(_ => ExecuteUpdate(), _ => CanExecuteUpdate());
            DeleteCommand = new RelayCommand(_ => ExecuteDelete(), _ => CanExecuteDelete());
            RefreshCommand = new RelayCommand(_ => RefreshAnimals());

            // Initialize CurrentAnimal 
            if (IsOwner)
            {
                // If an Owner, prefill OwnerName and leave other fields blank
                CurrentAnimal = new Animal
                {
                    OwnerName = _loggedInUsername,
                    OwnerContact = string.Empty
                };
            }
            else
            {
                CurrentAnimal = new Animal();
            }

            // These will be set via selection in the UI
            CurrentChipNumber = string.Empty;
            CurrentClinic = null!;
            SelectedAnimal = null!;
        }

        /// <summary>
        /// True if the logged‐in user’s role is “User (Owner)”.
        /// </summary>
        public bool IsOwner => _currentRole == UserRole.User;

        /// <summary>
        /// True if the logged‐in user’s role is “Registry Administrator” or higher.
        /// </summary>
        public bool IsAdmin =>
               _currentRole == UserRole.RegistryAdministrator
            || _currentRole == UserRole.SystemAdministrator
            || _currentRole == UserRole.Tester;

        public string LoggedInUsername => _loggedInUsername;

        public ObservableCollection<Animal> Animals { get; }
        public ObservableCollection<Clinic> Clinics { get; }

        private Animal? _selectedAnimal;
        public Animal? SelectedAnimal
        {
            get => _selectedAnimal;
            set
            {
                _selectedAnimal = value;
                OnPropertyChanged(nameof(SelectedAnimal));

                if (_selectedAnimal != null)
                {
                    // Copy fields into CurrentAnimal for editing/viewing
                    CurrentAnimal = new Animal
                    {
                        Id = _selectedAnimal.Id,
                        Name = _selectedAnimal.Name,
                        Type = _selectedAnimal.Type,
                        Breed = _selectedAnimal.Breed,
                        Sex = _selectedAnimal.Sex,
                        Age = _selectedAnimal.Age,
                        OwnerName = _selectedAnimal.OwnerName,
                        OwnerContact = _selectedAnimal.OwnerContact,
                        Microchip = _selectedAnimal.Microchip
                    };

                    if (_selectedAnimal.Microchip != null)
                    {
                        CurrentChipNumber = _selectedAnimal.Microchip.ChipNumber;
                        CurrentClinic = _selectedAnimal.Microchip.Clinic!;
                    }
                    else
                    {
                        CurrentChipNumber = string.Empty;
                        CurrentClinic = null!;
                    }
                }
                else
                {
                    // No selection => clear fields
                    if (IsOwner)
                    {
                        CurrentAnimal = new Animal
                        {
                            OwnerName = _loggedInUsername,
                            OwnerContact = string.Empty
                        };
                    }
                    else
                    {
                        CurrentAnimal = new Animal();
                    }

                    CurrentChipNumber = string.Empty;
                    CurrentClinic = null!;
                }

                RaiseAllCanExecuteChanged();
            }
        }

        private Animal _currentAnimal = null!;
        /// <summary>
        /// Bound to the input form.  
        /// If IsOwner==true, clinic & chip fields are ignored at add time.
        /// </summary>
        public Animal CurrentAnimal
        {
            get => _currentAnimal;
            set
            {
                _currentAnimal = value;
                OnPropertyChanged(nameof(CurrentAnimal));
                RaiseAllCanExecuteChanged();
            }
        }

        private string _currentChipNumber = string.Empty;
        public string CurrentChipNumber
        {
            get => _currentChipNumber;
            set
            {
                _currentChipNumber = value;
                OnPropertyChanged(nameof(CurrentChipNumber));
            }
        }

        private Clinic? _currentClinic;
        public Clinic? CurrentClinic
        {
            get => _currentClinic;
            set
            {
                _currentClinic = value;
                OnPropertyChanged(nameof(CurrentClinic));
            }
        }

        public ICommand AddCommand { get; }
        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        #region Refresh / Load

        /// <summary>
        /// Reloads Animals, filtering by OwnerName if IsOwner.
        /// </summary>
        private void RefreshAnimals()
        {
            Animals.Clear();

            var allAnimals = _animalService.GetAllAnimals();
            if (IsOwner)
            {
                // Only show animals that belong to this user
                var myAnimals = allAnimals
                    .Where(a => string.Equals(a.OwnerName, _loggedInUsername, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                foreach (var a in myAnimals)
                    Animals.Add(a);
            }
            else
            {
                // Admin sees all
                foreach (var a in allAnimals)
                    Animals.Add(a);
            }

            OnPropertyChanged(nameof(Animals));
        }

        #endregion

        #region Add Logic

        private bool CanExecuteAdd()
        {
            // Common required fields: Name, Type, Breed, Sex, Age, OwnerName, OwnerContact
            bool basicFieldsValid =
                !string.IsNullOrWhiteSpace(CurrentAnimal.Name)
                && Enum.IsDefined(typeof(AnimalType), CurrentAnimal.Type)
                && !string.IsNullOrWhiteSpace(CurrentAnimal.Breed)
                && CurrentAnimal.Sex.HasValue
                && CurrentAnimal.Age.HasValue
                && !string.IsNullOrWhiteSpace(CurrentAnimal.OwnerName)
                && !string.IsNullOrWhiteSpace(CurrentAnimal.OwnerContact);

            if (!basicFieldsValid)
                return false;

            if (IsOwner)
            {
                // Owner never sets clinic/chip. Only basic fields matter.
                return true;
            }
            else
            {
                // Admin can choose to attach a chip/clinic or leave blank.
                // Always allow add (clinic & chip optional).
                return true;
            }
        }

        private void ExecuteAdd()
        {
            try
            {
                // 1) Insert basic Animal record
                CurrentAnimal.OwnerName = IsOwner ? _loggedInUsername : CurrentAnimal.OwnerName;
                CurrentAnimal.OwnerContact = CurrentAnimal.OwnerContact;
                _animalService.AddAnimal(CurrentAnimal);
                var createdAnimal = CurrentAnimal;

                // 2) Only Admins can attach microchip/clinic
                if (IsAdmin
                    && !string.IsNullOrWhiteSpace(CurrentChipNumber)
                    && CurrentClinic != null)
                {
                    var chip = new Microchip
                    {
                        ChipNumber = CurrentChipNumber,
                        Status = ChipStatus.Assigned,
                        ClinicId = CurrentClinic.Id,
                        AnimalId = createdAnimal.Id
                    };
                    _microchipService.AddMicrochip(chip);
                }

                RefreshAnimals();

                // Clear fields for next entry
                if (IsOwner)
                {
                    CurrentAnimal = new Animal
                    {
                        OwnerName = _loggedInUsername,
                        OwnerContact = string.Empty
                    };
                }
                else
                {
                    CurrentAnimal = new Animal();
                }

                CurrentChipNumber = string.Empty;
                CurrentClinic = null!;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding animal:\n{ex.Message}",
                                "Add Failed",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        #endregion

        #region Update Logic (Admins only)

        private bool CanExecuteUpdate()
        {
            if (!IsAdmin || SelectedAnimal == null)
                return false;

            // Same basic validation as add
            bool basicFieldsValid =
                !string.IsNullOrWhiteSpace(CurrentAnimal.Name)
                && Enum.IsDefined(typeof(AnimalType), CurrentAnimal.Type)
                && !string.IsNullOrWhiteSpace(CurrentAnimal.Breed)
                && CurrentAnimal.Sex.HasValue
                && CurrentAnimal.Age.HasValue
                && !string.IsNullOrWhiteSpace(CurrentAnimal.OwnerName)
                && !string.IsNullOrWhiteSpace(CurrentAnimal.OwnerContact);

            if (!basicFieldsValid)
                return false;

            return true;
        }

        private void ExecuteUpdate()
        {
            if (SelectedAnimal == null)
                return;

            try
            {
                // 1) Update the Animal fields
                var toUpdate = new Animal
                {
                    Id = CurrentAnimal.Id,
                    Name = CurrentAnimal.Name,
                    Type = CurrentAnimal.Type,
                    Breed = CurrentAnimal.Breed,
                    Sex = CurrentAnimal.Sex,
                    Age = CurrentAnimal.Age,
                    OwnerName = CurrentAnimal.OwnerName,
                    OwnerContact = CurrentAnimal.OwnerContact
                };
                _animalService.UpdateAnimal(toUpdate);

                // 2) Handle microchip assignment/deletion
                var existingChip = SelectedAnimal.Microchip;
                if (!string.IsNullOrWhiteSpace(CurrentChipNumber)
                    && CurrentClinic != null)
                {
                    if (existingChip != null)
                    {
                        existingChip.ChipNumber = CurrentChipNumber;
                        existingChip.ClinicId = CurrentClinic.Id;
                        _microchipService.UpdateMicrochip(existingChip);
                    }
                    else
                    {
                        var newChip = new Microchip
                        {
                            ChipNumber = CurrentChipNumber,
                            Status = ChipStatus.Assigned,
                            ClinicId = CurrentClinic.Id,
                            AnimalId = CurrentAnimal.Id
                        };
                        _microchipService.AddMicrochip(newChip);
                    }
                }
                else if (existingChip != null)
                {
                    // If admin cleared chip or clinic fields => remove chip
                    _microchipService.DeleteMicrochip(existingChip.ChipNumber);
                }

                RefreshAnimals();
                SelectedAnimal = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating animal:\n{ex.Message}",
                                "Update Failed",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        #endregion

        #region Delete Logic (Admins only)

        private bool CanExecuteDelete()
        {
            return IsAdmin && SelectedAnimal != null;
        }

        private void ExecuteDelete()
        {
            if (SelectedAnimal == null)
                return;

            try
            {
                // Delete chip first if exists
                var chipNumber = SelectedAnimal.Microchip?.ChipNumber;
                if (!string.IsNullOrWhiteSpace(chipNumber))
                    _microchipService.DeleteMicrochip(chipNumber);

                var repo = new AnimalRepository();
                repo.Delete(SelectedAnimal.Id);

                RefreshAnimals();
                SelectedAnimal = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting animal:\n{ex.Message}",
                                "Delete Failed",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        #endregion

        private void RaiseAllCanExecuteChanged()
        {
            ((RelayCommand)AddCommand).RaiseCanExecuteChanged();
            ((RelayCommand)UpdateCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
