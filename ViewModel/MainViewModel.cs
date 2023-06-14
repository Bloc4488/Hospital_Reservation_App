using Hospital_Reservation_App.Model;
using Hospital_Reservation_App.Repositories;
using Hospital_Reservation_App.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hospital_Reservation_App.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private UserModel _currentAccount;
        private ViewModelBase _childView;
        private string _title;

        private System.Windows.Visibility _isVisibleReservation = System.Windows.Visibility.Collapsed;
        private System.Windows.Visibility _isVisibleVisits = System.Windows.Visibility.Collapsed;
        private System.Windows.Visibility _isVisibleUsers = System.Windows.Visibility.Collapsed;

        public string StringPass
        {
            get
            {
                return new NetworkCredential("", CurrentAccount.Password).Password;
            }
        }
        public UserModel CurrentAccount
        {
            get { return _currentAccount; }
            set
            {
                _currentAccount = value;
                OnPropertyChanged(nameof(CurrentAccount));
            }
        }
        public ViewModelBase ChildView
        {
            get { return _childView; }
            set
            {
                _childView = value;
                OnPropertyChanged(nameof(ChildView));
            }
        }
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public System.Windows.Visibility IsVisibleReservation
        {
            get { return _isVisibleReservation; }
            set
            {
                if (_isVisibleReservation != value)
                {
                    _isVisibleReservation = value;
                    OnPropertyChanged(nameof(IsVisibleReservation));
                }
            }
        }
        public System.Windows.Visibility IsVisibleVisits
        {
            get { return _isVisibleVisits; }
            set
            {
                if (_isVisibleVisits != value)
                {
                    _isVisibleVisits = value;
                    OnPropertyChanged(nameof(IsVisibleVisits));
                }
            }
        }
        public System.Windows.Visibility IsVisibleUsers
        {
            get { return _isVisibleUsers; }
            set
            {
                if (_isVisibleUsers != value)
                {
                    _isVisibleUsers = value;
                    OnPropertyChanged(nameof(IsVisibleUsers));
                }
            }
        }
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowUsersAdminViewCommand { get; }
        public ICommand ShowVisitsViewCommand { get; }
        public ICommand ShowReservationsViewCommand { get; }
        public ICommand ShowAddReservationViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }

        private IUserRepository userRepository;
        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentAccount = new UserModel();
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowUsersAdminViewCommand = new ViewModelCommand(ExecuteShowUsersAdminViewCommand);
            ShowReservationsViewCommand = new ViewModelCommand(ExecuteReservationsViewCommand);
            ShowAddReservationViewCommand = new ViewModelCommand(ExecuteAddReservationViewCommand);
            ShowSettingsViewCommand = new ViewModelCommand(ExecuteSettingsViewCommand);
            ExecuteShowHomeViewCommand(null);
            LoadCurrentAccountData();
            ShowButtons();
        }
        private void ExecuteShowHomeViewCommand(object obj)
        {
            ChildView = new HomeViewModel();
            Title = "Menu główne";
        }
        private void ExecuteShowUsersAdminViewCommand(object obj)
        {
            ChildView = new AdminViewModel();
            Title = "Użytkownicy";
        }
        private void ExecuteReservationsViewCommand(object obj)
        {
            ChildView = new ReservationViewModel();
            Title = "Rezerwacje";
        }
        private void ExecuteAddReservationViewCommand(object obj)
        {
            ChildView = new AddReservationViewModel();
            Title = "Dodawanie rezerwacji";
        }
        private void ExecuteSettingsViewCommand(object obj)
        {
            ChildView = new SettingsViewModel();
            Title = "Ustawienia";
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
                CurrentAccount.privilege = user.privilege;
                CurrentAccount.DisplayName = $"Witam {user.firstName} {user.lastName} !";
            }
            else
            {
                CurrentAccount.DisplayName = "User not logged in!";
            }
        }
        private void ShowButtons()
        {
            if (CurrentAccount.privilege == "1")
            {
                IsVisibleReservation = System.Windows.Visibility.Visible;
            }
            else if (CurrentAccount.privilege == "2")
            {
                IsVisibleVisits = System.Windows.Visibility.Visible;
                IsVisibleReservation = System.Windows.Visibility.Visible;
            }
            else if (CurrentAccount.privilege == "0")
            {
                IsVisibleUsers = System.Windows.Visibility.Visible;
                IsVisibleReservation = System.Windows.Visibility.Visible;
            }
        }
    }
}
