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
        public ICommand ShowHomeViewCommand { get; }
        public MainViewModel()
        {
            userRepository = new UserRepository();
            CurrentAccount = new UserModel();
            ShowHomeViewCommand = new ViewModelCommand(ExecuteShowHomeViewCommand);
            ExecuteShowHomeViewCommand(null);
            LoadCuurentAccountData();
        }
        private void ExecuteShowHomeViewCommand(object obj)
        {
            ChildView = new HomeViewModel();
        }
        private void LoadCuurentAccountData()
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
                CurrentAccount.DisplayName = $"Witam {user.firstName} {user.lastName} !";
            }
            else
            {
                CurrentAccount.DisplayName = "User not logged in!";
            }
        }
    }
}
