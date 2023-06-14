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
        private ObservableCollection<ReservationModel> _showReservations;
        private string _showComment;
        private string _gradeChoice;
        private DoctorNoteModel _doctorNote;
        private ObservableCollection<string> _showListGrades;

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
                    if (_selectedReservation == null)
                    {
                        DoctorNoteModel note = new DoctorNoteModel();
                        note.Note = "";
                        DoctorNote = note;
                    }
                    else
                        LoadDoctorNote();
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
        public DoctorNoteModel DoctorNote
        {
            get { return _doctorNote; }
            set
            {
                if (_doctorNote != value)
                {
                    _doctorNote = value;
                    OnPropertyChanged(nameof(DoctorNote));
                }
            }
        }
        public ObservableCollection<string> ShowListGrades
        {
            get { return _showListGrades; }
            set
            {
                if (_showListGrades != value)
                {
                    _showListGrades = value;
                    OnPropertyChanged(nameof(ShowListGrades));
                }
            }
        }

        public ICommand DeleteReservationCommand { get; }
        public ICommand AddCommentCommand { get; }

        private readonly IUserRepository userRepository;
        private readonly IReservationRepository reservationRepository;
        private readonly IGradeAndCommentRepository gradeAndCommentRepository;
        private readonly IDoctorNoteRepository doctorNoteRepository;

        public ReservationViewModel()
        {
            CurrentAccount = new UserModel();
            ShowReservations = new ObservableCollection<ReservationModel>();
            userRepository = new UserRepository();
            reservationRepository = new ReservationRepository();
            gradeAndCommentRepository = new GradeAndCommentRepository();
            doctorNoteRepository = new DoctorNoteRepository();
            DeleteReservationCommand = new ViewModelCommand(ExecuteDeleteReservationCommand, CanExecuteDeleteReservationCommand);
            AddCommentCommand = new ViewModelCommand(ExecuteAddCommentCommand, CanExecuteAddCommentCommand);
            LoadCurrentAccountData();
            LoadCurrentUserAllReservations();
            LoadShowListGrades();
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
            else if (string.IsNullOrEmpty(ShowComment))
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
            GradeAndCom.grade = GradeChoice;
            GradeAndCom.comment = ShowComment;

            gradeAndCommentRepository.AddComment(GradeAndCom);
            GradeChoice = "";
            ShowComment = "";
            SelectedReservation = null;
        }

        private void ExecuteDeleteReservationCommand(object obj)
        {
            reservationRepository.DeleteReservation(SelectedReservation);
            LoadCurrentUserAllReservations();
            CheckBoxAllChecked = true;
        }



        private void LoadShowListGrades()
        {
            ShowListGrades = new ObservableCollection<string>();
            for (int i = 5; i >= 1; i--)
            {
                string time = i.ToString();
                ShowListGrades.Add(time);
            }
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
        private void LoadDoctorNote()
        {
            DoctorNote = doctorNoteRepository.GetDoctorNote(SelectedReservation);
            if (string.IsNullOrEmpty(DoctorNote.Note))
            {
                DoctorNoteModel note = new DoctorNoteModel();
                note.Note = "Nie ma notatek od lekarza!";
                DoctorNote = note;
            }
        }
    }
}
