using Hospital_Reservation_App.Model;
using Hospital_Reservation_App.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hospital_Reservation_App.ViewModel
{
    public class VisitsViewModel : ViewModelBase
    {
        private DoctorModel _currentDoctor;
        private VisitModel _selectedVisit;
        private string _doctorNote;
        private GradeAndCommentModel _patientComment;
        private ObservableCollection<VisitModel> _showVisits;


        private bool _checkBoxAllChecked = true;
        private bool _checkBoxPastChecked;
        private bool _checkBoxFutureChecked;


        public DoctorModel CurrentDoctor
        {
            get { return _currentDoctor; } 
            set
            {
                if (_currentDoctor != value)
                {
                    _currentDoctor = value;
                    OnPropertyChanged(nameof(CurrentDoctor));
                }
            }
        }
        public VisitModel SelectedVisit
        {
            get { return _selectedVisit; }
            set
            {
                if (_selectedVisit != value)
                {
                    _selectedVisit = value;
                    if (_selectedVisit == null) 
                    {
                        GradeAndCommentModel comment = new GradeAndCommentModel();
                        comment.comment = "";
                        PatientComment = comment;
                    }
                    else
                        LoadComment();
                    OnPropertyChanged(nameof(SelectedVisit));
                }
            }
        }
        public string DoctorNote
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
        public GradeAndCommentModel PatientComment
        {
            get { return _patientComment; }
            set
            {
                if (_patientComment != value)
                {
                    _patientComment = value;
                    OnPropertyChanged(nameof(PatientComment));
                }
            }
        }
        public ObservableCollection<VisitModel> ShowVisits
        {
            get { return _showVisits; }
            set
            {
                if (_showVisits != value)
                {
                    _showVisits = value;
                    OnPropertyChanged(nameof(ShowVisits));
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
                    LoadCurrentDoctorAllVisits();
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
                    LoadCurrentDoctorPastVisits();
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
                    LoadCurrentDoctorFutureVisits();
                    CheckBoxPastChecked = false;
                    CheckBoxAllChecked = false;
                }
                OnPropertyChanged(nameof(CheckBoxFutureChecked));
            }
        }

        private readonly IReservationRepository reservationRepository;
        private readonly IUserRepository userRepository;
        private readonly IGradeAndCommentRepository gradeAndCommentRepository;
        private readonly IDoctorNoteRepository doctorNoteRepository;

        public ICommand AddNoteCommand { get; }

        public VisitsViewModel()
        {
            reservationRepository = new ReservationRepository();
            userRepository = new UserRepository();
            gradeAndCommentRepository = new GradeAndCommentRepository();
            doctorNoteRepository = new DoctorNoteRepository();
            AddNoteCommand = new ViewModelCommand(ExecuteAddNoteCommand, CanExecuteAddNoteCommand);
            LoadCurrentDoctor();
            LoadCurrentDoctorAllVisits();
        }


        private bool CanExecuteAddNoteCommand(object obj)
        {
            bool validAdd;
            if (SelectedVisit == null)
                validAdd = false;
            else if (string.IsNullOrEmpty(DoctorNote))
                validAdd = false;
            else
                validAdd = true;
            return validAdd;
        }
        private void ExecuteAddNoteCommand(object obj)
        {
            DoctorNoteModel noteModel = new DoctorNoteModel();
            noteModel.Reservation_id = SelectedVisit.ReservationId;
            noteModel.Note = DoctorNote;
            doctorNoteRepository.AddDoctorNote(noteModel);
            DoctorNote = "";
            SelectedVisit = null;
        }
        private void LoadCurrentDoctor()
        {
            var doctor = userRepository.GetDoctor(Thread.CurrentPrincipal.Identity.Name);
            if (doctor != null)
            {
                CurrentDoctor = doctor;
            }
            else
            {
                //TODO
            }
        }
        private void LoadComment()
        {
            PatientComment = gradeAndCommentRepository.GetComment(SelectedVisit);
            if (string.IsNullOrEmpty(PatientComment.comment))
            {
                GradeAndCommentModel commentModel = new GradeAndCommentModel();
                commentModel.comment = "Pacjent nie zostawił komentarz!";
                PatientComment = commentModel;
            }
        }
        private void LoadCurrentDoctorAllVisits()
        {
            ShowVisits = new ObservableCollection<VisitModel>(reservationRepository.GetAllReservationsData(CurrentDoctor));
        }
        private void LoadCurrentDoctorPastVisits()
        {
            ShowVisits = new ObservableCollection<VisitModel>(reservationRepository.GetPastReservationsData(CurrentDoctor));
        }
        private void LoadCurrentDoctorFutureVisits()
        {
            ShowVisits = new ObservableCollection<VisitModel>(reservationRepository.GetFutureReservationsData(CurrentDoctor));
        }
    }
}
