using Hospital_Reservation_App.Model;
using Hospital_Reservation_App.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hospital_Reservation_App.ViewModel
{
    /// <summary>
    /// ViewModel class for AdminView
    /// </summary>
    public class AdminViewModel : ViewModelBase
    {
        private ObservableCollection<UserModel> _showPatients;
        private ObservableCollection<DoctorModel> _showDoctors;
        private ObservableCollection<SpecialityModel> _showListSpeciality;
        private ObservableCollection<SpecialityModel> _showSpecialities;

        private UserModel _selectedPatient;
        private DoctorModel _selectedDoctor;
        private SpecialityModel _selectedSpeciality;
        private SpecialityModel _selectedSpeciality2;
        private string _newSpeciality;

        private System.Windows.Visibility _isPatientsVisible = Visibility.Collapsed;
        private System.Windows.Visibility _isDoctorsVisible = Visibility.Collapsed;
        private System.Windows.Visibility _isSpecialityVisible = Visibility.Collapsed;


        public ObservableCollection<UserModel> ShowPatients
        {
            get { return _showPatients; }
            set
            {
                if (_showPatients != value)
                {
                    _showPatients = value;
                    OnPropertyChanged(nameof(ShowPatients));
                }
            }
        }
        public ObservableCollection<DoctorModel> ShowDoctors
        {
            get { return _showDoctors; }
            set
            {
                if (_showDoctors != value)
                {
                    _showDoctors = value;
                    OnPropertyChanged(nameof(ShowDoctors));
                }
            }
        }
        public ObservableCollection<SpecialityModel> ShowListSpeciality
        {
            get { return _showListSpeciality; }
            set
            {
                if (_showListSpeciality  != value)
                {
                    _showListSpeciality = value;
                    OnPropertyChanged(nameof(ShowListSpeciality));
                }
            }
        }
        public ObservableCollection<SpecialityModel> ShowSpecialities
        {
            get { return _showSpecialities; }
            set
            {
                if (_showSpecialities != value)
                {
                    _showSpecialities = value;
                    OnPropertyChanged(nameof(ShowSpecialities));
                }
            }
        }
        public UserModel SelectedPatient
        {
            get { return _selectedPatient; }
            set
            {
                if (_selectedPatient != value)
                {
                    _selectedPatient = value;
                    OnPropertyChanged(nameof(SelectedPatient));
                }
            }
        }
        public DoctorModel SelectedDoctor
        {
            get { return _selectedDoctor; }
            set
            {
                if (_selectedDoctor != value)
                {
                    _selectedDoctor = value;
                    OnPropertyChanged(nameof(SelectedDoctor));
                }
            }
        }
        public SpecialityModel SelectedSpeciality
        {
            get { return _selectedSpeciality; }
            set
            {
                if (_selectedSpeciality != value)
                {
                    _selectedSpeciality = value;
                    OnPropertyChanged(nameof(SelectedSpeciality));
                }
            }
        }
        public SpecialityModel SelectedSpeciality2
        {
            get { return _selectedSpeciality2; }
            set
            {
                if (_selectedSpeciality2 != value)
                {
                    _selectedSpeciality2 = value;
                    OnPropertyChanged(nameof(SelectedSpeciality2));
                }
            }
        }
        public string NewSpeciality
        {
            get { return _newSpeciality; }
            set
            {
                if (_newSpeciality != value)
                {
                    _newSpeciality = value;
                    OnPropertyChanged(nameof(NewSpeciality));
                }
            }
        }
        public System.Windows.Visibility IsPatientsVisible
        {
            get { return _isPatientsVisible; }
            set
            {
                if (_isPatientsVisible != value)
                {
                    _isPatientsVisible = value;
                    OnPropertyChanged(nameof(IsPatientsVisible));
                }
            }
        }
        public System.Windows.Visibility IsDoctorsVisible
        {
            get { return _isDoctorsVisible; }
            set
            {
                if (_isDoctorsVisible != value)
                {
                    _isDoctorsVisible = value;
                    OnPropertyChanged(nameof(IsDoctorsVisible));
                }
            }
        }
        public System.Windows.Visibility IsSpecialityVisible
        {
            get { return _isSpecialityVisible; }
            set
            {
                if (_isSpecialityVisible != value)
                {
                    _isSpecialityVisible = value;
                    OnPropertyChanged(nameof(IsSpecialityVisible));
                }
            }
        }

        public ICommand ShowPatientsCommand { get; }
        public ICommand ShowDoctorsCommand { get; }
        public ICommand ShowSpecialityCommand { get; }
        public ICommand DeleteDoctorCommand { get; }
        public ICommand DeleteDoctorUserCommand { get; }
        public ICommand MakeDoctorCommand { get; }
        public ICommand DeleteUserCommand { get; }
        public ICommand DeleteSpecialityCommand { get; }
        public ICommand MakeSpecialityCommand { get; }

        private readonly IUserRepository userRepository;
        private readonly ISpecialityRepository specialityRepository;

        public AdminViewModel()
        {
            userRepository = new UserRepository();
            specialityRepository = new SpecialityRepository();
            ShowPatientsCommand = new ViewModelCommand(ExecuteShowPatientsCommand);
            ShowDoctorsCommand = new ViewModelCommand(ExecuteShowDoctorsCommand);
            ShowSpecialityCommand = new ViewModelCommand(ExecuteShowSpecialityCommand);
            DeleteDoctorCommand = new ViewModelCommand(ExecuteDeleteDoctorCommand, CanExecuteDeleteDoctorCommand);
            DeleteDoctorUserCommand = new ViewModelCommand(ExecuteDeleteDoctorUserCommand, CanExecuteDeleteDoctorCommand);
            MakeDoctorCommand = new ViewModelCommand(ExecuteMakeDoctorCommand, CanExecuteMakeDoctorCommand);
            DeleteUserCommand = new ViewModelCommand(ExecuteDeleteUserCommand, CanExecuteDeleteUserCommand);
            DeleteSpecialityCommand = new ViewModelCommand(ExecuteDeleteSpecialityCommand, CanExecuteDeleteSpecialityCommand);
            MakeSpecialityCommand = new ViewModelCommand(ExecuteMakeSpecialityCommand, CanExecuteMakeSpecialityCommand);
        }

        private void ExecuteShowPatientsCommand(object obj)
        {
            IsPatientsVisible = Visibility.Visible;
            IsDoctorsVisible = Visibility.Collapsed;
            IsSpecialityVisible = Visibility.Collapsed;
            LoadPatients();
            LoadListSpecialties();
        }
        private void ExecuteShowDoctorsCommand(object obj)
        {
            IsDoctorsVisible = Visibility.Visible;
            IsPatientsVisible = Visibility.Collapsed;
            IsSpecialityVisible = Visibility.Collapsed;
            LoadDoctors();
        }
        private void ExecuteShowSpecialityCommand(object obj)
        {
            IsSpecialityVisible = Visibility.Visible;
            IsDoctorsVisible = Visibility.Collapsed;
            IsPatientsVisible = Visibility.Collapsed;
            LoadSpecialties();
        }
        private void ExecuteDeleteDoctorCommand(object obj)
        {
            userRepository.DeleteDoctorFromDoctors(SelectedDoctor);
            LoadDoctors();
        }
        private void ExecuteDeleteDoctorUserCommand(object obj)
        {
            userRepository.Delete(SelectedDoctor);
            LoadDoctors();
        }
        private bool CanExecuteDeleteDoctorCommand(object obj)
        {
            bool validDelete;
            if (SelectedDoctor == null)
                validDelete = false;
            else
                validDelete = true;
            return validDelete;
        }
        private bool CanExecuteMakeDoctorCommand(object obj)
        {
            bool validMake;
            if (SelectedPatient == null)
                validMake = false;
            else if (SelectedSpeciality == null)
                validMake = false;
            else
                validMake = true;
            return validMake;
        }
        private void ExecuteMakeDoctorCommand(object obj)
        {
            userRepository.CreateDoctor(SelectedPatient, SelectedSpeciality);
            LoadPatients();
        }
        private void ExecuteDeleteUserCommand(object obj)
        {
            userRepository.Delete(SelectedPatient);
            SelectedSpeciality = new SpecialityModel();
            LoadPatients();
        }
        private bool CanExecuteDeleteUserCommand(object obj)
        {
            bool validDelete;
            if (SelectedPatient == null)
                validDelete = false;
            else
                validDelete = true;
            return validDelete;
        }
        private void ExecuteDeleteSpecialityCommand(object obj)
        {
            specialityRepository.Delete(SelectedSpeciality2);
            LoadSpecialties();
        }
        private bool CanExecuteDeleteSpecialityCommand(object obj)
        {
            bool validDelete;
            if (SelectedSpeciality2 == null)
                validDelete = false;
            else
                validDelete = true;
            return validDelete;
        }
        private void ExecuteMakeSpecialityCommand(object obj)
        {
            SpecialityModel specialityModel = new SpecialityModel();
            specialityModel.Name = NewSpeciality;
            specialityRepository.Add(specialityModel);
            NewSpeciality = null;
            LoadSpecialties();
        }
        private bool CanExecuteMakeSpecialityCommand(object obj)
        {
            bool validAdd;
            if (string.IsNullOrEmpty(NewSpeciality))
                validAdd = false;
            else
                validAdd = true;
            return validAdd;
        }
        private void LoadPatients()
        {
            List<UserModel> patients = userRepository.GetPatients();
            ShowPatients = new ObservableCollection<UserModel>(patients);
        }
        private void LoadDoctors()
        {
            List<DoctorModel> doctors = userRepository.GetDoctors();
            ShowDoctors = new ObservableCollection<DoctorModel>(doctors);
        }
        private void LoadListSpecialties()
        {
            List<SpecialityModel> specialities = specialityRepository.GetAll();
            ShowListSpeciality = new ObservableCollection<SpecialityModel>(specialities);
        }
        private void LoadSpecialties()
        {
            List<SpecialityModel> specialities = specialityRepository.GetAll();
            ShowSpecialities = new ObservableCollection<SpecialityModel>(specialities);
        }
    }
}
