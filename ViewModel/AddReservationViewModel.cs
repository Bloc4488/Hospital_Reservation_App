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

namespace Hospital_Reservation_App.ViewModel
{
    public class AddReservationViewModel : ViewModelBase
    {
        private bool _isVisible;
        private UserModel _currentAccount;
        private DateTime _day;
        private DateTime _hour;

        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                if (_isVisible != value)
                {
                    _isVisible = value;
                    OnPropertyChanged(nameof(IsVisible));
                }
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
        public DateTime day
        {
            get { return _day; }
            set
            {
                if (_day != value)
                {
                    _day = value;
                    OnPropertyChanged(nameof(day));
                }
            }
        }

        public DateTime hour
        {
            get { return _hour; }
            set
            {
                if (_hour != value)
                {
                    _hour = value;
                    OnPropertyChanged(nameof(hour));
                }
            }
        }

        public ICommand AddReservationCommand { get; }
        private IUserRepository userRepository;
        private IReservationRepository reservationRepository;

        public AddReservationViewModel()
        {
            userRepository = new UserRepository();
            reservationRepository = new ReservationRepository();    
            AddReservationCommand = new ViewModelCommand(ExecuteAddReservationCommand, CanExecuteAddReservationCommand);
        }
        private bool CanExecuteAddReservationCommand(object obj)
        {
            bool validAdd = true;
            return validAdd;
        }
        private void ExecuteAddReservationCommand(object obj)
        {
            DateTime d = day;
            DateTime h = hour;
            String s = "2015-05-05";
            String a = "10:00:00";
            //DateTime time = new DateTime(s,a);
            DateTime time = new DateTime(2023, 12, 31, 10, 0, 0);
            //ReservationModel reservation = new ReservationModel();
            //var user = userRepository.GetUser(Thread.CurrentPrincipal.Identity.Name);
            //if (user != null)
            //{
            //    CurrentAccount.id = user.id;
            //    CurrentAccount.PESEL = user.PESEL;
            //    CurrentAccount.firstName = user.firstName;
            //    CurrentAccount.lastName = user.lastName;
            //    CurrentAccount.sex = user.sex;
            //    CurrentAccount.email = user.email;
            //    CurrentAccount.Password = user.Password;
            //}
            //else
            //{
            //    //TODO
            //    //MessageBox.Show("User not logged in!");
            //}
            reservationRepository.AddRes(1, 2, time);
        }

    }
}
