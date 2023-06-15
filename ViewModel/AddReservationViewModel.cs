using Hospital_Reservation_App.Model;
using Hospital_Reservation_App.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Text.RegularExpressions;
using Hospital_Reservation_App.View;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Windows;
using System.Collections.ObjectModel;

namespace Hospital_Reservation_App.ViewModel
{
    public class AddReservationViewModel : ViewModelBase
    {
        private UserModel _currentAccount; 

        private DoctorModel _selectedDoctor;
        private SpecialityModel _selectedSpeciality;
        private DateTime _selectedDay;
        private TimeSpan _selectedTime;
        private DateTime _selectedDayTime;

        private ObservableCollection<TimeSpan> _showListTime;
        private ObservableCollection<SpecialityModel> _showListSpeciality;
        private ObservableCollection<DoctorModel> _showListDoctors;
        
        private System.Windows.Visibility _isDateVisible = Visibility.Visible;
        private System.Windows.Visibility _isTimeVisible = Visibility.Collapsed;
        private System.Windows.Visibility _isSpecialityVisible = Visibility.Collapsed;
        private System.Windows.Visibility _isDoctorVisible = Visibility.Collapsed;
        private System.Windows.Visibility _isReservationVisible = Visibility.Collapsed;
        private System.Windows.Visibility _isReservationCompleteVisible = Visibility.Collapsed;


