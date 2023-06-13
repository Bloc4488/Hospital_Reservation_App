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
using System.Windows.Forms;
using System.Windows.Input;

namespace Hospital_Reservation_App.ViewModel
{
    public class ReservationViewModel : ViewModelBase
    {
        private UserModel _currentAccount;
        private ReservationModel _selectedReservation;
        private List<ReservationModel> _listReservations;
        private ObservableCollection<ReservationModel> _showReservations;
        private string _showComment;
        private string _gradeChoice;

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
        public List<ReservationModel> ListReservations
        {
            get { return _listReservations; }
            set
            {
                if (_listReservations != value)
                {
                    _listReservations = value;
                    OnPropertyChanged(nameof(ListReservations));
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

        public string ShowComment
        {
            get { return _showComment; }
            set
            {
                if (_showComment != value)
                {
                    _showComment = value;
                    OnPropertyChanged(nameof(ShowComment));
                }
            }
        }

        public string GradeChoice
        {
            get { return _gradeChoice; }
            set
            {
                if (_gradeChoice != value)
                {
                    _gradeChoice = value;
                    OnPropertyChanged(nameof(GradeChoice));
                }
            }
        }

        public ICommand DeleteReservationCommand { get; }
        public ICommand AddCommentCommand { get; }

        private IUserRepository userRepository;
        private IReservationRepository reservationRepository;
        private IGradeAndCommentRepository gradeAndCommentRepository;

        public ReservationViewModel()
        {
            CurrentAccount = new UserModel();
            ListReservations = new List<ReservationModel>();
            ShowReservations = new ObservableCollection<ReservationModel>();
            userRepository = new UserRepository();
            reservationRepository = new ReservationRepository();
            gradeAndCommentRepository = new GradeAndCommentRepository();
            DeleteReservationCommand = new ViewModelCommand(ExecuteDeleteReservationCommand, CanExecuteDeleteReservationCommand);
            AddCommentCommand = new ViewModelCommand(ExecuteAddCommentCommand, CanExecuteAddCommentCommand);
            LoadCurrentAccountData();
            LoadCurrentUserReservations();
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

        private bool CanExecuteAddCommentCommand(object obj)
        {
            bool validAdd;
            if (SelectedReservation == null)
            {
                validAdd = false;
            }
            else
            {
                validAdd = true;
            }
            return validAdd;
        }

        private void ExecuteAddCommentCommand(object obj)
        {
            GradeAndCommentModel GradeAndCom = new GradeAndCommentModel();

            GradeAndCom.ReservationID = SelectedReservation.Id;
            //GradeAndCom.grade = GradeChoice;
            GradeAndCom.grade = "4";
            GradeAndCom.comment = ShowComment;

            gradeAndCommentRepository.AddComment(GradeAndCom);
        }

        private void ExecuteDeleteReservationCommand(object obj)
        {
            reservationRepository.DeleteReservation(SelectedReservation);
            LoadCurrentUserReservations();
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
                //MessageBox.Show("User not logged in!");
            }
        }

        private void LoadCurrentUserReservations()
        {
            ListReservations = reservationRepository.GetReservationsData(CurrentAccount);
            ShowReservations = new ObservableCollection<ReservationModel>(ListReservations);
        }
    }
}
