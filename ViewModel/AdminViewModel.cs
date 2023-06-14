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
    public class AdminViewModel : ViewModelBase
    {
        private ObservableCollection<UserModel> _showPatients;
        private UserModel _selectedPatient;

        private System.Windows.Visibility _isPatientsVisible = Visibility.Visible;
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
        private readonly IUserRepository userRepository;

        public AdminViewModel()
        {
            userRepository = new UserRepository();
            ShowPatientsCommand = new ViewModelCommand(ExecuteShowPatientsCommand);
            ShowDoctorsCommand = new ViewModelCommand(ExecuteShowDoctorsCommand);
            ShowSpecialityCommand = new ViewModelCommand(ExecuteShowSpecialityCommand);
        }

        private void ExecuteShowPatientsCommand(object obj)
        {
            IsPatientsVisible = Visibility.Visible;
            IsDoctorsVisible = Visibility.Collapsed;
            IsSpecialityVisible = Visibility.Collapsed;
            LoadPatients();
        }
        private void ExecuteShowDoctorsCommand(object obj)
        {
            IsDoctorsVisible = Visibility.Visible;
            IsPatientsVisible = Visibility.Collapsed;
            IsSpecialityVisible = Visibility.Collapsed;
        }
        private void ExecuteShowSpecialityCommand(object obj)
        {
            IsSpecialityVisible = Visibility.Visible;
            IsDoctorsVisible = Visibility.Collapsed;
            IsPatientsVisible = Visibility.Collapsed;
        }
        private void LoadPatients()
        {
            List<UserModel> patients = userRepository.GetPatients();
            ShowPatients = new ObservableCollection<UserModel>(patients);
        }
    }
}