        public UserModel CurrentAccount
        {
            get { return _currentAccount; }
            set
            {
                if (_currentAccount != value)
                {
                    _currentAccount = value;
                    OnPropertyChanged(nameof(CurrentAccount));
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
        public DateTime SelectedDay
        {
            get { return _selectedDay; }
            set
            { 
                if (_selectedDay != value)
                {
                    _selectedDay = value;
                    OnPropertyChanged(nameof(SelectedDay));
                } 
            }
        }
        public TimeSpan SelectedTime
        {
            get { return _selectedTime; }
            set
            {
                if (_selectedTime != value)
                {
                    _selectedTime = value;
                    OnPropertyChanged(nameof(SelectedTime));
                }
            }
        }
        public DateTime SelectedDayTime
        {
            get { return _selectedDayTime; }
            set
            {
                if (_selectedDayTime != value)
                {
                    _selectedDayTime = value;
                    OnPropertyChanged(nameof(SelectedDayTime));
                }
            }
        }
        public ObservableCollection<TimeSpan> ShowListTime
        {
            get { return _showListTime; }
            set
            {
                if (_showListTime != value)
                {
                    _showListTime = value;
                    OnPropertyChanged(nameof(ShowListTime));
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
        public ObservableCollection<DoctorModel> ShowListDoctors
        {
            get { return _showListDoctors; }
            set
            {
                if (_showListDoctors != value)
                {
                    _showListDoctors = value;
                    OnPropertyChanged(nameof(ShowListDoctors));
                }
            }
        }
        public System.Windows.Visibility IsDateVisible
        {
            get { return _isDateVisible; }
            set
            {
                if (_isDateVisible != value)
                {
                    _isDateVisible = value;
                    OnPropertyChanged(nameof(IsDateVisible));
                }
            }
        }
        public System.Windows.Visibility IsTimeVisible
        {
            get { return _isTimeVisible; }
            set
            {
                if (_isTimeVisible != value)
                {
                    _isTimeVisible = value;
                    OnPropertyChanged(nameof(IsTimeVisible));
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
        public System.Windows.Visibility IsDoctorVisible
        {
            get { return _isDoctorVisible; }
            set
            {
                if (_isDoctorVisible != value)
                {
                    _isDoctorVisible = value;
                    OnPropertyChanged(nameof(IsDoctorVisible));
                }
            }
        }
        public System.Windows.Visibility IsReservationVisible
        {
            get { return _isReservationVisible; }
            set
            {
                if (_isReservationVisible != value)
                {
                    _isReservationVisible = value;
                    OnPropertyChanged(nameof(IsReservationVisible));
                }
            }
        }
        public System.Windows.Visibility IsReservationCompleteVisible
        {
            get { return _isReservationCompleteVisible; }
            set
            {
                if (_isReservationCompleteVisible != value)
                {
                    _isReservationCompleteVisible = value;
                    OnPropertyChanged(nameof(IsReservationCompleteVisible));
                }
            }
        }

        public ICommand AddReservationCommand { get; }
        public ICommand NextWindowCommand1 { get; }
        public ICommand NextWindowCommand2 { get; }
        public ICommand NextWindowCommand3 { get; }
        public ICommand NextWindowCommand4 { get; }
        public ICommand PreviousWindowCommand { get; }
        public ICommand StartWindowCommand { get; }

        private readonly IUserRepository userRepository;
        private readonly IReservationRepository reservationRepository;
        private readonly ISpecialityRepository specialityRepository;

        public AddReservationViewModel()
        {
            userRepository = new UserRepository();
            reservationRepository = new ReservationRepository();
            specialityRepository = new SpecialityRepository();

            CurrentAccount = new UserModel();

            AddReservationCommand = new ViewModelCommand(ExecuteAddReservationCommand);
            NextWindowCommand1 = new ViewModelCommand(ExecuteNextWindowCommand1, CanExecuteNextWindowCommand1);
            NextWindowCommand2 = new ViewModelCommand(ExecuteNextWindowCommand2, CanExecuteNextWindowCommand2);
            NextWindowCommand3 = new ViewModelCommand(ExecuteNextWindowCommand3, CanExecuteNextWindowCommand3);
            NextWindowCommand4 = new ViewModelCommand(ExecuteNextWindowCommand4, CanExecuteNextWindowCommand4);
            PreviousWindowCommand = new ViewModelCommand(ExecutePreviousWindowCommand);
            StartWindowCommand = new ViewModelCommand(ExecuteStartWindowCommand);
            LoadCurrentAccountData();
        }

        private void ExecuteAddReservationCommand(object obj)
        {

            ReservationModel reservation = new ReservationModel
            {
                PatientId = CurrentAccount.id,
                Doctor = SelectedDoctor,
                ReservationTime = SelectedDayTime
            };
            reservationRepository.AddRes(reservation);
            IsReservationVisible = Visibility.Collapsed;
            IsReservationCompleteVisible = Visibility.Visible;
        }
        private bool CanExecuteNextWindowCommand1(object obj)
        {
            if (SelectedDay < DateTime.Today)
            {
                return false;
            }
            else return true;
        }
        private void ExecuteNextWindowCommand1(object obj)
        {
            IsDateVisible = Visibility.Collapsed;
            IsTimeVisible = Visibility.Visible;
            LoadShowListTime();
            SelectedTime = new TimeSpan();
        }
        private bool CanExecuteNextWindowCommand2(object obj)
        {
            if (SelectedTime < new TimeSpan(9,0,0) || SelectedTime > new TimeSpan(17, 0, 0))
            {
                return false;
            }
            else return true;
        }
        private void ExecuteNextWindowCommand2(object obj)
        {
            IsTimeVisible = Visibility.Collapsed;
            IsSpecialityVisible = Visibility.Visible;
            LoadShowListSpecialty();
            SelectedSpeciality = null;
        }
        private bool CanExecuteNextWindowCommand3(object obj)
        {
            if (SelectedSpeciality == null)
            {
                return false;
            }
            else return true;
        }
        private void ExecuteNextWindowCommand3(object obj)
        {
            IsSpecialityVisible = Visibility.Collapsed;
            IsDoctorVisible = Visibility.Visible;
            LoadShowListDoctors();
            SelectedDoctor = null;
        }
        private bool CanExecuteNextWindowCommand4(object obj)
        {
            if (SelectedDoctor == null)
            {
                return false;
            }
            else return true;
        }
        private void ExecuteNextWindowCommand4(object obj)
        {
            IsDoctorVisible = Visibility.Collapsed;
            IsReservationVisible = Visibility.Visible;
        }

        private void ExecutePreviousWindowCommand(object obj)
        {
            if (IsTimeVisible == Visibility.Visible)
            {
                IsTimeVisible = Visibility.Collapsed;
                IsDateVisible = Visibility.Visible;
            }
            else if (IsSpecialityVisible == Visibility.Visible)
            {
                IsSpecialityVisible = Visibility.Collapsed;
                IsTimeVisible = Visibility.Visible;
            }
            else if (IsDoctorVisible == Visibility.Visible)
            {
                IsDoctorVisible = Visibility.Collapsed;
                IsSpecialityVisible = Visibility.Visible;
            }
            else if (IsReservationVisible == Visibility.Visible)
            {
                IsReservationVisible = Visibility.Collapsed;
                IsDoctorVisible = Visibility.Visible;
            }
        }
        private void ExecuteStartWindowCommand(object obj)
        {
            IsReservationCompleteVisible = Visibility.Collapsed;
            IsDateVisible = Visibility.Visible;
            SelectedDay = new DateTime();
            SelectedTime = new TimeSpan();
            SelectedDayTime = new DateTime();
            SelectedSpeciality = new SpecialityModel();
            SelectedDoctor = new DoctorModel();
        }

        private void LoadShowListTime()
        {
            List<DateTime> list = reservationRepository.GetUserTime(SelectedDay, CurrentAccount);
            ShowListTime = new ObservableCollection<TimeSpan>();
            for (int i = 9; i <= 17; i++)
            {
                if (list.Contains(new DateTime(SelectedDay.Year, SelectedDay.Month, SelectedDay.Day, i, 0, 0)))
                    continue;
                TimeSpan time = new TimeSpan(i, 0, 0);
                ShowListTime.Add(time);
            }
        }
        private void LoadShowListSpecialty()
        {
            List<SpecialityModel> specialities = specialityRepository.GetAll();
            ShowListSpeciality = new ObservableCollection<SpecialityModel>(specialities);
        }
        private void LoadShowListDoctors()
        {
            SelectedDayTime = new DateTime(SelectedDay.Year, SelectedDay.Month, SelectedDay.Day, SelectedTime.Hours, SelectedTime.Minutes, SelectedTime.Seconds);
            List<DoctorModel> doctors = userRepository.GetDoctorsData(SelectedDayTime, SelectedSpeciality);
            doctors.Remove(doctors.Find(x => x.Id == CurrentAccount.id));
            ShowListDoctors = new ObservableCollection<DoctorModel>(doctors);
        }
        private void LoadCurrentAccountData()
        {
            var user = userRepository.GetUser(Thread.CurrentPrincipal.Identity.Name);
            if (user != null)
            {
                CurrentAccount.id = user.id;
                CurrentAccount.PESEL = user.PESEL;
                CurrentAccount.firstName = user.firstName;
                CurrentAccount.lastName = user.lastName;
                CurrentAccount.sex = user.sex;
                CurrentAccount.email = user.email;
                CurrentAccount.Password = user.Password;
            }
            else
            {
                //TODO
                MessageBox.Show("User not logged in!");
            }
        }
    }
}
