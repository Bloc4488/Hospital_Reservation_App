﻿using Hospital_Reservation_App.Model;
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
        private DateTime _day;
        private DateTime _hour;
        private List<DoctorModel> _listDoctors;
        private List<SpecialityModel> _listSpeciality;
        private ObservableCollection<DoctorModel> _showListDoctors;
        private ObservableCollection<SpecialityModel> _showSpecialties;
        private DoctorModel _selectedDoctor;

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
        public DateTime Day
        {
            get { return _day; }
            set
            {
                if (_day != value)
                {
                    _day = value;
                    OnPropertyChanged(nameof(Day));
                }
            }
        }

        public DateTime Hour
        {
            get { return _hour; }
            set
            {
                if (_hour != value)
                {
                    _hour = value;
                    OnPropertyChanged(nameof(Hour));
                }
            }
        }
        public List<DoctorModel> ListDoctors
        {
            get { return _listDoctors; }
            set
            {
                if (_listDoctors != value)
                {
                    _listDoctors = value;
                    OnPropertyChanged(nameof(ListDoctors));
                }
            }
        }
        public List<SpecialityModel> ListSpeciality
        {
            get { return _listSpeciality; }
            set
            {
                if (_listSpeciality != value)
                {
                    _listSpeciality = value;
                    OnPropertyChanged(nameof(ListSpeciality));
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
        public ObservableCollection<SpecialityModel> ShowSpecialties
        {
            get { return _showSpecialties; }
            set
            {
                if (_showSpecialties != value)
                {
                    _showSpecialties = value;
                    OnPropertyChanged(nameof(ShowSpecialties));
                }
            }
        }
        public DoctorModel selectedDoctor
        {
            get { return _selectedDoctor; }
            set
            {
                if (_selectedDoctor != value)
                {
                    _selectedDoctor = value;
                    OnPropertyChanged(nameof(selectedDoctor));
                }
            }
        }
        public ICommand AddReservationCommand { get; }
        private IUserRepository userRepository;
        private IReservationRepository reservationRepository;
        private ISpecialityRepository specialityRepository;

        public AddReservationViewModel()
        {
            CurrentAccount = new UserModel();
            ListDoctors = new List<DoctorModel>();
            ListSpeciality = new List<SpecialityModel>();
            userRepository = new UserRepository();
            reservationRepository = new ReservationRepository();
            specialityRepository = new SpecialityRepository();
            AddReservationCommand = new ViewModelCommand(ExecuteAddReservationCommand, CanExecuteAddReservationCommand);
            LoadCurrentAccountData();
            LoadDoctorsData();
            LoadSpecialtiesData();
            Day = DateTime.Now;
        }
        private bool CanExecuteAddReservationCommand(object obj)
        {
            //if (string.IsNullOrEmpty(firstname) || firstname.Length < 3)
            //{
            //    ErrorMessageRegistration = "Imię musi być więcej 3 znaków!";
            //    validCreate = false;
            //}
            bool validAdd = true;
            return validAdd;
        }
        private void ExecuteAddReservationCommand(object obj)
        {
            ReservationModel ResModel = new ReservationModel();
            ResModel.Doctor = new DoctorModel();
            ResModel.PatientId = CurrentAccount.id;
            ResModel.Doctor = selectedDoctor;
            ResModel.ReservationTime = Day;
          
            reservationRepository.AddRes(ResModel.PatientId,ResModel.Doctor.Id, ResModel.ReservationTime);
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
        private void LoadDoctorsData()
        {
            ListDoctors = userRepository.GetDoctorsData();
            ShowListDoctors = new ObservableCollection<DoctorModel>(ListDoctors);
        }
        private void LoadSpecialtiesData()
        {
            ListSpeciality = specialityRepository.GetAll();
            ShowSpecialties = new ObservableCollection<SpecialityModel>(ListSpeciality);
        }
    }
}
