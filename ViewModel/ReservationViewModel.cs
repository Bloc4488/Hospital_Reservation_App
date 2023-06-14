using Hospital_Reservation_App.Model;
using Hospital_Reservation_App.Repositories;
using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hospital_Reservation_App.ViewModel
{
    public class ReservationViewModel : ViewModelBase
    {
        private UserModel _currentAccount;
        private ReservationModel _selectedReservation;
        private ObservableCollection<ReservationModel> _showReservations;

        private bool _checkBoxAllChecked = true;
        private bool _checkBoxPastChecked;
        private bool _checkBoxFutureChecked;

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
        public ReservationModel SelectedReservation
        {
            get { return _selectedReservation; }
            set
            {
                if (_selectedReservation != value)
                {
                    _selectedReservation = value;
                    OnPropertyChanged(nameof(SelectedReservation));
                }
            }
        }
        public ObservableCollection<ReservationModel> ShowReservations
        {
            get { return _showReservations; }
            set
            {
                if (_showReservations != value)
                {
                    _showReservations = value;
                    OnPropertyChanged(nameof(ShowReservations));
                }
            }
        }
        public bool CheckBoxAllChecked
        {
            get { return _checkBoxAllChecked; }
            set
            {
                _checkBoxAllChecked = value;
                if (value)
                {
                    LoadCurrentUserAllReservations();
                    CheckBoxPastChecked = false;
                    CheckBoxFutureChecked = false;
                }
                OnPropertyChanged(nameof(CheckBoxAllChecked));
            }
        }
        public bool CheckBoxPastChecked
        {
            get { return _checkBoxPastChecked; }
            set
            {
                _checkBoxPastChecked = value;
                if (value)
                {
                    LoadCurrentUserPastReservations();
                    CheckBoxAllChecked = false;
                    CheckBoxFutureChecked = false;
                }
                OnPropertyChanged(nameof(CheckBoxPastChecked));
            }
        }
        public bool CheckBoxFutureChecked
        {
            get { return _checkBoxFutureChecked; }
            set
            {
                _checkBoxFutureChecked = value;
                if (value)
                {
                    LoadCurrentUserFutureReservations();
                    CheckBoxPastChecked = false;
                    CheckBoxAllChecked = false;
                }
                OnPropertyChanged(nameof(CheckBoxFutureChecked));
            }
        }

        public ICommand DeleteReservationCommand { get; }

        private readonly IUserRepository userRepository;
        private readonly IReservationRepository reservationRepository;

        public ReservationViewModel()
        {
            CurrentAccount = new UserModel();
            ShowReservations = new ObservableCollection<ReservationModel>();
            userRepository = new UserRepository();
            reservationRepository = new ReservationRepository();
            DeleteReservationCommand = new ViewModelCommand(ExecuteDeleteReservationCommand, CanExecuteDeleteReservationCommand);
            LoadCurrentAccountData();
            LoadCurrentUserAllReservations();
        }

        private bool CanExecuteDeleteReservationCommand(object obj)
        {
            bool validDelete;
            if (SelectedReservation == null)
            {
                validDelete = false;
            }
            else
            {
                validDelete = true;
            }
            return validDelete;
        }

        private void ExecuteDeleteReservationCommand(object obj)
        {
            reservationRepository.DeleteReservation(SelectedReservation);
            LoadCurrentUserAllReservations();
            CheckBoxAllChecked = true;
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

        private void LoadCurrentUserAllReservations()
        {
            List<ReservationModel> list = reservationRepository.GetAllReservationsData(CurrentAccount);
            ShowReservations = new ObservableCollection<ReservationModel>(list);
        }
        private void LoadCurrentUserPastReservations()
        {
            List<ReservationModel> list = reservationRepository.GetPastReservationsData(CurrentAccount);
            ShowReservations = new ObservableCollection<ReservationModel>(list);
        }
        private void LoadCurrentUserFutureReservations()
        {
            List<ReservationModel> list = reservationRepository.GetFutureReservationsData(CurrentAccount);
            ShowReservations = new ObservableCollection<ReservationModel>(list);
        }
    }
}
