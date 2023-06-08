using Hospital_Reservation_App.Model;
using Hospital_Reservation_App.Repositories;
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
        private string _reservationText;
        private IUserRepository userRepository;
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
        public string ReservationText
        {
            get { return _reservationText; }
            set
            {
                _reservationText = value;
                OnPropertyChanged(nameof(ReservationText));
            }
        }
        public ICommand ShowHomeViewCommand { get; }
        public ICommand ShowReservationsViewCommand { get; }
        public ICommand ShowAddReservationViewCommand { get; }
        public ICommand ShowSettingsViewCommand { get; }
        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentAccount = new UserModel();
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ShowReservationsViewCommand = new ViewModelCommand(ExecuteReservationsViewCommand);
            ShowAddReservationViewCommand = new ViewModelCommand(ExecuteAddReservationViewCommand);
            ShowSettingsViewCommand = new ViewModelCommand(ExecuteSettingsViewCommand);
            ExecuteShowHomeViewCommand(null);
            LoadCurrentAccountData();
        }
        private void ExecuteShowHomeViewCommand(object obj)
        {
            ChildView = new HomeViewModel();
            Title = "Menu główne";
        }
        private void ExecuteReservationsViewCommand(object obj)
        {
            if (CurrentAccount.privilege == "1")
            {
                ChildView = new PatientMainViewModel();
                Title = "Rezerwacje";
            }
            else if (CurrentAccount.privilege == "2")
            {
                ChildView = new DoctorMainViewModel();
                Title = "Wizyty";
            }
            else if (CurrentAccount.privilege == "0")
            {
                ChildView = new AdminViewModel();
                Title = "Użytkownicy";
            }
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
                if (CurrentAccount.privilege == "1")
                    ReservationText = "Rezerwacje";
                else if (CurrentAccount.privilege == "2")
                    ReservationText = "Wizyty";
                else if (CurrentAccount.privilege == "0")
                    ReservationText = "Użytkownicy";
                CurrentAccount.DisplayName = $"Witam {user.firstName} {user.lastName} !";
            }
            else
            {
                CurrentAccount.DisplayName = "User not logged in!";
            }
        }
    }
}
