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
    /// ViewModel for MicrochipView: generate, assign, delete microchips.
    /// Only admins (RegistryAdministrator, SystemAdministrator, Tester) may perform these actions.
    /// Plain Users see no microchip controls (you might hide the entire tab).
    /// </summary>
    public class MicrochipViewModel : INotifyPropertyChanged
    {
        private readonly IMicrochipService _microchipService;
        private readonly IAnimalService _animalService;
        private readonly IClinicService _clinicService;

        private readonly UserRole _currentRole;
        private readonly string _loggedInUsername;

        public MicrochipViewModel()
        {
            // Determine current user role
            var user = SessionManager.CurrentUser;
            _currentRole = user?.Role ?? UserRole.User;
            _loggedInUsername = user?.Username ?? string.Empty;

            var chipRepo = new MicrochipRepository();
            var animalRepo = new AnimalRepository();
            var clinicRepo = new ClinicRepository();

            _microchipService = new MicrochipService(chipRepo);
            _animalService = new AnimalService(animalRepo, chipRepo);
            _clinicService = new ClinicService(clinicRepo);

            AllChips = new ObservableCollection<Microchip>();
            AvailableChips = new ObservableCollection<Microchip>();
            AnimalsWithoutChip = new ObservableCollection<Animal>();
            Clinics = new ObservableCollection<Clinic>();

            GenerateCommand = new RelayCommand(_ => ExecuteGenerate(), _ => CanExecuteGenerate());
            AssignCommand = new RelayCommand(_ => ExecuteAssign(), _ => CanExecuteAssign());
            SearchCommand = new RelayCommand(_ => ExecuteSearch(), _ => CanExecuteSearch());
            DeleteCommand = new RelayCommand(_ => ExecuteDelete(), _ => CanExecuteDelete());
            RefreshCommand = new RelayCommand(_ => RefreshAll(), _ => true);

            // Default inputs
            GenerateCount = "1";
            SelectedChip = null!;
            SelectedAnimal = null!;
            SelectedClinic = null!;
            SearchChipNumber = string.Empty;
            FoundMicrochip = null;

            RefreshAll();
        }

        /// <summary>
        /// True if the logged‐in user’s role is a plain “User” (owner).
        /// </summary>
        public bool IsOwner => _currentRole == UserRole.User;

        /// <summary>
        /// True if the logged‐in user’s role is “RegistryAdministrator”, 
        /// “SystemAdministrator”, or “Tester”. Those may perform microchip actions.
        /// </summary>
        public bool IsAdmin =>
               _currentRole == UserRole.RegistryAdministrator
            || _currentRole == UserRole.SystemAdministrator
            || _currentRole == UserRole.Tester;

        public ObservableCollection<Microchip> AllChips { get; }
        public ObservableCollection<Microchip> AvailableChips { get; }
        public ObservableCollection<Animal> AnimalsWithoutChip { get; }
        public ObservableCollection<Clinic> Clinics { get; }

        private Microchip? _selectedChip;
        public Microchip? SelectedChip
        {
            get => _selectedChip;
            set
            {
                _selectedChip = value;
                OnPropertyChanged(nameof(SelectedChip));
                ((RelayCommand)AssignCommand).RaiseCanExecuteChanged();
            }
        }

        private Animal? _selectedAnimal;
        public Animal? SelectedAnimal
        {
            get => _selectedAnimal;
            set
            {
                _selectedAnimal = value;
                OnPropertyChanged(nameof(SelectedAnimal));
                ((RelayCommand)AssignCommand).RaiseCanExecuteChanged();
            }
        }

        private Clinic? _selectedClinic;
        public Clinic? SelectedClinic
        {
            get => _selectedClinic;
            set
            {
                _selectedClinic = value;
                OnPropertyChanged(nameof(SelectedClinic));
                ((RelayCommand)AssignCommand).RaiseCanExecuteChanged();
            }
        }

        private string _generateCount = "1";
        public string GenerateCount
        {
            get => _generateCount;
            set
            {
                _generateCount = value;
                OnPropertyChanged(nameof(GenerateCount));
                ((RelayCommand)GenerateCommand).RaiseCanExecuteChanged();
            }
        }

        private string _searchChipNumber = string.Empty;
        public string SearchChipNumber
        {
            get => _searchChipNumber;
            set
            {
                _searchChipNumber = value;
                OnPropertyChanged(nameof(SearchChipNumber));
                ((RelayCommand)SearchCommand).RaiseCanExecuteChanged();
            }
        }

        private Microchip? _foundMicrochip;
        public Microchip? FoundMicrochip
        {
            get => _foundMicrochip;
            set
            {
                _foundMicrochip = value;
                OnPropertyChanged(nameof(FoundMicrochip));
                ((RelayCommand)DeleteCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand GenerateCommand { get; }
        public ICommand AssignCommand { get; }
        public ICommand SearchCommand { get; }
        public ICommand DeleteCommand { get; }
        public ICommand RefreshCommand { get; }

        #region Refresh Logic

        private void RefreshAll()
        {
            AllChips.Clear();
            foreach (var c in _microchipService.GetAllMicrochips())
                AllChips.Add(c);

            AvailableChips.Clear();
            foreach (var c in AllChips.Where(c => c.AnimalId == null))
                AvailableChips.Add(c);

            AnimalsWithoutChip.Clear();
            var allAnimals = _animalService.GetAllAnimals();
            if (IsOwner)
            {
                // Owner only sees their own animals that have no chip
                var myAnimalsNoChip = allAnimals
                    .Where(a => string.Equals(a.OwnerName, _loggedInUsername, StringComparison.OrdinalIgnoreCase))
                    .Where(a => _microchipService.GetMicrochipByAnimalId(a.Id) == null);
                foreach (var a in myAnimalsNoChip)
                    AnimalsWithoutChip.Add(a);
            }
            else
            {
                foreach (var a in allAnimals)
                {
                    if (_microchipService.GetMicrochipByAnimalId(a.Id) == null)
                        AnimalsWithoutChip.Add(a);
                }
            }

            Clinics.Clear();
            foreach (var c in _clinicService.GetAllClinics())
                Clinics.Add(c);

            OnPropertyChanged(nameof(AllChips));
            OnPropertyChanged(nameof(AvailableChips));
            OnPropertyChanged(nameof(AnimalsWithoutChip));
            OnPropertyChanged(nameof(Clinics));
        }

        #endregion

        #region Generate Logic

        private bool CanExecuteGenerate()
        {
            if (!IsAdmin) return false;

            return int.TryParse(GenerateCount, out var n) && n > 0;
        }

        private void ExecuteGenerate()
        {
            if (!int.TryParse(GenerateCount, out var count) || count < 1)
            {
                MessageBox.Show(
                    "Please enter a valid positive integer to generate microchips.",
                    "Invalid Input",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            try
            {
                _microchipService.GenerateMicrochips(count);
                MessageBox.Show(
                    $"{count} new microchip(s) generated successfully.",
                    "Generate Complete",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                GenerateCount = "1";
                RefreshAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error generating microchips:\n{ex.Message}",
                    "Generate Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        #endregion

        #region Assign Logic

        private bool CanExecuteAssign()
        {
            if (!IsAdmin) return false;

            return SelectedChip != null
                && SelectedAnimal != null
                && SelectedClinic != null;
        }

        private void ExecuteAssign()
        {
            if (SelectedChip == null || SelectedAnimal == null || SelectedClinic == null)
                return;

            try
            {
                SelectedChip.AnimalId = SelectedAnimal.Id;
                SelectedChip.ClinicId = SelectedClinic.Id;
                SelectedChip.Status = ChipStatus.Assigned;

                _microchipService.UpdateMicrochip(SelectedChip);

                MessageBox.Show(
                    $"Microchip '{SelectedChip.ChipNumber}' assigned to animal '{SelectedAnimal.Name}' " +
                    $"at clinic '{SelectedClinic.Name}'.",
                    "Assignment Success",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                SelectedChip = null!;
                SelectedAnimal = null!;
                SelectedClinic = null!;
                RefreshAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error assigning microchip:\n{ex.Message}",
                    "Assign Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        #endregion

        #region Search/Delete Logic

        private bool CanExecuteSearch()
        {
            return !string.IsNullOrWhiteSpace(SearchChipNumber);
        }

        private void ExecuteSearch()
        {
            try
            {
                var chip = _microchipService
                              .GetMicrochipByNumber(SearchChipNumber.Trim());

                if (chip == null)
                {
                    MessageBox.Show(
                        $"No microchip found with number '{SearchChipNumber}'.",
                        "Not Found",
                        MessageBoxButton.OK,
                        MessageBoxImage.Information);

                    FoundMicrochip = null;
                }
                else
                {
                    FoundMicrochip = chip;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error searching microchip:\n{ex.Message}",
                    "Search Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);

                FoundMicrochip = null;
            }
        }

        private bool CanExecuteDelete()
        {
            if (!IsAdmin) return false;
            return FoundMicrochip != null;
        }

        private void ExecuteDelete()
        {
            if (FoundMicrochip == null)
                return;

            var result = MessageBox.Show(
                $"Are you sure you want to delete microchip '{FoundMicrochip.ChipNumber}'?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result != MessageBoxResult.Yes)
                return;

            try
            {
                _microchipService.DeleteMicrochip(FoundMicrochip.ChipNumber);

                MessageBox.Show(
                    $"Microchip '{FoundMicrochip.ChipNumber}' deleted successfully.",
                    "Delete Complete",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                FoundMicrochip = null;
                SearchChipNumber = string.Empty;
                RefreshAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error deleting microchip:\n{ex.Message}",
                    "Delete Failed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        #endregion
    }
}
